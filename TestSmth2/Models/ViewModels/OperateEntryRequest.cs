using TestSmth2.Models.Domain;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.Rendering;


namespace TestSmth2.Models.ViewModels
{
    public class OperateEntryRequest
    {

        public Guid ID { get; set; }

        public string entryCode { get; set; }

        public string PatientCNP { get; set; }

        public string PathologicProduct { get; set; }

        public string Microbe { get; set; }

        public string sectiaDeProvenienta { get; set; }

        public string shortDescription { get; set; }

        public string medicSolicitant { get; set; }

        public string URLHandle { get; set; }

        public string FileURL { get; set; }

        public DateTime collectionDate { get; set; }

        public DateTime validationDate { get; set; }

        public IEnumerable<SelectListItem> Antibiotics { get; set; }
        public IEnumerable<SelectListItem> Resistance { get; set; }

        public string[] SelectedAntibiotics { get; set; } = Array.Empty<string>();
        public string[] SelectedResistance { get; set; } = Array.Empty<string>();
    }
}
