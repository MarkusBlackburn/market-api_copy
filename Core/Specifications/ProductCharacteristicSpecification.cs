using Core.Models.Domain;

namespace Core.Specifications
{
    public class ProductCharacteristicSpecification : BaseSpecification<ProductCharacteristic>
    {
        public ProductCharacteristicSpecification(int id) : base(x =>x.Id == id)
        {
            AddInclude(c => c.Product!); 
        }
    }
}
