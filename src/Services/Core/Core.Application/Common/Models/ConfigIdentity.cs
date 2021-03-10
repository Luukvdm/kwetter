using System;
using System.Collections.Generic;
using System.Collections.Immutable;

namespace Kwetter.Services.Core.Application.Common.Models
{
    public class ConfigIdentity
    {
        public const string AppSettingKey = "Identity";

        public string ClientId { get; set; }
        public string ClientSecret { get; set; }
        public string Authority { get; set; }
        public IDictionary<string, string> Scopes { get; set; } = ImmutableDictionary<string, string>.Empty;
        public string[] RequiredPolicies { get; set; } = Array.Empty<string>();
    }
}
