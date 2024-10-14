using SqlSugar.IOC;
using MyBlog.IRepository;
using MyBlog.Repository;
using MyBlog.IService;
using MyBlog.Service;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.OpenApi.Models;
using MyBlog.Utility._AutoMapper;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    //swaggers使用鉴权组件
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Description = "直接在下框中输入Bearer {token}（注意两者之间是一个空格）",
        Name = "Authorization",
        BearerFormat = "JWT",
        Scheme = "Bearer"
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
        {
          {
            new OpenApiSecurityScheme
            {
              Reference=new OpenApiReference
              {
                Type=ReferenceType.SecurityScheme,
                Id="Bearer"
              }
            },
            new string[] {}
          }
        });

});

#region IOC注册
//创建configuration对象并调用
var configuration = builder.Configuration.AddJsonFile("appsettings.json").Build();

//注入SqlSugar对象
builder.Services.AddSqlSugar(new IocConfig()
{
	ConnectionString = configuration["SqlConn"],
	DbType = IocDbType.SqlServer,
	IsAutoCloseConnection = true
});

//customer ioc注入 用autofac一样
builder.Services.AddCustomIOC();

//JWT鉴权
builder.Services.AddCustomJWT();

//AutoMapper注册
builder.Services.AddAutoMapper(typeof(CustomAutoMapperProfile));

//添加缓存服务
builder.Services.AddMemoryCache();
#endregion

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

//添加鉴权 在webapi中
app.UseAuthentication();
//添加授权 授权在JWT中
app.UseAuthorization();

app.MapControllers();

app.Run();

#region Extend class
static class IOCExtend
{
	public static IServiceCollection AddCustomIOC(this IServiceCollection services)
	{
        services.AddScoped<IBlogNewsRepository, BlogNewsRepository>();
		services.AddScoped<IBlogNewsService, BlogNewsService>();
		services.AddScoped<ITypeInfoRepository, TypeInfoRepository>();
		services.AddScoped<ITypeInfoService, TypeInfoService>();
        services.AddScoped<IWriterInfoRepository, WriterInfoRepository>();
        services.AddScoped<IWriterInfoService, WriterInfoService>();
		return services;
    }

	public static IServiceCollection AddCustomJWT(this IServiceCollection services)
	{
        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("G8hs0bXKm17vRlw2tJvH9NMbq3Aq74Lb")),
                    ValidateIssuer = true,
                    ValidIssuer = "http://localhost:6060",
                    ValidateAudience = true,
                    ValidAudience = "http://localhost:54614",
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.FromMinutes(60)
                };
            });
        return services;
    }
}
#endregion