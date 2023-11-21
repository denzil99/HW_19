using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HW19.Data.Models
{
	public class Message
	{
		[Column("id")]
		public int Id { get; set; }
		[Column("content")]
		public string Content { get; set; }
		[Column("sender_id")]
		public string SenderId { get; set; }
		[Column("reciept_id")]
		public string RecieptId { get; set; }
	}
}
