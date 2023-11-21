using System.ComponentModel.DataAnnotations.Schema;

namespace HW19.Data.Models
{
	public class Message
	{
		[Column("id")]
		public int Id { get; set; }
		[Column("content")]
		public string Content { get; set; }
		[Column("sender_id")]
		public int SenderId { get; set; }
		[Column("reciept_id")]
		public int RecieptId { get; set; }
	}
}
