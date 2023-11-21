using HW19.BLL.Contracts;
using HW19.Dal.Contracts.Models;
using HW19.Data.Excepttions;
using HW19.Data.Helpers;
using HW19.Data.Models;

namespace HW19.BLL.Services
{
	public class UserInterfaceService : IUserInterfaceService
	{
		private readonly IUserService _userService;
		private readonly IMessageService _messageService;

		public UserInterfaceService()
		{
			_userService = new UserService();
			_messageService = new MessageService();
		}

		public async Task Run()
		{
			Console.WriteLine("Войти в профиль (нажмите 1)");
			Console.WriteLine("Зарегистрироваться (нажмите 2)");

			switch (Console.ReadLine())
			{
				case "1":
					{
						AunteficateUser();

						break;
					}

				case "2":
					{
						RegisterUser();
						break;
					}
			}
		}

		private void AunteficateUser()
		{
			var authenticationData = new UserAuthenticationData();

			Console.WriteLine("Введите почтовый адрес:");
			authenticationData.Email = Console.ReadLine();

			Console.WriteLine("Введите пароль:");
			authenticationData.Password = Console.ReadLine();

			try
			{
				var user = _userService.Authenticate(authenticationData);

				SuccessMessage.Show("Вы успешно вошли в социальную сеть!");
				SuccessMessage.Show("Добро пожаловать " + user.FirstName);

				ShowUserMenu(user);
			}

			catch (WrongPasswordException)
			{
				AlertMessage.Show("Пароль не корректный!");
			}

			catch (UserNotFoundException)
			{
				AlertMessage.Show("Пользователь не найден!");
			}
		}

		private void RegisterUser()
		{
			var userRegistrationData = new UserRegistrationData();

			Console.WriteLine("Для создания нового профиля введите ваше имя:");
			userRegistrationData.FirstName = Console.ReadLine();

			Console.Write("Ваша фамилия:");
			userRegistrationData.LastName = Console.ReadLine();

			Console.Write("Пароль:");
			userRegistrationData.Password = Console.ReadLine();

			Console.Write("Почтовый адрес:");
			userRegistrationData.Email = Console.ReadLine();

			try
			{
				_userService.Register(userRegistrationData);

				SuccessMessage.Show("Ваш профиль успешно создан. Теперь Вы можете войти в систему под своими учетными данными.");
			}

			catch (ArgumentNullException)
			{
				AlertMessage.Show("Введите корректное значение.");
			}

			catch (Exception)
			{
				AlertMessage.Show("Произошла ошибка при регистрации.");
			}
		}

		private void ShowUserMenu(User user)
		{
			while (true)
			{
				Console.WriteLine("Входящие сообщения: {0}", user.IncomingMessages.Count());
				Console.WriteLine("Исходящие сообщения: {0}", user.OutgoingMessages.Count());
				Console.WriteLine("Мои друзья: {0}", user.Friends.Count());

				Console.WriteLine("Просмотреть информацию о моём профиле (нажмите 1)");
				Console.WriteLine("Редактировать мой профиль (нажмите 2)");
				Console.WriteLine("Добавить в друзья (нажмите 3)");
				Console.WriteLine("Написать сообщение (нажмите 4)");
				Console.WriteLine("Просмотреть входящие сообщения (нажмите 5)");
				Console.WriteLine("Просмотреть исходящие сообщения (нажмите 6)");
				Console.WriteLine("Просмотреть моих друзей (нажмите 7)");
				Console.WriteLine("Выйти из профиля (нажмите 8)");

				string keyValue = Console.ReadLine();

				if (keyValue == "8") break;

				switch (keyValue)
				{
					case "1":
						{
							ShowUserInfo(user);
							break;
						}
					case "2":
						{
							UpdateUserData(user);
							user = _userService.FindById(user.Id);
							break;
						}
					case "3":
						{
							AddFriend(user);
							user = _userService.FindById(user.Id);
							break;
						}
					case "4":
						{
							SendMessage(user);
							user = _userService.FindById(user.Id);
							break;
						}
					case "5":
						{
							CheckIncomingMessages(user.IncomingMessages);
							break;
						}
					case "6":
						{
							OutcomingMessages(user.OutgoingMessages);
							break;
						}
					case "7":
						{
							CheckFriendList(user.Friends);
							break;
						}
				}
			}
		}

		private void ShowUserInfo(User user)
		{
			Console.WriteLine("Информация о моем профиле");
			Console.WriteLine("Мой идентификатор: {0}", user.Id);
			Console.WriteLine("Меня зовут: {0}", user.FirstName);
			Console.WriteLine("Моя фамилия: {0}", user.LastName);
			Console.WriteLine("Мой пароль: {0}", user.Password);
			Console.WriteLine("Мой почтовый адрес: {0}", user.Email);
			Console.WriteLine("Ссылка на моё фото: {0}", user.Photo);
			Console.WriteLine("Мой любимый фильм: {0}", user.FavouriteMovie);
			Console.WriteLine("Моя любимая книга: {0}", user.FavouriteBook);
		}

		private void UpdateUserData(User user)
		{
			Console.Write("Меня зовут:");
			user.FirstName = Console.ReadLine();

			Console.Write("Моя фамилия:");
			user.LastName = Console.ReadLine();

			Console.Write("Ссылка на моё фото:");
			user.Photo = Console.ReadLine();

			Console.Write("Мой любимый фильм:");
			user.FavouriteMovie = Console.ReadLine();

			Console.Write("Моя любимая книга:");
			user.FavouriteBook = Console.ReadLine();

			_userService.Update(user);

			SuccessMessage.Show("Ваш профиль успешно обновлён!");
		}

		private void AddFriend(User user)
		{
			try
			{
				var userAddingFriendData = new UserAddingFriendData();

				Console.WriteLine("Введите почтовый адрес пользователя которого хотите добавить в друзья: ");

				userAddingFriendData.FriendEmail = Console.ReadLine();
				userAddingFriendData.UserId = user.Id;

				_userService.AddFriend(userAddingFriendData);

				SuccessMessage.Show("Вы успешно добавили пользователя в друзья!");
			}

			catch (UserNotFoundException)
			{
				AlertMessage.Show("Пользователя с указанным почтовым адресом не существует!");
			}

			catch (Exception)
			{
				AlertMessage.Show("Произоша ошибка при добавлении пользотваеля в друзья!");
			}
		}

		private void SendMessage(User user)
		{
			var messageSendingData = new MessageSendingData();

			Console.Write("Введите почтовый адрес получателя: ");
			messageSendingData.RecipientEmail = Console.ReadLine();

			Console.WriteLine("Введите сообщение (не больше 5000 символов): ");
			messageSendingData.Content = Console.ReadLine();

			messageSendingData.SenderId = user.Id;

			try
			{
				_messageService.SendMessage(messageSendingData);

				SuccessMessage.Show("Сообщение успешно отправлено!");

				user = _userService.FindById(user.Id);
			}

			catch (UserNotFoundException)
			{
				AlertMessage.Show("Пользователь не найден!");
			}

			catch (ArgumentNullException)
			{
				AlertMessage.Show("Введите корректное значение!");
			}

			catch (Exception)
			{
				AlertMessage.Show("Произошла ошибка при отправке сообщения!");
			}

		}

		private void CheckIncomingMessages(IEnumerable<Message> incomingMessages)
		{
			Console.WriteLine("Входящие сообщения");


			if (incomingMessages.Count() == 0)
			{
				Console.WriteLine("Входящих сообщения нет");
				return;
			}

			incomingMessages.ToList().ForEach(message =>
			{
				Console.WriteLine("От кого: {0}. Текст сообщения: {1}", message.SenderEmail, message.Content);
			});
		}

		private void OutcomingMessages(IEnumerable<Message> outcomingMessages)
		{
			Console.WriteLine("Исходящие сообщения");

			if (outcomingMessages.Count() == 0)
			{
				Console.WriteLine("Исходящих сообщений нет");
				return;
			}

			outcomingMessages.ToList().ForEach(message =>
			{
				Console.WriteLine("Кому: {0}. Текст сообщения: {1}", message.RecipientEmail, message.Content);
			});
		}

		private void CheckFriendList(IEnumerable<User> friends)
		{
			Console.WriteLine("Мои друзья");


			if (friends.Count() == 0)
			{
				Console.WriteLine("У вас нет друзей");
				return;
			}

			friends.ToList().ForEach(friend =>
			{
				Console.WriteLine("Почтовый адрес друга: {0}. Имя друга: {1}. Фамилия друга: {2}", friend.Email, friend.FirstName, friend.LastName);
			});
		}
	}
}
