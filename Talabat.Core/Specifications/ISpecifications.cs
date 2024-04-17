using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;

namespace Talabat.Core.Specifications
{
	public interface ISpecifications<T> where T : BaseEntity
	{
		//First Spec [Where]
        public Expression<Func<T , bool>> Criteria { get; set; } //el variable elly haeshel al value elly haeb3tha lel where [p => p.Id ==1]

		//Second Spec [Include]
		public List<Expression<Func<T, object>>> Includes { get; set; }



    }
}
