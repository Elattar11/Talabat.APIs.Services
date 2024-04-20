using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;

namespace Talabat.Core.Specifications.Product_Specs
{
	public class ProductWithBrandsAndCategoriesSpecifications : BaseSpecifications<Product>
	{
        //this constructor will be used for creating an object that will be used to get all products
        public ProductWithBrandsAndCategoriesSpecifications(string sort) : base()
		{
			AddIncludes();

			if (!string.IsNullOrEmpty(sort))
			{
				switch(sort)
				{
					case "priceAsc":
						AddOrderBy(P => P.Price);
						break;

					case "priceDesc":
						AddOrderByDesc(P => P.Price);
						break;

					default:
						AddOrderBy(P => P.Name); 
						break;
				}
			}
			else
			{
				AddOrderBy(P => P.Name);
			}
		}

		//this constructor will be used for creating an object that will be used to get a specific product with id
		public ProductWithBrandsAndCategoriesSpecifications(int id) : base(P => P.Id == id)
		{
			AddIncludes();
		}

		private void AddIncludes()
		{
			Includes.Add(P => P.Brand);
			Includes.Add(P => P.Category);

			
		}

		


	}
}
