﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;
using Talabat.Core.Repositories.Contract;
using Talabat.Repository.Data;

namespace Talabat.Repository
{
	public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
	{
		private readonly StoreContext _dbContext;

		public GenericRepository(StoreContext dbContext) //Ask CLR to create Object from DbContext Implicitly
        {
			_dbContext = dbContext;
		}
        public async Task<IEnumerable<T>> GetAllAsync()
		{
			#region Get Categories and Brands of Products

			//will make inner join with two table [ProductCategories and ProductBrands]
			if (typeof(T) == typeof(Product))
				return (IEnumerable<T>)await _dbContext.Set<Product>()
					.Include(P => P.Brand)
					.Include(P => P.Category)
					.ToListAsync(); 
			#endregion


			return await _dbContext.Set<T>().ToListAsync();
		}

		public async Task<T?> GetAsync(int id)
		{
			if (typeof(T) == typeof(Product))
				return await _dbContext.Set<Product>()
					.Where(p => p.Id == id)
					.Include(P => P.Brand)
					.Include(P => P.Category)
					.FirstOrDefaultAsync() as T;

			//FindAsync ==> Return Object or null [so T must be allow null]
			return await _dbContext.Set<T>().FindAsync(id);
		}
	}
}
