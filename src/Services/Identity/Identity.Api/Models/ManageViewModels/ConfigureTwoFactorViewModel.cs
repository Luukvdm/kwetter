using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Kwetter.Services.Identity.Api.Models.ManageViewModels
{
    public record ConfigureTwoFactorViewModel
    {
        public string SelectedProvider { get; init; }

        public ICollection<SelectListItem> Providers { get; init; }
    }
}
