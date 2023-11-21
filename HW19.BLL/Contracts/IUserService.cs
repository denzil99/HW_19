using HW19.Dal.Contracts.Models;
using HW19.DAL.Entities;
using HW19.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HW19.BLL.Contracts
{
	public interface IUserService
	{
		void Register(UserRegistrationData userRegistrationData);
		User Authenticate(UserAuthenticationData userAuthenticationData);
		User FindByEmail(string email);
		User FindById(int id);
		void Update(User user);
		IEnumerable<User> GetFriendsByUserId(int userId);
		void AddFriend(UserAddingFriendData userAddingFriendData);
	}
}
