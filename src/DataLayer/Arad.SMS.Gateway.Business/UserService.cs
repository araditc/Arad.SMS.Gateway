using System;
using System.Data;
using GeneralLibrary;

namespace Business
{
	public class UserService : DataAccessLayer.DataAccess
	{
		private Guid guid;
		private decimal price;
		private Guid serviceGuid;
		private Guid userGuid;
		private bool hasError;
		private string errorMessage;
		UserAccess userAccess = new UserAccess();
		Access access = new Access();

		public bool HasError
		{
			get { return hasError; }
			set { hasError = value; }
		}

		public string ErrorMessage
		{
			get { return errorMessage + "<br/>"; }
			set { errorMessage = value; }
		}

		public Guid Guid
		{
			get { return guid; }
			set { guid = value; }
		}

		public decimal Price
		{
			get { return price; }
			set { price = value; }
		}

		public Guid ServiceGuid
		{
			get { return serviceGuid; }
			set { serviceGuid = value; }
		}

		public Guid UserGuid
		{
			get { return userGuid; }
			set { userGuid = value; }
		}

		public UserService()
			: base("UserServices")
		{
			hasError = false;
			errorMessage = "";
		}

		public bool insert(UserService[] userServiceArray)
		{
			DataTable dataTableAccess = new DataTable();
			DataTable dataTableUserService=new DataTable();
			bool result = true;
			bool isDelete = true;

			base.BeginTransaction();

			dataTableUserService = GetUserService();
			if (dataTableUserService.Rows.Count > 0)
			{
				for (int counterUserService = 0; counterUserService < dataTableUserService.Rows.Count; counterUserService++)
				{
					Guid = Helper.GetGuid(dataTableUserService.Rows[counterUserService]["UserServiceGuid"].ToString());
					ServiceGuid = Helper.GetGuid(dataTableUserService.Rows[counterUserService]["ServiceGuid"].ToString());
					UserGuid = Helper.GetGuid(dataTableUserService.Rows[counterUserService]["UserGuid"].ToString());
					if (!Delete())
					{
						isDelete = false;
						break;
					}
				}
			}

			if (isDelete)
			{
				for (int counterUserService = 0; counterUserService < userServiceArray.Length; counterUserService++)
				{
					if (base.Insert("@Price", userServiceArray[counterUserService].Price, "@ServiceGuid", userServiceArray[counterUserService].ServiceGuid, "@UserGuid", UserGuid) == Guid.Empty)
					{
						result = false;
						break;
					}
				}
				if (result)
				{
					access.ServiceGuid = ServiceGuid;
					dataTableAccess = access.GetAccessOfService();
					if (dataTableAccess.Rows.Count > 0)
					{
						for (int counterAccess = 0; counterAccess < dataTableAccess.Rows.Count; counterAccess++)
						{
							userAccess.AccessGuid = Helper.GetGuid(dataTableAccess.Rows[counterAccess]["Guid"]);
							userAccess.UserGuid = UserGuid;
							if (!userAccess.insert())
							{
								result = false;
								break;
							}
						}
					}
				}
				if (result)
				{
					base.CommitTransaction();
					return true;
				}
				else
					return false;
			}
			else
				return false;
			
		}

		public bool Update()
		{
			return base.Update("@Guid", Guid,
												 "@Price", Price,
												 "@ServiceGuid", ServiceGuid,
												 "@UserGuid", UserGuid);
		}

		public DataTable GetUserService()
		{
			return base.SelectSPDataTable("GetUserService", "@UserGuid", UserGuid);
		}

		public bool Delete()
		{
			DataTable dataTableUserAccess = new DataTable();
			bool isDelete=true;

			if (!base.Delete(Guid))
				isDelete = false;

			if (isDelete)
			{
				access.ServiceGuid = ServiceGuid;
				dataTableUserAccess = access.GetAccessOfService();
				if (dataTableUserAccess.Rows.Count > 0)
				{
					for (int counterUserAccess = 0; counterUserAccess < dataTableUserAccess.Rows.Count; counterUserAccess++)
					{
						userAccess.UserGuid = UserGuid;
						userAccess.AccessGuid = Helper.GetGuid(dataTableUserAccess.Rows[counterUserAccess]["Guid"].ToString());
						if (!userAccess.DeleteAccess())
						{
							isDelete = false;
							break;
						}
					}
				}
			}

			if (isDelete)
			{
				return true;
			}
			else
				return false;
		}
	}
}
