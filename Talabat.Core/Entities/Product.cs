using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Talabat.Core.Entities
{
	public class Product : BaseEntity
	{
        #region Properties
        public string Name { get; set; }
        public string Description { get; set; }
        public string PictureUrl { get; set; }
        public decimal Price { get; set; } 
        #endregion

        #region Product Brand Relation

        //[ForeignKey(nameof(Product.Brand))]
        public int BrandId { get; set; } //FK Column => ProductBrands

        public ProductBrand Brand { get; set; } //Navigational Property [One] 
        #endregion

        #region Product Category Relation 

        public int CategoryId { get; set; } //FK Columg => Product Category
        public ProductCategory Category { get; set; } // Navigational Property [One]
        #endregion

    }
}
