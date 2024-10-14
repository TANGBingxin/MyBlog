using System;
using SqlSugar;

namespace MyBlog.Utility.ApiResult
{
	public class ApiResultHepler
	{
        /// <summary>
        /// 分页版 SqlSugar的参数 RefAsync
        /// </summary>
        /// <param name="data"></param>
        /// <param name="total"></param>
        /// <returns></returns>
        public static ApiResult Success(dynamic data,RefAsync<int> total = null)
        {
            return new ApiResult
            {
                Code = 200,
                Msg = "Action Succesful",
                Total = total!=null?total:0,
                Data = data
            };
        }

        public static ApiResult Error(string msg)
        {
            return new ApiResult
            {
                Code = 500,
                Data = null,
                Msg = msg,
                Total = 0
            };
        }

    }
}

