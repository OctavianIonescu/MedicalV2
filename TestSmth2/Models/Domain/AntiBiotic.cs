namespace TestSmth2.Models.Domain
{
    public class AntiBiotic
    {
        public Guid ID { get; set; }
        public string name { get; set; }
        public bool IsResistant { get; set; }
        public ICollection<Entry> Entries { get; set; }
    }
}
