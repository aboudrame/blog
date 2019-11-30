using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace blog
{
    public class RefererService: Controller
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public RefererService(HttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }
        public string referer()
        {
            var referer = _httpContextAccessor.HttpContext.Request.Headers["Referer"].ToString();
            return referer;
        }
    }
}
