using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataTelephone
{
    public class ProductRepository : Repository
    {
        public ProductRepository(StoreContext context) : base(context)
        {
        }
        public int Add(Product obj)
        {
            context.Products.Add(obj);
            return context.SaveChanges();
        }
        public int Add(List<Product> list)
        {
            context.Products.AddRange(list);
            return context.SaveChanges();
        }
    }
}
