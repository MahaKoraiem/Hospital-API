using System.ComponentModel.DataAnnotations;

namespace HospitalApi.Models
{
    public class PharmaceuticalCompany
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(100)]
        public string Name { get; set; }
        [Required]
        [StringLength(100)]
        public string Address { get; set; }

        [Phone]
        public string Phone { get; set; }
        public ICollection<Drug>? Drugs { get; set; }
    }
}
