namespace TestSmth2.Models.ViewModels
{
    public class EditAntibioticRequest
    {
        public Guid ID { get; set; }
        public string Name { get; set; }
        public bool IsResistant { get; set; }
    }
}