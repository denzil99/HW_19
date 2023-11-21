using HW19.DAL.Entities;

namespace HW19.DAL.Contracts.Interfaces
{
	public interface IMessageRepository
	{
		int Create(MessageEntity messageEntity);
		IEnumerable<MessageEntity> FindBySenderId(int senderId);
		IEnumerable<MessageEntity> FindByRecipientId(int recipientId);
		int DeleteById(int messageId);
	}
}
