using MyBlog.IRepository;
using MyBlog.IService;
using MyBlog.Repository;
using MyBlog.Service;
using SqlSugar.IOC;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//IOC注入
builder.Services.AddScoped<IWriterInfoRepository, WriterInfoRepository>();
builder.Services.AddScoped<IWriterInfoService, WriterInfoService>();

//注入SqlSugar对象
//创建configuration对象并调用
var configuration = builder.Configuration.AddJsonFile("appsettings.json").Build();
builder.Services.AddSqlSugar(new IocConfig()
{
    ConnectionString = configuration["SqlConn"],
    DbType = IocDbType.SqlServer,
    IsAutoCloseConnection = true
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

