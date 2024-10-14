using MyBlog.Model;
using Microsoft.AspNetCore.Mvc;
using MyBlog.IService;
using MyBlog.Utility.ApiResult;
using MyBlog.Utility._MD5;
using AutoMapper;
using MyBlog.Model.DTO;

namespace MyBlog.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WriterController : ControllerBase
    {
        private readonly IWriterInfoService _iWriterInfoService;

        public WriterController(IWriterInfoService iWriterInfoService)
        {
            _iWriterInfoService = iWriterInfoService;
        }

        [HttpPost(template: "Create")]
        public async Task<ApiResult> Create(string name, string username, string password)
        {
            WriterInfo writer = new WriterInfo
            {
                Name = name,
                UserName = username,
                UserPwd = MD5Helper.MD5Encrypt32(password)
            };

            var existWriter = await _iWriterInfoService.FindAsync(e => e.UserName == username);
            if (existWriter != null) return ApiResultHepler.Error("Writer already exists");
            bool result = await _iWriterInfoService.CreateAsync(writer);
            if (!result) return ApiResultHepler.Error("Creation Write Failed!");
            return ApiResultHepler.Success(writer);
        }

        [HttpPut("Edit")]
        public async Task<ApiResult> Edit(string name)
        {
            int id = Convert.ToInt32(this.User.FindFirst("Id").Value);
            var writer = await _iWriterInfoService.FindAsync(id);
            writer.Name = name;
            bool result = await _iWriterInfoService.EditAsync(writer);
            if (!result) return ApiResultHepler.Error("Edit writer failed!");
            return ApiResultHepler.Success("Edit writer succesful");
        }

        [HttpGet("FindWriter")]
        public async Task<ApiResult> FindWriter([FromServices] IMapper mapper,int id)
        {
            var writer = await _iWriterInfoService.FindAsync(id);
            if (writer == null) return ApiResultHepler.Error("Writer not exist");
            var writerDTO = mapper.Map<WriterDTO>(writer);
            return ApiResultHepler.Success(writerDTO);
        }
    } 
}

