using TestSmth2.Models.Domain;

namespace TestSmth2.Models.ViewModels
{
    public class HomeViewModel
    {
        public IEnumerable<Entry> entries { get; set; }
        public IEnumerable<AntiBiotic> antibiotics { get; set; }
    }
}