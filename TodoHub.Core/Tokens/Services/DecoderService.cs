using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace TodoHub.Core.Tokens.Services
{
    public class DecoderService(IHttpContextAccessor httpContextAccessor)
    {
        public string GetUserId()
        {
            return httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value;
            //.HttUser.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value;
            
        }

    }
}
