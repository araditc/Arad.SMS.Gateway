using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApi
{
	public class IpNotValidException : Exception
	{
		public IpNotValidException()
		{
			throw new ArgumentNullException("Message", "Ip Not Valid!");
		}
	}
}
