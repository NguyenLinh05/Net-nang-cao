using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace QuanLy.Controllers
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class CustomAuthorizationAttribute : AuthorizeAttribute
    {
        public CustomAuthorizationAttribute(string role)
        {
            Roles = role;
        }
    }
}
