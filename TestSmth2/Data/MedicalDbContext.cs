using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using TestSmth2.Models.Domain;

namespace TestSmth2.Data
{
    public class MedicalDbContext : DbContext
    {
        public MedicalDbContext(DbContextOptions<MedicalDbContext> options) : base(options)
        {
        }

        public DbSet<Entry> Entries { get; set; }
        public DbSet<AntiBiotic> AntiBiotics { get; set; }
        public DbSet<Patient> Patients { get; set; }
    }
}
