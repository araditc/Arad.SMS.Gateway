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
using Arad.SMS.Gateway.GeneralLibrary.BaseCore;
using System.Data;

namespace Arad.SMS.Gateway.Facade
{
	public class Image : FacadeEntityBase
	{
		//public static DataTable GetAllImage(Guid userGuid)
		//{
		//	Business.Image imageController = new Business.Image();
		//	return imageController.GetAllImage(userGuid);
		//}

		public static DataTable GetImagesOfGallery(Guid galleryImageGuid)
		{
			Business.Image imageController = new Business.Image();
			return imageController.GetImagesOfGallery(galleryImageGuid);
		}

		public static bool Insert(Common.Image images)
		{
			Business.Image imageController = new Business.Image();
			return imageController.Insert(images) != Guid.Empty ? true : false;
		}

		public static Common.Image LoadImage(Guid imageGuid)
		{
			Business.Image imageController = new Business.Image();
			Common.Image images = new Common.Image();
			imageController.Load(imageGuid, images);
			return images;
		}

		public static bool Delete(Guid imageGuid)
		{
			Business.Image imageController = new Business.Image();
			return imageController.Delete(imageGuid);
		}

		public static bool Update(Common.Image image)
		{
			Business.Image imageController = new Business.Image();
			return imageController.Update(image);
		}

		public static bool ActiveImage(Guid imageGuid)
		{
			Business.Image imageController = new Business.Image();
			return imageController.ActiveImage(imageGuid);
		}
	}
}
