using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace MoH_Microservice.Models.Form
{


    public class PaymentTypeDTO
    {
        public int? DescriptionId { get; set; }
        public int? TypeId { get; set; }
        public string? Type { get; set; }
        public string? Description { get; set; }
        public decimal? PriceLimit { get; set; }
        public int? TimeLimitInHours { get; set; }
        public string? CreatedBy { get; set; }
        public string? UpdatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public DateTime? CreatedOn { get; set; }
    }
    public class PaymentTypeDTO2
    {
        public int? Id { get; set; }
        public string? Type { get; set; }
        public PaymentTypeDiscriptionDTO[]? Description { get; set; }
    }
    public class PaymentTypeDiscriptionDTO
    {
        public int? Id { get; set; }
        public string? Description { get; set; }
    }
}
