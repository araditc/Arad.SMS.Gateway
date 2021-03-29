using System.Data;
using Common;
using GeneralLibrary;
using GeneralLibrary.BaseCore;

namespace Business
{
	public class UserMessage : BusinessEntityBase
	{
		public UserMessage(DataAccessBase dataAccessProvider = null)
			: base(TableNames.UserMessages.ToString(), dataAccessProvider) { }

		public DataTable GetPagedUserMessages(string domainName, string sortField, int pageNo, int pageSize, ref int resultCount)
		{
			DataSet dataSetUserMessage = base.FetchSPDataSet("GetPagedUserMessages","@DomainName", domainName,
																																							"@PageNo", pageNo,
																																							"@PageSize", pageSize,
																																							"@SortField", sortField);

			resultCount = Helper.GetInt(dataSetUserMessage.Tables[0].Rows[0]["RowCount"]);
			return dataSetUserMessage.Tables[1];
		}
	}
}