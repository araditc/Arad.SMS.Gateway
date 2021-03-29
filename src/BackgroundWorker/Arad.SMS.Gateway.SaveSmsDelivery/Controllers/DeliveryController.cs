using Common;
using GeneralLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Messaging;
using System.Runtime.Serialization.Formatters;

namespace SaveSmsDelivery
{
	public class DeliveryController
	{
		public void SaveDeliveryStatus(object sender, EventArgs e)
		{
			try
			{
				MessageQueue queue;
				DeliveryMessage delivery;
				DeliveryMessage oldStatusDelivery;
				List<DeliveryMessage> lstDelivery = new List<DeliveryMessage>();

				string queuePath = string.Format(@"185.37.53.188\private$\{0}", ConfigurationManager.GetSetting("QueueName"));

				if (!MessageQueue.Exists(queuePath))
					return;

				int counter = 0;
				int capacity = 10000;
				var messages = new List<Message>();
				queue = new MessageQueue(queuePath);
				queue.Formatter = new BinaryMessageFormatter();
				var msgEnumerator = queue.GetMessageEnumerator2();
				while (msgEnumerator.MoveNext(new TimeSpan(0, 0, 1)))
				{
					using (var msg = msgEnumerator.Current)
					{
						messages.Add(msg);
						counter++;
						if (counter == capacity)
							break;
					}
				}

				foreach (Message msg in messages)
				{
					delivery = new DeliveryMessage();
					msg.Formatter = new BinaryMessageFormatter(FormatterAssemblyStyle.Full, FormatterTypeStyle.TypesAlways);
					delivery = (DeliveryMessage)msg.Body;
					delivery.MessageId = msg.Id;

					switch (delivery.Agent)
					{
						case (int)SmsSenderAgentReference.SLS:
						case (int)SmsSenderAgentReference.RahyabRG:
							if (lstDelivery.Where(item =>
																		item.Agent == delivery.Agent &&
																		item.BatchId == delivery.BatchId &&
																		item.Mobile == delivery.Mobile &&
																		item.Date <= delivery.Date &&
																		item.Time < delivery.Time).Count() > 0)
							{
								oldStatusDelivery = lstDelivery.Where(item =>
																											item.Agent == delivery.Agent &&
																											item.BatchId == delivery.BatchId &&
																											item.Mobile == delivery.Mobile &&
																											item.Date <= delivery.Date &&
																											item.Time < delivery.Time).First();

								lstDelivery.Remove(oldStatusDelivery);
								queue.ReceiveById(oldStatusDelivery.MessageId);
							}
							break;
						default:
							if (lstDelivery.Where(item =>
																		item.Agent == delivery.Agent &&
																		item.ReturnId == delivery.ReturnId &&
																		item.Date <= delivery.Date &&
																		item.Time < delivery.Time).Count() > 0)
							{
								oldStatusDelivery = lstDelivery.Where(item =>
																											item.Agent == delivery.Agent &&
																											item.ReturnId == delivery.ReturnId &&
																											item.Date <= delivery.Date &&
																											item.Time < delivery.Time).First();

								lstDelivery.Remove(oldStatusDelivery);
								queue.ReceiveById(oldStatusDelivery.MessageId);
							}
							break;
					}

					lstDelivery.Add(delivery);
				}

				if (Facade.OutboxNumber.UpdateDeliveryStatus(lstDelivery))
				{
					foreach (DeliveryMessage d in lstDelivery)
						queue.ReceiveById(d.MessageId);
				}

				lstDelivery.Clear();
				messages.Clear();
			}
			catch (Exception ex)
			{
				LogController<ServiceLogs>.LogInFile(ServiceLogs.SaveDelivery, string.Format("On SaveDeliveryStatus : {0}*{1}", ex.Message, ex.StackTrace));
				throw ex;
			}
		}
	}
}
