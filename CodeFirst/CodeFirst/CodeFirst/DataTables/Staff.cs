using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CodeFirst.DataTables
{
    [Table("Staff")]
    public class Staff
    {
        [Key]
        public int IDstaff {  get; set; }
        [MinLength(1)]
        public string Name { get; set; }
        [MinLength(1)]
        public string Last_Name { get; set; }
        [MinLength(1)]
        public string Middle_Name { get; set; }
        public int Age { get; set; }
        [MinLength(1)]
        public string Position { get; set; }
    }
}
