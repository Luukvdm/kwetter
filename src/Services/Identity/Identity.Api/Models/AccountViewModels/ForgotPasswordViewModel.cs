using System.ComponentModel.DataAnnotations;

namespace Kwetter.Services.Identity.Api.Models.AccountViewModels
{
    public record ForgotPasswordViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; init; }
    }
}
