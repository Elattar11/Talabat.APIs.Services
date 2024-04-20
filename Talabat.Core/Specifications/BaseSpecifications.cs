﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;

namespace Talabat.Core.Specifications
{
	public class BaseSpecifications<T> : ISpecifications<T> where T : BaseEntity
	{
        public Expression<Func<T, bool>> Criteria { get; set; } = null;
        public List<Expression<Func<T, object>>> Includes { get; set; } = new List<Expression<Func<T, object>>>();
		public Expression<Func<T, object>> OrderBy { get; set; }
		public Expression<Func<T, object>> OrderByDesc { get; set; }

		public BaseSpecifications()
        {
            //Criteria = null;

        }

        public BaseSpecifications(Expression<Func<T, bool>> criteriaExpression)
        {
            Criteria = criteriaExpression; //P => P.Id = 10

		}

        public void AddOrderBy(Expression<Func<T ,object>> orderByExpression)
        {
            OrderBy = orderByExpression;
        }

		public void AddOrderByDesc(Expression<Func<T, object>> orderByDesExpression)
		{
			OrderByDesc = orderByDesExpression;
		}


	}
}
