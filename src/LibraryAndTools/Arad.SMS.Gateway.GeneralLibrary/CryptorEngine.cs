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
using System.Runtime.Serialization;
using System.Security.Cryptography;

namespace Arad.SMS.Gateway.GeneralLibrary
{
	[Serializable]
	public class CryptorEngine : ISerializable
	{
		private Rijndael rijndael;
		private int blockSize;
		private int keySize;
		private byte[] key;
		private byte[] iv;

		public int BlockSize
		{
			get
			{
				return blockSize;
			}
		}

		public int KeySize
		{
			get
			{
				return keySize;
			}
		}

		public byte[] Key
		{
			get
			{
				return key;
			}
		}

		public byte[] IV
		{
			get
			{
				return iv;
			}
		}

		public CryptorEngine()
		{
			GenerateKeys(128);
		}
		public CryptorEngine(int keySize)
		{
			GenerateKeys(keySize);
		}
		public CryptorEngine(byte[] IV, byte[] key)
		{
			rijndael = Rijndael.Create();
			rijndael.IV = IV;
			rijndael.Key = key;
		}
		public CryptorEngine(string IV, string key) : this(Helper.HexToByteArray(IV), Helper.HexToByteArray(key)) { }
		protected CryptorEngine(SerializationInfo info, StreamingContext context)
		{
			this.blockSize = info.GetInt32("BlockSize");
			this.keySize = info.GetInt32("KeySize");
			this.iv = Helper.HexToByteArray(info.GetString("IV"));
			this.key = Helper.HexToByteArray(info.GetString("Key"));

			rijndael = Rijndael.Create();

			rijndael.BlockSize = blockSize;
			rijndael.KeySize = keySize;
			rijndael.IV = iv;
			rijndael.Key = key;
		}

		public void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			info.AddValue("BlockSize", this.blockSize);
			info.AddValue("KeySize", this.keySize);
			info.AddValue("IV", Helper.ByteArrayToHex(this.iv));
			info.AddValue("Key", Helper.ByteArrayToHex(this.key));
		}

		public string GetObjectData()
		{
			System.Text.StringBuilder info = new System.Text.StringBuilder();
			info.Append(Helper.ByteArrayToHex(this.iv));
			info.Append("$");
			info.Append(Helper.ByteArrayToHex(this.key));
			return info.ToString();
		}

		public void GenerateKeys(int keySize)
		{
			rijndael = Rijndael.Create();
			rijndael.BlockSize = 128;
			rijndael.KeySize = keySize;
			rijndael.GenerateIV();
			rijndael.GenerateKey();

			this.blockSize = rijndael.BlockSize;
			this.keySize = rijndael.KeySize;
			this.key = rijndael.Key;
			this.iv = rijndael.IV;
		}

		public string Encrypt(string text)
		{
			byte[] byteArray = System.Text.Encoding.UTF8.GetBytes(text);

			MemoryStream memoryStream = new MemoryStream();
			CryptoStream cryptoStream;

			CryptoStream cryptoStreamBase64 = new CryptoStream(memoryStream, new ToBase64Transform(), CryptoStreamMode.Write);
			cryptoStream = new CryptoStream(cryptoStreamBase64, rijndael.CreateEncryptor(), CryptoStreamMode.Write);

			cryptoStream.Write(byteArray, 0, byteArray.Length);
			cryptoStream.Close();

			byte[] encryptedArray = memoryStream.ToArray();
			string encodedString = Helper.ByteArrayToHex(encryptedArray);

			return encodedString;
		}
		public string Decrypt(string encryptedText)
		{
			try
			{
				byte[] encryptedArray = Helper.HexToByteArray(encryptedText, encryptedText.Length / 2);

				MemoryStream memoryStream = new MemoryStream();
				CryptoStream cryptoStream;

				CryptoStream cryptoStreamTemp = new CryptoStream(memoryStream, rijndael.CreateDecryptor(), CryptoStreamMode.Write);
				cryptoStream = new CryptoStream(cryptoStreamTemp, new FromBase64Transform(), CryptoStreamMode.Write);

				cryptoStream.Write(encryptedArray, 0, encryptedArray.Length);
				cryptoStream.Close();

				byte[] decryptedArray = memoryStream.ToArray();
				string decodedString = System.Text.Encoding.UTF8.GetString(decryptedArray);

				return decodedString;
			}
			catch
			{
				return string.Empty;
			}
		}

		public string SimpleDecrypt(string encryptedText)
		{
			byte[] encryptedArray = Helper.HexToByteArray(encryptedText, encryptedText.Length / 2);

			MemoryStream memoryStream = new MemoryStream();
			CryptoStream cryptoStream;

			CryptoStream cryptoStreamTemp = new CryptoStream(memoryStream, rijndael.CreateDecryptor(), CryptoStreamMode.Write);
			cryptoStream = new CryptoStream(cryptoStreamTemp, new FromBase64Transform(), CryptoStreamMode.Write);

			cryptoStream.Write(encryptedArray, 0, encryptedArray.Length);
			cryptoStream.Close();

			byte[] decryptedArray = memoryStream.ToArray();
			string decodedString = System.Text.Encoding.UTF8.GetString(decryptedArray);

			return decodedString;
		}
	}
}

