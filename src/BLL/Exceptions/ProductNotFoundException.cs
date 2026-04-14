using BLL.Exceptions;

namespace BLL.Exceptions
{
    public class ProductNotFoundException : EntityNotFoundException
    {
        public ProductNotFoundException(int id)
            : base($"Product {id} not found.") { }

        public ProductNotFoundException(string barcode)
            : base($"No product found with barcode '{barcode}'.") { }
    }
}
