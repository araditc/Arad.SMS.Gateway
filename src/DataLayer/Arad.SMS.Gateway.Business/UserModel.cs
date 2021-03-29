using DataAccessLayer;
using DatabaseInfo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModel;

namespace Business
{
	public class UserModel : SqlDataProvider<IRegisterUserView, Guid>//BusinessEntityBase
	{
		public UserModel(IDatabaseInfoProvider dbInfo = null)
			: base(dbInfo)
		{
		}

		public void RegisterUser(IRegisterUserView user)
		{
			AddItem(user);
		}
	}
}
