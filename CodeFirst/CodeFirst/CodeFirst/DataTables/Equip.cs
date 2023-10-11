using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace CodeFirst.DataTables
{
    [Table("Equipment")]
    public class Equipment
    {
        [Key]
        public int IDeq { get; set; }
        [MinLength(1)]
        public string Name { get; set; }
        public int Quantity { get; set; }
        [MinLength(1)]
        public string Manufacturer { get; set; }
    }
}
