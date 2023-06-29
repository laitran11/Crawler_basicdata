using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataTelephone
{
    [Table("Product")]
    public class Product
    {
        [Column("ProductId")]
        public int Id { get; set; }
        public byte CategoryId { get; set; }
        [Column("ProductName")]
        public string Name { get; set; }
        //public string Color { get; set; }
        [Column("ProductCode")]
        public string Code { get; set; }
        public int minPrice { get; set; }
        public Category Category { get; set; }
        public int Bonus { get; set; }
        public List<ProductSize> ProductSizes { get; set; }
        public List<ProductImage> ProductImages { get; set; }
        public List<Colors> ProductColors { get; set; }

    }
}
