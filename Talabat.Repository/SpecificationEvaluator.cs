using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;
using Talabat.Core.Specifications;

namespace Talabat.Repository
{
	internal static class SpecificationEvaluator<TEntity> where TEntity : BaseEntity
	{
		public static IQueryable<TEntity> GetQuery(IQueryable<TEntity> inputQuery , ISpecifications<TEntity> specification)
		{
			var query = inputQuery; // _dbContext.Set<Product>()

			if (specification.Criteria is not null) 
				query = query.Where(specification.Criteria);

			//query = _dbContext.Set<Product>().Where(P => P.Id = 10)

			//Includes
			query = specification.Includes.Aggregate(query, (currentQuery, includeExpression) => currentQuery.Include(includeExpression));
			//_dbContext.Set<Product>().Where(P => P.Id = 10).Include(P => P.Brand)
			//_dbContext.Set<Product>().Where(P => P.Id = 10).Include(P => P.Brand).Include(P => P.Category)



			return query;
		}
	}
}
