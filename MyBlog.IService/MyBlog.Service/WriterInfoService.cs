using System;
using MyBlog.IRepository;
using MyBlog.IService;
using MyBlog.Model;

namespace MyBlog.Service
{
	public class WriterInfoService : BaseService<WriterInfo>, IWriterInfoService
    {
        private readonly IWriterInfoRepository _iWriterInfoRepository;

        public WriterInfoService(IWriterInfoRepository iWriterInfoRepository)
        {
            _iWriterInfoRepository = iWriterInfoRepository;
            base._iBaseRepository = iWriterInfoRepository;

        }
    }
}

