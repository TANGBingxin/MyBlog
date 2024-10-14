using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Caching.Memory;

namespace MyBlog.Utility.Utility._Filter
{
    /// <summary>
    /// 使用IMemeroyCache缓存 不需要每次都从数据库调用
    /// </summary>
    public class CustomResourceFilterAttribute : Attribute, IResourceFilter
	{
        private readonly IMemoryCache _cache;
		public CustomResourceFilterAttribute(IMemoryCache cache)
		{
            _cache = cache;
		}

        public void OnResourceExecuted(ResourceExecutedContext context)
        {
            string path = context.HttpContext.Request.Path;
            string route = context.HttpContext.Request.QueryString.Value;
            string key = path + route;
            _cache.Set(key, context.Result);
        }

        public void OnResourceExecuting(ResourceExecutingContext context)
        {
            string path = context.HttpContext.Request.Path;
            string route = context.HttpContext.Request.QueryString.Value;
            string key = path + route;
            if(_cache.TryGetValue(key,out object value))
            {
                context.Result = value as IActionResult;
            }
        }
    }
}

