// using Microsoft.AspNetCore.Mvc;
// using Microsoft.AspNetCore.Mvc.Filters;

// // Custom attribute to authorize based on role
// namespace Backend.Attributes{
//     public class RoleAuthorize : Attribute, IAuthorizationFilter{
//         private readonly string[] role;

//         public RoleAuthorize(params string[] incomingRole){
//             this.role = incomingRole;
//         }

//         // OnAuthorization method will be called for each action/controller that uses this attribute
//         public void OnAuthorization(AuthorizationFilterContext context){
//             // Extract the role from the token in the HttpContext (middleware should have set this value)
//             var userRole = context.HttpContext.Items["Type"]?.ToString();

//             // If the user's role doesn't match the required role, deny access
//             if (userRole==null && !role.Contains(userRole)){
//                 Console.WriteLine($"User Role: {userRole}");
//                 context.Result = new UnauthorizedResult(); // Return 401 Unauthorized
//             }
//         }
//     }   

// }

using Microsoft.AspNetCore.Authorization; // For AuthorizeAttribute
using System.Linq; // For string.Join()


namespace Backend.Attributes
{
    public class RoleAuthorizeAttribute : AuthorizeAttribute
    {
        public RoleAuthorizeAttribute(params string[] roles)
        {
            // Create an individual policy for each role
            Policy = string.Join(";", roles.Select(role => $"{role}Policy"));
        }
    }
}

