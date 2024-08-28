using Core.Models.Domain;

namespace Core.Specifications
{
    public class ProductSpecification : BaseSpecification<Product>
    {
        public ProductSpecification(ProductSpecParams specParams) : base(x =>
            (!specParams.Names.Any() || specParams.Names.Contains(x.Name)) &&
            (!specParams.Codes.Any() || (specParams.Codes.Contains(Convert.ToString(x.InternalCode)))) &&
            (string.IsNullOrEmpty(specParams.Search) || x.Description.ToLower() .Contains(specParams.Search)))
        {
            switch (specParams.Sort)
            {
                case "priceAsc":
                    AddOrderBy(x => x.Price);
                    break;

                case "priceDesc":
                    AddOrderByDescending(x => x.Price);
                    break;

                case "codeAsc":
                    AddOrderBy(x => x.InternalCode);
                    break;

                case "codeDesc":
                    AddOrderByDescending(x => x.InternalCode);
                    break;

                case "saleAsc":
                    AddOrderBy(x => x.Discount);
                    break;

                case "saleDesc":
                    AddOrderByDescending(x => x.Discount);
                    break;

                default:
                    AddOrderBy(x => x.Name);
                    break;
            }

            AddInclude(c => c.Categories);
            AddInclude(c => c.Characteristics);
            AddInclude(c => c.Images);

            ApplyPaging(specParams.PageSize * (specParams.PageIndex -1), specParams.PageSize);
        }
    }
}
