using HW19.DAL.Entities;

namespace HW19.DAL.Contracts.Interfaces
{
	public interface IFriendRepository
	{
		int Create(FriendEntity friendEntity);
		IEnumerable<FriendEntity> FindAllByUserId(int userId);
		int Delete(int id);
	}
}
