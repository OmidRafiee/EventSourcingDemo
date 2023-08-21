using System;
using EventSourcingDemo.Domain;
using Microsoft.EntityFrameworkCore;

namespace EventSourcingDemo.Data
{
	public class AppDbContext : DbContext
	{
		public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
		{
		}
		
		
		public DbSet<ProductEntity> Products { get; set; }
	}
}