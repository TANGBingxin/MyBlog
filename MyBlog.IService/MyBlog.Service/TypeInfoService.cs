using System;
using MyBlog.IRepository;
using MyBlog.IService;
using MyBlog.Service;
using MyBlog.Model;

namespace MyBlog.Service
{
	public class TypeInfoService: BaseService<TypeInfo>,ITypeInfoService
	{
        private readonly ITypeInfoRepository _iTypeInfoRepository;

        public TypeInfoService(ITypeInfoRepository iTypeInfoRepository)
        {
            _iTypeInfoRepository = iTypeInfoRepository;
            base._iBaseRepository = iTypeInfoRepository;

        }
    }
}

