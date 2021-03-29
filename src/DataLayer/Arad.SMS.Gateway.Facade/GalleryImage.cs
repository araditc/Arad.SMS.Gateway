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
	public class GalleryImage : FacadeEntityBase
	{
		public static DataTable GetAllGalleryImage(Guid userGuid)
		{
			Business.GalleryImage galleryImageController = new Business.GalleryImage();
			return galleryImageController.GetAllGalleryImage(userGuid);
		}

		public static bool Insert(Common.GalleryImage galleryImage)
		{
			Business.GalleryImage galleryImageController = new Business.GalleryImage();
			return galleryImageController.Insert(galleryImage) != Guid.Empty ? true : false;
		}

		public static bool Update(Common.GalleryImage galleryImage)
		{
			Business.GalleryImage galleryImageController = new Business.GalleryImage();
			return galleryImageController.Update(galleryImage);
		}

		public static Common.GalleryImage LoadGalleryImage(Guid galleryImageGuid)
		{
			Business.GalleryImage galleryImageController = new Business.GalleryImage();
			Common.GalleryImage galleryImage = new Common.GalleryImage();
			galleryImageController.Load(galleryImageGuid, galleryImage);
			return galleryImage;
		}

		public static bool Delete(Guid galleryImageGuid)
		{
			Business.GalleryImage galleryImageController = new Business.GalleryImage();
			return galleryImageController.Delete(galleryImageGuid);
		}

		public static bool ActiveGallery(Guid galleryGuid)
		{
			Business.GalleryImage galleryImageController = new Business.GalleryImage();
			return galleryImageController.ActiveGallery(galleryGuid);
		}
	}
}
