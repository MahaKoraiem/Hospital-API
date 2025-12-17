using HospitalApi.Models;
using System.ComponentModel.DataAnnotations;
namespace HospitalApi.DTOs.PrescriptionDrug
{
    public class PrescriptionDrugReadDto
    {
        public int Id { get; set; }
        [Required]
        public int PrescriptionId { get; set; }
        [Required]
        public int DrugId { get; set; }
        [Required]
        public int Quantity_Per_Drug { get; set; }

        public DTOs.Prescription.PrescriptionReadDto? Prescription { get; set; }
        public DTOs.Drug.DrugReadDto? Drug { get; set; }
    }
}
