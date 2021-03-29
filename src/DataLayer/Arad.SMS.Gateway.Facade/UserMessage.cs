using System;
using System.Data;
using GeneralLibrary.BaseCore;

namespace Facade
{
	public class UserMessage : FacadeEntityBase
	{
		public static bool Insert(Common.UserMessage userMessage)
		{
			Business.UserMessage userMessageController = new Business.UserMessage();
			return userMessageController.Insert(userMessage) != Guid.Empty ? true : false;
		}

		public static DataTable GetPagedUserMessages(string domainName, string sortField, int pageNo, int pageSize, ref int resultCount)
		{
			Business.UserMessage userMessageController = new Business.UserMessage();
			return userMessageController.GetPagedUserMessages(domainName, sortField, pageNo, pageSize, ref resultCount);
		}

		public static bool UpdateUserMessage(Common.UserMessage userMessage)
		{
			Business.UserMessage userMessageController = new Business.UserMessage();
			return userMessageController.Update(userMessage);
		}

		public static bool DeleteUserMessage(Guid userMessageGuid)
		{
			Business.UserMessage userMessageController = new Business.UserMessage();
			return userMessageController.Delete(userMessageGuid);
		}

		public static Common.UserMessage LoadUserMessage(Guid userMessageGuid)
		{
			Business.UserMessage userMessageController = new Business.UserMessage();
			Common.UserMessage userMessage = new Common.UserMessage();
			userMessageController.Load(userMessageGuid, userMessage);
			return userMessage;
		}
	}
}
