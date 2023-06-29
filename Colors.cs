using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataTelephone
{
    [Table("Colors")]
    public class Colors
    {
        public int ProductId { get; set; }
        public string Color { get; set; }
        public Product Product { get; set; }
    }
}
