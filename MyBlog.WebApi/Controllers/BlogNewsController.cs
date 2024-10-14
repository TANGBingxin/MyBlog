using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyBlog.IService;
using MyBlog.Model;
using MyBlog.Utility.ApiResult;
using SqlSugar;

namespace MyBlog.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class BlogNewsController : ControllerBase
    {
        private readonly IBlogNewsService _iBlogNewsService;

        public BlogNewsController(IBlogNewsService iBlogNewsService)
        {
            _iBlogNewsService = iBlogNewsService;
        }

        [HttpGet(template: "BlogNews")]
        public async Task<ActionResult<ApiResult>> GetBlogNews()
        {
            //得到当前用户
            var id = Convert.ToInt32(this.User.FindFirst("Id").Value);
            var data = await _iBlogNewsService.QueryAsync(b => b.WriterId == id);
            if (data == null) return ApiResultHepler.Error(msg: "There is no article");
            return ApiResultHepler.Success(data);
        }

        [HttpPost(template: "Create")]
        public async Task<ActionResult<ApiResult>> CreateBlogNews(string title, string content, int typeId)
        {
            //数据验证
            BlogNews blogNews = new BlogNews
            {
                BrowseCount = 0,
                Content = content,
                LikeCount = 0,
                Time = DateTime.Now,
                Title = title,
                TypeId = typeId,
                WriterId = Convert.ToInt32(this.User.FindFirst("Id").Value)
            };
            bool result = await _iBlogNewsService.CreateAsync(blogNews);
            if (!result) return ApiResultHepler.Error("Creation Blog Failed! An error occurred on the server.");
            return ApiResultHepler.Success(blogNews);
        }

        [HttpDelete(template: "Delete")]
        public async Task<ActionResult<ApiResult>> DeleteBlogNews(int id)
        {
            bool result = await _iBlogNewsService.DeleteAsync(id);
            if (!result) return ApiResultHepler.Error("Delete Blog Failed! An error occurred on the server.");
            return ApiResultHepler.Success(id);
        }

        [HttpPut(template: "Edit")]
        public async Task<ActionResult<ApiResult>> EditBlogNews(int id, string title, string content, int typeId)
        {
            BlogNews blogNews = await _iBlogNewsService.FindAsync(id);
            if (blogNews == null) return ApiResultHepler.Error("Article Not Found");

            blogNews.Title = title;
            blogNews.Content = content;
            blogNews.TypeId = typeId;

            bool result = await _iBlogNewsService.EditAsync(blogNews);
            if (!result) return ApiResultHepler.Error(msg: "Edit Blog Failed! An error occurred on the server.");
            return ApiResultHepler.Success(result);
        }

        [HttpGet("BlogNewsPage")]
        public async Task<ApiResult> GetBlogNewsPage([FromServices] IMapper iMapper,int page, int size)
        {
            RefAsync<int> total = 0;
            var blog = await _iBlogNewsService.QueryAsync(page, size, total);
            try
            {
                var blogDTO = iMapper.Map<List<BlogNews>>(blog);
                return ApiResultHepler.Success(blogDTO);

            }
            catch (Exception)
            {
                return ApiResultHepler.Error(msg: "AutoMapper Error");
            }
        }
    }
}

