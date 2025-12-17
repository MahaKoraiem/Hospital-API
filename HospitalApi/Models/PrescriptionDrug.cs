namespace HospitalApi.Models
{
    public class PrescriptionDrug
    {
        public int Id { get; set; }
        public int PrescriptionId { get; set; }
        public Prescription Prescription { get; set; }
        public int DrugId { get; set; }
        public Drug Drug { get; set; }

        public int Quantity_Per_Drug { get; set; }
    }
}
