using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HW19.Data.Models
{
	public class User
	{
		[Column("id")]
		public int Id { get; set; }
		[Column("first_name")]
		public string FirstName { get; set; }
		[Column("last_name")]
		public string LastName { get; set; }
		[Column("password")]
		public string Password { get; set; }
		[Column("email")]
		public string Email { get; set; }
		[Column("photo")]
		public string Photo { get; set; }
		[Column("favourite_movie")]
		public string FavouriteMovie { get; set; }
		[Column("favourite_book")]
		public string FavouriteBook { get; set; }
	}
}
