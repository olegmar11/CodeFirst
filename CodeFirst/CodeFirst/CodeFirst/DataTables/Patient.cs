using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace CodeFirst.DataTables
{
    [Table("Patient")]
    public class Patient
    {
        [Key]
        public int IDpat {  get; set; }
        [MinLength(1)]
        [MaxLength(20,ErrorMessage="This field cannot exceed limititaion of 20 symbols, or be lover 1 symbol")]
        public string First_Name { get; set; }
        [MinLength(1)]
        [MaxLength(20, ErrorMessage = "This field cannot exceed limititaion of 20 symbols, or be lover 1 symbol")]
        public string Last_Name { get; set; }
        [MinLength(1)]
        [MaxLength(20, ErrorMessage = "This field cannot exceed limititaion of 20 symbols, or be lover 1 symbol")]
        public string Middle_Name { get; set; }
        public int Age { get; set; }
        public string Disease { get; set; }
        public DateTime? Arrival_Date {  get; set; }
        public DateTime? Discharge_Date { get; set; }
        public string Ward {  get; set; }
        [DefaultValue(1)]
        public int? LuckLevel { get; set; }
        public void Display()
        {
            Console.WriteLine("I am displayed");
        }
    }
}
