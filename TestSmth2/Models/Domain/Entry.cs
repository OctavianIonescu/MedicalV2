using System.ComponentModel.DataAnnotations.Schema;

namespace TestSmth2.Models.Domain
{
    public class Entry
    {
        [Column]
        public Guid ID { get; set; }
        [Column]
        public string entryCode { get; set; }
        [ForeignKey(nameof(Patient))]
        public string PatientCNP { get; set; }
        public Patient Patient { get; set; }
        [Column]
        public string PathologicProduct { get; set; }
        [Column]
        public string Microbe { get; set; }
        [Column]
        public string sectiaDeProvenienta { get; set; }
        [Column]
        public string? shortDescription { get; set; }
        [Column]
        public string medicSolicitant { get; set; }
        [Column]
        public string URLHandle { get; set; }
        [Column]
        public string? FileURL { get; set; }
        [Column]
        public DateTime collectionDate { get; set; }
        [Column]
        public DateTime validationDate { get; set; }
        [Column]
        public ICollection<AntiBiotic> Tags { get; set; }
        public ICollection<ResistanceMechanism> Resistance { get; set; }

        public Entry() { }

    }
}
