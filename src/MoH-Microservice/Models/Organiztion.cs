using System;
using System.ComponentModel.DataAnnotations;

namespace MoH_Microservice.Models
{
    public class Organiztion
    {
        public int Id { get; set; }
        [Required,MaxLength(200)]
        public string Organization { get; set; }
        [Required, MaxLength(200)]
        public string CreatedBy { get; set; }
        public string? UpdatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime? UpdatedOn { get; set; }
    }
    public class OrganiztionReg
    {
        public int Id { get; set; }
        public string Organization { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
    }
    public class OrganiztionUpdate
    {
        public int Id { get; set; }
        public string Organization { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime UpdatedOn { get; set; }
    }
    public class OrganiztionDelete
    {
        public int Id { get; set; }
        public string deletedBy { get; set; }
    }
}
