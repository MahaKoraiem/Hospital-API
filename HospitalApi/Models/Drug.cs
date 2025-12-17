using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HospitalApi.Models
{
    public class Drug
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Trade_Name { get; set; }
        [Required]
        [StringLength(30)]
        public string Strength { get; set; }
        [Required]
        [ForeignKey("Comapny_Id")]
        public int Company_Id { get; set; }
        public PharmaceuticalCompany PharmaceuticalCompany { get; set; }
        public ICollection<PrescriptionDrug>? PrescriptionDrugs { get; set; }
    }
}
