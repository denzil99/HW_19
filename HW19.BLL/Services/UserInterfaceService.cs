using HW19.BLL.Contracts;

namespace HW19.BLL.Services
{
	public class UserInterfaceService
	{
		private readonly IUserService _userService;
		private readonly IMessageService _messageService;

		public UserInterfaceService()
		{
			_userService = new UserService();
			_messageService = new MessageService();

		}



	}
}
