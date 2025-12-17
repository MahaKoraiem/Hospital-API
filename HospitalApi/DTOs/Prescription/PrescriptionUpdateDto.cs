using System.ComponentModel.DataAnnotations;

namespace HospitalApi.DTOs.Prescription
{
    public class PrescriptionUpdateDto
    {
        [Required]
        [Range(1, 1000, ErrorMessage = "Quantity must be at least 1")]
        public int Total_Quantity_Per_Prescription { get; set; }
        [Required]
        public DateTime Date { get; set; }
        [Required]
        public int Patient_Ur_Number { get; set; }
        [Required]
        public int Doctor_Id { get; set; }
    }
}
