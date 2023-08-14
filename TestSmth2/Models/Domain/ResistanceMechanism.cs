namespace TestSmth2.Models.Domain
{
    public class ResistanceMechanism
    {
        public Guid ID { get; set; }
        public string Name { get; set; }
        public ICollection<Entry> Entries { get; set; }

    }
}
