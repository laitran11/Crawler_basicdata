using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataTelephone
{
    [Table("ProductSize")]
    public class ProductSize
    {
        public int ProductId { get; set; }

        public string Size { get; set; }
        public bool IsSoldOut { get; set; }
        public Product Product { get; set; }
    }
}
