using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TestSmth2.Models.Domain
{
    public class Patient
    {
        [Key]
        public string PatientCNP { get; set; }
        [Column]
        public required string PatientFirstName { get; set; }
        [Column]
        public string PatientLastName { get; set; }
        [Column]
        public ICollection<Entry> Entries { get; set; }
    }
}
