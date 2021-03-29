using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GeneralLibrary.BaseCore;
using System.Data;

namespace Facade
{
	public class GroupTemplate :FacadeEntityBase
	{
		public static bool Insert(Common.GroupTemplate groupTemplate)
		{
			Business.GroupTemplate groupTemplateController = new Business.GroupTemplate();
			return groupTemplateController.InsertGroupTemplate(groupTemplate);
		}

		public static DataTable GetPagedGroupTemplates(string title, Guid userGuid, string sortField, int pageNo, int pageSize, ref int resultCount)
		{
			Business.GroupTemplate groupTemplateController = new Business.GroupTemplate();
			return groupTemplateController.GetPagedGroupTemplates(title, userGuid, sortField, pageNo, pageSize, ref resultCount);
		}

		public static bool UpdateGroupTemplate(Common.GroupTemplate groupTemplate)
		{
			Business.GroupTemplate groupTemplateController = new Business.GroupTemplate();
			return groupTemplateController.UpdateGroupTemplate(groupTemplate);
		}

		public static bool DeleteGroupTemplate(Guid groupTemplateGuid)
		{
			Business.GroupTemplate groupTemplateController = new Business.GroupTemplate();
			return groupTemplateController.Delete(groupTemplateGuid);
		}

		public static Common.GroupTemplate LoadGroupTemplate(Guid groupTemplateGuid)
		{
			Business.GroupTemplate groupTemplateController = new Business.GroupTemplate();
			Common.GroupTemplate groupTemplate = new Common.GroupTemplate();
			groupTemplateController.Load(groupTemplateGuid, groupTemplate);
			return groupTemplate;
		}
	}
}
