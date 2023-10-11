using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeFirst.DataTables
{
    public class Class1
    {
        [Key]
        public int ID { get; set; }
        public string Name { get; set; }
    }
}
