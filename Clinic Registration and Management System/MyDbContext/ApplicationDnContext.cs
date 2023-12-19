using Clinic_Registration_and_Management_System.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinic_Registration_and_Management_System.MyDbContext
{
    internal class ApplicationDnContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
             
            optionsBuilder.UseSqlServer("Data Source=DESKTOP-ISH7TNU;Initial Catalog=Clinic Registration;Integrated Security=True;");
        }
        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    //modelBuilder.Entity<Appointment>()
        //    //    .Property(w => w.Appointment_Status)
        //    //    .HasDefaultValue("pending");
        //}
        public  DbSet<Patient> Patients { get; set; }
        public DbSet<Specialization> Specializations { get; set; }
        public DbSet<Appointment> Appointments { get; set; }
    }
}
