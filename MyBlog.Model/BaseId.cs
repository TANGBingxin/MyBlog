namespace MyBlog.Model;
using SqlSugar;
public class BaseId
{
    //设置主键，ID自增
    [SugarColumn(IsIdentity =true,IsPrimaryKey =true)]
    public int Id { get; set; }
}

