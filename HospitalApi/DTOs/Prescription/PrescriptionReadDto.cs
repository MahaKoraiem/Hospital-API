using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HospitalApi.DTOs.Prescription
{
    public class PrescriptionReadDto
    {
        public int Id { get; set; }
        [Required]
        [Range(1, 1000, ErrorMessage = "Quantity must be at least 1")]
        public int Total_Quantity_Per_Prescription { get; set; }
        [Required]
        public DateTime Date { get; set; }
        [Required]
        public int Patient_Ur_Number { get; set; }
        [Required]
        public int Doctor_Id { get; set; }
        public List<DTOs.PrescriptionDrug.PrescriptionDrugReadDto>? Prescriped_Drugs { get; set; }
        public DTOs.Doctor.DoctorReadDto? Doctor { get; set; }
        public DTOs.Patient.PatientReadDto? Patient { get; set; }
    }
}
