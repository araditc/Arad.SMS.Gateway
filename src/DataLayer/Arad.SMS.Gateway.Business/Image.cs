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
using System.Text;
using Arad.SMS.Gateway.GeneralLibrary.BaseCore;
using Arad.SMS.Gateway.GeneralLibrary;
using Arad.SMS.Gateway.Common;
using System.Data;

namespace Arad.SMS.Gateway.Business
{
	public class Image : BusinessEntityBase
	{
		public Image(DataAccessBase dataAccessProvider = null)
			: base(TableNames.Images.ToString(), dataAccessProvider) { }

		//public DataTable GetAllImage(Guid userGuid)
		//{
		//	return base.FetchSPDataTable("GetAllImage", "@UserGuid", userGuid);
		//}

		public bool Update(Common.Image image)
		{
			return base.ExecuteSPCommand("Update",
																	 "@Guid", image.ImageGuid,
																	 "@DataGuid", image.DataGuid,
																	 "@Title", image.Title,
																	 "@Description", image.Description,
																	 "@ImagePath", image.ImagePath);
		}

		public DataTable GetImagesOfGallery(Guid galleryImageGuid)
		{
			return base.FetchSPDataTable("GetImagesOfGallery", "@GalleryImageGuid", galleryImageGuid);
		}


		public bool ActiveImage(Guid imageGuid)
		{
			return base.ExecuteSPCommand("Activation", "@Guid", imageGuid);
		}
	}
}
