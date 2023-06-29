using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataTelephone
{
    public class ProductSizeRepository : Repository
    {
        public ProductSizeRepository(StoreContext context) : base(context)
        {
        }

        public int Add(List<ProductSize> list)
        {
            context.ProductSizes.AddRange(list);
            return context.SaveChanges();
        }
    }
}
