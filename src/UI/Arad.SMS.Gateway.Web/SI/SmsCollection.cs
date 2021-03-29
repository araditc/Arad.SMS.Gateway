using System;
using System.Collections.ObjectModel;
using System.Runtime.Serialization;

namespace MessagingSystem.SI
{
	[DataContract]
	public class SmsCollection : Collection<Sms>
	{
		public SmsCollection()
		{
			
		}		
	}
}