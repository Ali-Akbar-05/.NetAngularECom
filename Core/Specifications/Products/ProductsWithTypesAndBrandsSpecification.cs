using Core.Entities;

namespace Core.Specifications.Products
{
    public class ProductsWithTypesAndBrandsSpecification : BaseSpecification<Product>
    {
        public ProductsWithTypesAndBrandsSpecification(ProductSpecParams productParams)
         : base(w =>
           (string.IsNullOrEmpty(productParams.Search) ||w.Name.ToLower().Contains(productParams.Search)) &&
           (!productParams.brandId.HasValue || w.ProductBrandId == productParams.brandId.Value) &&
           (!productParams.typeId.HasValue || w.ProductTypeId == productParams.typeId.Value)
          )
        {
            AddInclude(x => x.ProductType);
            AddInclude(x => x.ProductBrand);
            AddOrderBy(x => x.Name);
            
            ApplyPaging(productParams.PageSize*(productParams.PageIndex-1),productParams.PageSize);

            if (!string.IsNullOrEmpty(productParams.sort))
            {
                switch (productParams.sort)
                {
                    case "priceAsc":
                        AddOrderBy(p => p.Price);
                        break;
                    case "priceDesc":
                        AddOrderByDescending(p => p.Price);
                        break;
                    default:
                        AddOrderBy(b => b.Name);
                        break;
                }
            }
        }

        public ProductsWithTypesAndBrandsSpecification(int id) : base(x => x.Id == id)
        {
            AddInclude(x => x.ProductType);
            AddInclude(x => x.ProductBrand);
        }
    }
}