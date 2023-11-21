using HW19.Dal.Contracts.Models;
using HW19.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HW19.BLL.Contracts
{
	public interface IMessageService
	{
		IEnumerable<Message> GetIncomingMessagesByUserId(int recipientId);
		IEnumerable<Message> GetOutcomingMessagesByUserId(int senderId);
		void SendMessage(MessageSendingData messageSendingData);
	}
}
