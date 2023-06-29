using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataTelephone
{
    public class ColorRepository : Repository
    {
        public ColorRepository(StoreContext context) : base(context)
        {
        }
        public int Add(List<Colors> list)
        {
            context.ProductColors.AddRange(list);
            return context.SaveChanges();
        }
    }
}
