using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GeneralLibrary.BaseCore;
using System.Data;

namespace Facade
{
	public class EmailTemplate : FacadeEntityBase
	{
		public static Guid Insert(Common.EmailTemplate EmailTemplate)
		{
			Business.EmailTemplate emailTemplateController = new Business.EmailTemplate();
			return emailTemplateController.InsertTemplate(EmailTemplate);
		}

		public static DataTable GetPagedEmailTemplates(Guid userGuid, string sortField, int pageNo, int pageSize, ref int resultCount)
		{
			Business.EmailTemplate emailTemplateController = new Business.EmailTemplate();
			return emailTemplateController.GetPagedEmailTemplates(userGuid, sortField, pageNo, pageSize, ref resultCount);
		}

		public static bool DeleteEmailTemplate(Guid emailTemplateGuid)
		{
			Business.EmailTemplate emailTemplateController = new Business.EmailTemplate();
			return emailTemplateController.Delete(emailTemplateGuid);
		}

		public static Common.EmailTemplate LoadTemplate(Guid emailTemplateGuid)
		{
			Business.EmailTemplate emailTemplateController = new Business.EmailTemplate();
			Common.EmailTemplate emailTemplate = new Common.EmailTemplate();
			emailTemplateController.Load(emailTemplateGuid, emailTemplate);
			return emailTemplate;
		}

		public static bool UpdateTemplate(Common.EmailTemplate emailTemplate)
		{
			Business.EmailTemplate emailTemplateController = new Business.EmailTemplate();
			return emailTemplateController.UpdateTemplate(emailTemplate);
		}

		public static bool InsertTemplate(Common.EmailTemplate emailTemplate)
		{
			Business.EmailTemplate emailTemplateController = new Business.EmailTemplate();
			return emailTemplateController.InsertTemplate(emailTemplate) != Guid.Empty;
		}
	}
}