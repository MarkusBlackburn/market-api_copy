using Core.Models.Domain;

namespace Core.Specifications
{
    public class SingleProductSpecification : BaseSpecification<Product>
    {
        public SingleProductSpecification(int id) : base(x => x.Id == id)
        {
            AddInclude(c => c.Categories);
            AddInclude(c => c.Images);
            AddInclude(c => c.Characteristics);
        }
    }
}
