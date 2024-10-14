using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyBlog.Utility.Utility._Filter;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MyBlog.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        [HttpGet("NoAuthorize")]
        public string NoAuthorize()
        {
            return "this is no Authorize";
        }

        [HttpGet("Authorize")]
        [Authorize]
        public string Authorize()
        {
            return "this is Authorize";
        }

        //缓存测试
        [TypeFilter(typeof(CustomResourceFilterAttribute))]
        [HttpGet]
       public IActionResult GetCache(string name)
        {
            return new JsonResult(new
            {
                name = name,
                age = 18,
                sex = true
            }) ;
        }
    }
}

