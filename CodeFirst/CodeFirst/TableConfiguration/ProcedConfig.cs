using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.ModelConfiguration;
using CodeFirst.DataTables;
using Microsoft.EntityFrameworkCore;

namespace CodeFirst.TableConfiguration
{
    public class ProcedConfig : EntityTypeConfiguration<Proced>
    {
        public ProcedConfig() 
        {
            this.HasOptional(p => p.Equipment)
             .WithMany()
             .HasForeignKey(p => p.Equipment_ID);

            this.HasOptional(p => p.Medicine)
             .WithMany()
             .HasForeignKey(p => p.Medicine_ID);

            this.HasOptional(p => p.Patient)
             .WithMany()
             .HasForeignKey(p => p.Patient_ID);

            this.HasOptional(p => p.Staff)
             .WithMany()
             .HasForeignKey(p => p.Staff_ID);
        }
    }
}
