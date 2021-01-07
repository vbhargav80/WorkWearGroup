using System.ComponentModel.DataAnnotations;

namespace WorkWearGroup.API.Models
{
    public class ErrorContent
    {
        public ErrorContent(string message)
        {
            Message = message;
        }

        [Required]
        public string Message { get; }
    }
}
