using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataTelephone
{
    [Table("ProductImage")]
    public class ProductImage
    {
        public int ProductId { get; set; }
        public string ImageUrl { get; set; }
        public Product Product { get; set; }
    }
}
