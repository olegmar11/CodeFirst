using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CodeFirst.DataTables
{
    [Table("Proced")]
    public class Proced
    {
        [Key]
        public int IDproc {  get; set; }
        public string Name { get; set; }
        public int Price {  get; set; }

        public int? Equipment_ID { get; set; }
        public int? Medicine_ID { get; set; }
        public int? Patient_ID { get; set; }
        public int? Staff_ID { get; set; }

        public virtual Equipment Equipment { get; set; }
        public virtual Medicine Medicine { get; set; }
        public virtual Patient Patient { get; set; }
        public virtual Staff Staff { get; set; }
    }

}


