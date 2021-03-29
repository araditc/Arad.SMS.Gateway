using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common;

namespace GetSmsDelivery
{
	public static class SmsServiceFactory
	{
		public static ISmsServiceManager GetSmsServiceManager(SmsSenderAgentReference agent)
		{
			switch (agent)
			{
				case SmsSenderAgentReference.Magfa:
					return new MagfaSmsServiceManager();

				case SmsSenderAgentReference.RahyabRG:
					return new RahyabRGSmsServiceManager();

				case SmsSenderAgentReference.RahyabPG:
					return new RahyabPGSmsServiceManager();
			}

			throw new Exception("Sms Sender Agent Not Defined!!!");
		}
	}
}
