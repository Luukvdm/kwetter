using System;
using System.Collections.Generic;
using System.Collections.Immutable;

namespace Kwetter.BuildingBlocks.Configurations.Models
{
    public class IdentityConfig
    {
        public const string AppSettingKey = "Identity";

        public string ClientId { get; set; }
        public string ClientSecret { get; set; }
        public string Authority { get; set; }
        public string RequiredAudience { get; set; }
        public IDictionary<string, string> Scopes { get; set; } = ImmutableDictionary<string, string>.Empty; // TODO wrong data type, normal string array is fine
        // public string[] RequiredPolicies { get; set; } = Array.Empty<string>();
    }
}