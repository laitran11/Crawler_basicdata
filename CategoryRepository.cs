using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataTelephone
{
    internal class CategoryRepository : Repository
    {
        public CategoryRepository(StoreContext context) : base(context)
        {
        }

        public int Add(List<Category> list)
        {
            context.Categories.AddRange(list);
            return context.SaveChanges();
        }

    }
}
