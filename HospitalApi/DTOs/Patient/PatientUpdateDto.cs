using System.ComponentModel.DataAnnotations;

namespace HospitalApi.DTOs.Patient
{
    public class PatientUpdateDto
    {

        [Required(ErrorMessage = "Name is required")]
        [StringLength(100, ErrorMessage = "Name cannot exceed 100 characters")]
        public string Name { get; set; }

        [Range(0, 100, ErrorMessage = "Age must be between 0 and 100")]
        public int Age { get; set; }
        [Phone(ErrorMessage = "Invalid phone number format")]
        public string Phone { get; set; }
        [StringLength(200)]
        public string? Address { get; set; }
        [EmailAddress(ErrorMessage = "Invalid email address")]
        public string? Email { get; set; }
    }
}
