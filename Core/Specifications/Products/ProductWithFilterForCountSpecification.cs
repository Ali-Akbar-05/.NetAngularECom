using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;

namespace Core.Specifications.Products
{
    public class ProductWithFilterForCountSpecification : BaseSpecification<Product>
    {
        public ProductWithFilterForCountSpecification(ProductSpecParams productParams)
       : base(w =>
           (string.IsNullOrEmpty(productParams.Search) || w.Name.ToLower().Contains(productParams.Search)) &&
           (!productParams.brandId.HasValue || w.ProductBrandId == productParams.brandId.Value) &&
           (!productParams.typeId.HasValue || w.ProductTypeId == productParams.typeId.Value)
          )
        {
        }
    }
}