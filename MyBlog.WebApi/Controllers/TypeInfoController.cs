using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MyBlog.IService;
using MyBlog.Model;
using MyBlog.Service;
using MyBlog.Utility.ApiResult;

namespace MyBlog.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TypeInfoController : ControllerBase
    {
        private readonly ITypeInfoService _iTypeInfoService;

        public TypeInfoController(ITypeInfoService iTypeInfoService)
        {
            _iTypeInfoService = iTypeInfoService;
        }

        [HttpGet(template: "TypeInfo")]
        public async Task<ActionResult<ApiResult>> GetTypeInfo()
        {
            var data = await _iTypeInfoService.QueryAsync();
            if (data == null) return ApiResultHepler.Error(msg: "There is no type info");
            return ApiResultHepler.Success(data);
        }

        [HttpPost(template: "Create")]
        public async Task<ActionResult<ApiResult>> CreateTypeInfo(string name)
        {
            //数据验证
            if (string.IsNullOrWhiteSpace(name)) return ApiResultHepler.Error("Type name is empty");

            TypeInfo typeInfo = new TypeInfo
            {
                Name = name
            };
            bool result = await _iTypeInfoService.CreateAsync(typeInfo);
            if (!result) return ApiResultHepler.Error("Creation Type Failed! An error occurred on the server.");
            return ApiResultHepler.Success(typeInfo);
        }

        [HttpDelete(template: "Delete")]
        public async Task<ActionResult<ApiResult>> DeleteTypeInfo(int id)
        {
            bool result = await _iTypeInfoService.DeleteAsync(id);
            if (!result) return ApiResultHepler.Error("Delete Type Failed! An error occurred on the server.");
            return ApiResultHepler.Success(result);
        }

        [HttpPut(template: "Edit")]
        public async Task<ActionResult<ApiResult>> EditBlogNews(int id, string name)
        {
            TypeInfo typeInfo = await _iTypeInfoService.FindAsync(id);
            if (typeInfo == null) return ApiResultHepler.Error("Type Not Found");

            typeInfo.Name = name;

            bool result = await _iTypeInfoService.EditAsync(typeInfo);
            if (!result) return ApiResultHepler.Error(msg: "Edit Type Failed! An error occurred on the server.");
            return ApiResultHepler.Success(typeInfo);
        }

    }
}

