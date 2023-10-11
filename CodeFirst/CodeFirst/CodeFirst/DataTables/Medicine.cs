using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CodeFirst.DataTables
{
    [Table("Medicine")]
    public class Medicine
    {
        [Key]
        public int IDmed { get; set; }
        [MinLength(1)]
        public string Name { get; set; }
        public string Producer { get; set; }
        public DateTime Created_Date { get; set; }
        public DateTime Expiration_Date { get; set; }
    }
}
