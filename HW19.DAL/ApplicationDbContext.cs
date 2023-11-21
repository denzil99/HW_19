using HW19.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace HW19.DAL
{
	public class ApplicationDbContext : DbContext
	{
		public DbSet<User> Users { get; set; }
		public DbSet<Friend> Friends { get; set; }
		public DbSet<Message> Messages { get; set; }

		public ApplicationDbContext()
		{
			Database.EnsureCreated();
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<User>().ToTable("users");
			modelBuilder.Entity<Friend>().ToTable("friend");
			modelBuilder.Entity<Message>().ToTable("messages");


		}
	}
}
