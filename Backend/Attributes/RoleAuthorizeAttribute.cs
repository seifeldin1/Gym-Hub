using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

// Custom attribute to authorize based on role
namespace Backend.Attributes{
    public class RoleAuthorizeAttribute : Attribute, IAuthorizationFilter{
        private readonly string role;

        public RoleAuthorizeAttribute(string incomingRole){
            this.role = incomingRole;
        }

        // OnAuthorization method will be called for each action/controller that uses this attribute
        public void OnAuthorization(AuthorizationFilterContext context){
            // Extract the role from the token in the HttpContext (middleware should have set this value)
            var userRole = context.HttpContext.Items["Type"]?.ToString();

            // If the user's role doesn't match the required role, deny access
            if (userRole != _role){
                context.Result = new UnauthorizedResult(); // Return 401 Unauthorized
            }
        }
    }   

}
