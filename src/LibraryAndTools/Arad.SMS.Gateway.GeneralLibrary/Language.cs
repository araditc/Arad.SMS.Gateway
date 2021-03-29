// --------------------------------------------------------------------
// Copyright (c) 2005-2020 Arad ITC.
//
// Author : Ammar Heidari <ammar@arad-itc.org>
// Licensed under the Apache License, Version 2.0 (the "License")
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
//     http://www.apache.org/licenses/LICENSE-2.0 
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
// --------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Data;
using System.Web;

namespace Arad.SMS.Gateway.GeneralLibrary
{
	public class Language
	{
		#region---------- Constractors -------------------
		private Language() { }
		static Language()
		{
			activeLanguage = AvalibaleLanguages.en;
		}
		#endregion

		public enum AvalibaleLanguages
		{
			fa = 1, //Farsi
			en = 2, //English
		}

		private static AvalibaleLanguages activeLanguage;
		private static Hashtable languageDictionary = null;
		private static object fillDictionaryCacheLock = new object(); // Used to ensure only one thread can run FillDictionaryCache

		private static void FillDictionaryCache()
		{
			lock (fillDictionaryCacheLock)
			{
				if (languageDictionary != null && languageDictionary.Count > 0)
					return;

				Hashtable dictionaryCache = new Hashtable();

				string xmlPath = ConfigurationManager.ApplicationPhysicalPath + "Dictionaries\\";

				foreach (AvalibaleLanguages language in System.Enum.GetValues(typeof(AvalibaleLanguages)))
				{
					string languageCode = language.ToString();
					Hashtable dictionary = new Hashtable();

					LoadDictionaryHashtableFromFile(languageCode, xmlPath, ref dictionary);

					if (!dictionaryCache.ContainsKey(languageCode))
						dictionaryCache.Add(languageCode, dictionary);
				}

				if (dictionaryCache.Count == 0)
					throw new Exception("Unable to load dictionary cache.");

				languageDictionary = dictionaryCache;
			}
		}

		private static void LoadDictionaryHashtableFromFile(string languageCode, string dictionariesPath, ref Hashtable htDictionary)
		{
			string filePath = dictionariesPath + languageCode + ".xml";

			if (!System.IO.File.Exists(filePath))
				return;

			DataSet dictionaryDataSet = new DataSet();
			dictionaryDataSet.ReadXml(filePath);

			if (dictionaryDataSet.Tables.Count == 0)
				return;

			DataTable dtDictionary = dictionaryDataSet.Tables[0];

			try
			{
				foreach (DataRow word in dtDictionary.Rows)
				{
					htDictionary.Add(word["key"].ToString(), word["value"]);
				}
			}
			catch (ArgumentException ex)
			{
				throw new Exception("Duplicate key found in XML dictionary files. " + ex.Message, ex);
			}
		}

		private static void ReloadDictionary()
		{
			languageDictionary = null;
			FillDictionaryCache();
		}

		public static AvalibaleLanguages ActiveLanguage
		{
			set
			{
				if (HttpContext.Current == null)
					activeLanguage = value;
				else
				{
					if (HttpContext.Current.Session == null)
						activeLanguage = value;
					else
						HttpContext.Current.Session["Language"] = value;
				}
			}
			get
			{
				if (HttpContext.Current == null)
					return activeLanguage;
				else
				{
					if (HttpContext.Current.Session["Language"] == null)
						HttpContext.Current.Session["Language"] = AvalibaleLanguages.fa;

					return (AvalibaleLanguages)HttpContext.Current.Session["Language"];
				}
			}
		}

		public static string GetString(string keyword)
		{
			string languageCode = ActiveLanguage.ToString();

			if (keyword == null)
				return string.Empty;

			if (languageDictionary == null)
				FillDictionaryCache();

			Hashtable dictionary = (Hashtable)languageDictionary[languageCode];

			if (dictionary == null) // This situation seems to be impossible, but it happen sometimes.
			{
				languageDictionary = null;

				FillDictionaryCache();

				dictionary = (Hashtable)languageDictionary[languageCode];
			}

			if (dictionary.Contains(keyword))
				return Helper.GetStandardizeCharacters((string)dictionary[keyword]);
			else
				return keyword;
		}
	}
}
