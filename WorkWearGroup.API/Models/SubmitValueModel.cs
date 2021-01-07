using System.ComponentModel.DataAnnotations;

namespace WorkWearGroup.API.Models
{
    public class SubmitValueModel
    {
        [Required]
        [StringLength(1024)]
        public string Value { get; set; }
    }
}
