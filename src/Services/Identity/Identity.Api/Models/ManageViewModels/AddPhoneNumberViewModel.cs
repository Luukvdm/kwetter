﻿using System.ComponentModel.DataAnnotations;

namespace Kwetter.Services.Identity.Api.Models.ManageViewModels
{
    public record AddPhoneNumberViewModel
    {
        [Required]
        [Phone]
        [Display(Name = "Phone number")]
        public string PhoneNumber { get; init; }
    }
}
