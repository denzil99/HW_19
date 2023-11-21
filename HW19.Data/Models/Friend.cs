using System.ComponentModel.DataAnnotations.Schema;

namespace HW19.Data.Models
{
	public class Friend
	{
		[Column("id")]
		public int Id { get; set; }
		[Column("user_id")]
		public int UserId { get; set; }
		[Column("Friend_id")]
		public int FriendtId { get; set; }
	}
}
