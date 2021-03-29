using SqlLibrary;
using System.ServiceModel;
using System.ServiceModel.MsmqIntegration;

namespace ApiProcessRequest
{
	[ServiceContract()]
	[ServiceKnownType(typeof(BatchMessage))]
	public interface IMessageService
	{
		[OperationContract(IsOneWay = true, Action = "*")]
		void ProcessMessage(MsmqMessage<BatchMessage> batchMessage);
	}
}
