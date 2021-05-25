using System.Collections.Generic;

namespace Kwetter.BuildingBlocks.IdentityBlocks.Constants
{
    public static class Roles
    {
        public static IList<string> ToList()
        {
            return new List<string> {Admin, Moderator};
        }
        
        public const string Admin = "admin";
        public const string Moderator = "mod";
    }
}