using GeneralLibrary;
using GeneralLibrary.BaseCore;
using System;
using System.Collections.Generic;
using System.Data;

namespace Facade
{
	public class BlackList : FacadeEntityBase
	{
		public static DataTable GetPagedNumbers(string query, int pageNo, int pageSize, string sortField, ref int resultCount)
		{
			Business.BlackList blackListController = new Business.BlackList();
			return blackListController.GetPagedNumbers(query, pageNo, pageSize, sortField, ref resultCount);
		}

		public static bool InsertListNumber(List<string> lstNumbers)
		{
			Business.BlackList blackListController = new Business.BlackList();
			DataTable dtNumbers = new DataTable();
			dtNumbers.Columns.Add("Mobile", typeof(string));
			DataRow row;
			foreach (string number in lstNumbers)
			{
				row = dtNumbers.NewRow();

				if (Helper.IsCellPhone(number) > 0)
					row["Mobile"] = Helper.GetLocalMobileNumber(number);

				dtNumbers.Rows.Add(row);
			}

			return blackListController.InsertListNumber(dtNumbers);
		}

		public static bool DeleteNumber(Guid guid)
		{
			Business.BlackList blackListController = new Business.BlackList();
			return blackListController.Delete(guid);
		}
	}
}
