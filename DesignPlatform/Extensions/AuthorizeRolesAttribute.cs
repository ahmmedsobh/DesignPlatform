using DesignPlatform.Enums;

namespace DesignPlatform.Extensions
{
    public class AuthorizeRolesAttribute  : Microsoft.AspNetCore.Authorization.AuthorizeAttribute
    {
        public AuthorizeRolesAttribute(params Roles[] roles) : base()
        {
            Roles = string.Join(",", roles);
        }
    }
}
