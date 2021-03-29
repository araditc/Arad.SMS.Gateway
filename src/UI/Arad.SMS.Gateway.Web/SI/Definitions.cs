using System;
using System.Runtime.Serialization;

namespace MessagingSystem.SI
{


	public enum MessageType
	{
		Flash = 0,
		Normal = 1,
		Simcard = 2,
		ApplicationSms = 3
	}


	public enum ErrorMessage
	{
		RecipientNumberIsInvalid = 1,
		SenderNumberIsInvalid = 2,
		EncodingIsInvalid = 3,
		MessageClassIsInvalid = 4,
		UdhIsInvalid = 6,
		MessageIsEmpty = 13,
		CreditIsLow = 14,
	}


	public enum Encoding
	{
		Default = 1,
		Utf8 = 2,
		Data = 5,
		Binary = 6
	}


	public enum SmsStatus
	{
		Waiting = 1,
		Received = 2,
		NotReceived = 3
	}
}
