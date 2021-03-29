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
using System.IO;
using System.Text;
using System.Xml.Serialization;

namespace Arad.SMS.Gateway.GeneralLibrary
{
	public class SerializationTools
	{
		public static string SerializeToXml(object obj, Type ObjType)
		{
			MemoryStream memoryStream = new MemoryStream();
			StreamReader streamReader = new StreamReader(memoryStream);
			string serializedData = string.Empty;
			try
			{
				XmlSerializer xmlSerializer = new XmlSerializer(ObjType);
				xmlSerializer.Serialize(memoryStream, obj);
				memoryStream.Seek(0, SeekOrigin.Begin);
				serializedData = streamReader.ReadToEnd();

				serializedData = System.Text.RegularExpressions.Regex.Replace(serializedData, ">\\s+<", "><");

			}
			catch
			{
				throw;
			}
			finally
			{
				memoryStream.Dispose();
				streamReader.Dispose();
			}
			return serializedData;
		}

		public static object DeserializeXml(string serializedData,Type type)
		{
			MemoryStream memoryStream = new MemoryStream(StringToUTF8ByteArray(serializedData));
			object obj;
			try
			{
				XmlSerializer xmlSerializer = new XmlSerializer(type);
				obj = xmlSerializer.Deserialize(memoryStream);
			}
			catch
			{
				throw;
			}
			finally
			{
				memoryStream.Dispose();
			}

			return obj;
		}

		private static Byte[] StringToUTF8ByteArray(string xmlString)
		{
			UTF8Encoding encoding = new UTF8Encoding();
			byte[] byteArray = encoding.GetBytes(xmlString);
			return byteArray;
		}
	}
}
