using System.ComponentModel.DataAnnotations;
namespace HospitalApi.DTOs.PrescriptionDrug
{
    public class PrescriptionDrugUpdateDto
    {
        [Required]
        public int PrescriptionId { get; set; }
        [Required]
        public int DrugId { get; set; }
        [Required]
        public int Quantity_Per_Drug { get; set; }
    }
}
