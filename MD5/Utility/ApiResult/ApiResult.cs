﻿using System;
namespace MyBlog.Utility.ApiResult
{
	public class ApiResult
	{
		public int Code { get; set; }
		public string Msg { get; set; }
		public int Total { get; set; }
		public dynamic Data { get; set; }
	}
}

