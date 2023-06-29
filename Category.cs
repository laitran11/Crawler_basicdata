using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataTelephone
{
    [Table("Category")]
    public class Category
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("CategoryId")]
        public byte Id { get; set; }
        [Column("CategoryName")]
        public string Name { get; set; }
        [NotMapped]
        public string Url { get; set; }
        public byte? ParentId { get; set; }
        public List<Product> Products { get; set; }
    }
}
