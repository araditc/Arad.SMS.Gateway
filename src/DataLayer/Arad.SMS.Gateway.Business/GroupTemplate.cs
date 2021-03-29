using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GeneralLibrary;
using GeneralLibrary.BaseCore;
using System.Data;
using Common;

namespace Business
{
	public class GroupTemplate : BusinessEntityBase
	{
		public GroupTemplate(DataAccessBase dataAccessProvider = null)
			: base(TableNames.GroupTemplates.ToString(), dataAccessProvider) { }

		public DataTable GetPagedGroupTemplates(string title, Guid userGuid, string sortField, int pageNo, int pageSize, ref int resultCount)
		{
			DataSet dataSetGroupTemplates = base.FetchSPDataSet("GetPagedGroupTemplates", "@Title", title,
																																										"@UserGuid", userGuid,
																																										"@PageNo", pageNo,
																																										"@PageSize", pageSize,
																																										"@SortField", sortField);

			resultCount = Helper.GetInt(dataSetGroupTemplates.Tables[0].Rows[0]["RowCount"]);
			return dataSetGroupTemplates.Tables[1];
		}

		public bool UpdateGroupTemplate(Common.GroupTemplate groupTemplate)
		{
			try
			{
				DataTable dataTableGroupTemplate = base.FetchDataTable(@"SELECT COUNT(*) AS [Count] FROM [GroupTemplates]
																																 WHERE [Title] = @Title AND [Guid] != @Guid AND [UserGuid] = @UserGuid", "@Title", groupTemplate.Title, "@Guid", groupTemplate.GroupTemplateGuid, "@UserGuid", groupTemplate.UserGuid);
				if (Helper.GetInt(dataTableGroupTemplate.Rows[0]["Count"]) == 0)
					return base.ExecuteSPCommand("UpdateGroupTemplate", "Guid", groupTemplate.GroupTemplateGuid,
																															"Title", groupTemplate.Title);
				else
					throw new Exception(Language.GetString("DuplicateTitleGroupTemplate"));
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		public bool InsertGroupTemplate(Common.GroupTemplate groupTemplate)
		{
			try
			{
				DataTable dataTableGroupTemplate = base.FetchDataTable("SELECT COUNT(*) AS [Count] FROM [GroupTemplates] WHERE [Title] = @Title AND [UserGuid] = @UserGuid", "@Title", groupTemplate.Title, "@UserGuid", groupTemplate.UserGuid);
				if (Helper.GetInt(dataTableGroupTemplate.Rows[0]["Count"]) == 0)
					return base.Insert(groupTemplate) != Guid.Empty ? true : false;
				else
					throw new Exception(Language.GetString("DuplicateTitleGroupTemplate"));
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}
	}
}
