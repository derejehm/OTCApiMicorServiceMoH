using System.ComponentModel.DataAnnotations;

namespace MoH_Microservice.Models
{
    public class Patient
    {
        public int Id { get; set; }
        [MaxLength(100)]
        public string PatientCardNumber { get; set; }
        [MaxLength(100)]
        public string PatientName { get; set; }
        [MaxLength(100)]
        public string PatientGender { get; set; }
        public string PatientAddress { get; set; }
        public int PatientAge { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; } = DateTime.Now;
        public string UpdatedBy { get; set; }
        public DateTime UpdatedOn { get; set; } = DateTime.Now;
    }

    public class PatientReg
    {

        public string PatientCardNumber { get; set; }
        public string PatientName { get; set; }
        public string PatientGender { get; set; }
        public string PatientAddress { get; set; }
        public int PatientAge { get; set; }
        public string CreatedBy { get; set; }
    }

    public class PatientUpdate
    {
        public string PatientCardNumber { get; set; }
        public string PatientName { get; set; }
        public string PatientGender { get; set; }
        public string PatientAddress { get; set; }
        public int PatientAge { get; set; }
        public string UpdatedBy { get; set; }
    }
}
