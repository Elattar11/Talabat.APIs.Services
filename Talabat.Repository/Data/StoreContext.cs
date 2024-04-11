using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;

namespace Talabat.Repository.Data
{
	public class StoreContext : DbContext
	{
		//Allow DI
        public StoreContext(DbContextOptions<StoreContext> options) : base(options)
        {

                
        }

		//Must add the package of migration at [Talabat.APIs] project 
		//becouse it is include the connection string 

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			//Apply all configurations from Assemply 
			modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
		}

		//Must Add Project Reference for [Talabat.Core] 
		#region Tables of Database
		public DbSet<Product> Products { get; set; }
        public DbSet<ProductBrand> ProductBrands { get; set; }
        public DbSet<ProductCategory> ProductCategories { get; set; } 
        #endregion

    }
}
