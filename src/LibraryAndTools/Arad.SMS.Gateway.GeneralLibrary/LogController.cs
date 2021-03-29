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

namespace Arad.SMS.Gateway.GeneralLibrary
{
	public class LogController<TLogEnumType> where TLogEnumType : struct, IComparable, IConvertible
	{
		private static object lockObject;

		static LogController()
		{
			lockObject = new object();
		}

		public static void LogInFile(TLogEnumType type, string logText)
		{
			lock (lockObject)
			{
				string rootDirectory = string.Format("{0}{1}{2}", Helper.GetTempDirectory(), Path.DirectorySeparatorChar, typeof(TLogEnumType).ToString());

				if (!Directory.Exists(rootDirectory))
					Directory.CreateDirectory(rootDirectory);

				string targetDirectory = string.Format("{0}{1}{2}", rootDirectory, Path.DirectorySeparatorChar, type.ToString());

				if (!Directory.Exists(targetDirectory))
					Directory.CreateDirectory(targetDirectory);

				string logFilePath = string.Format("{0}{1}{2} Log.txt", targetDirectory, Path.DirectorySeparatorChar, DateTime.Now.ToShortDateString().Replace('/', '-'));

				FileStream fileStream = new FileStream(logFilePath, FileMode.OpenOrCreate, FileAccess.Write);
				StreamWriter streamWriter = new StreamWriter(fileStream);

				streamWriter.BaseStream.Seek(0, SeekOrigin.End);
				streamWriter.WriteLine(string.Format("{0} : {1}{2}{2}", DateTime.Now.ToString(), logText, Environment.NewLine));
				streamWriter.Flush();
				streamWriter.Close();

				streamWriter.Dispose();
				fileStream.Dispose();
			}
		}

		public static void LogInFile(string directoryPath, string logText, string fileName = "")
		{
			lock (lockObject)
			{
				if (!Directory.Exists(directoryPath))
					Directory.CreateDirectory(directoryPath);

				string logFilePath = string.Format("{0}{1}{2} Log.txt", directoryPath, Path.DirectorySeparatorChar, fileName == string.Empty ? DateTime.Now.ToShortDateString().Replace('/', '-') : fileName);

				FileStream fileStream = new FileStream(logFilePath, FileMode.OpenOrCreate, FileAccess.Write);
				StreamWriter streamWriter = new StreamWriter(fileStream);

				streamWriter.BaseStream.Seek(0, SeekOrigin.End);
				streamWriter.WriteLine(string.Format("{0} : {1}{2}{2}", DateTime.Now.ToString(), logText, Environment.NewLine));
				streamWriter.Flush();
				streamWriter.Close();

				streamWriter.Dispose();
				fileStream.Dispose();
			}
		}
	}
}
