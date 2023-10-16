using Net7服务器.Hubs;                             // 引用你自己定义的Hub
using Net7服务器.Db;
using MongoDB.Bson;

// 创建一个Web应用构建器
var builder = WebApplication.CreateBuilder(args);
builder.WebHost.UseUrls("http://localhost:5050", "https://localhost:5051"); // 在这里设置URL和端口

// 添加Mongodb数据库的单例依赖注入
builder.Services.AddSingleton<Mongodb>(sp =>
    new Mongodb("mongodb://geniuslmt:genius@38.105.26.244:27017/Data?authSource=admin", "Data"));

builder.Services.AddSignalR();
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", builder =>
    {
        builder.AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader();
    });
});

// 构建应用
var app = builder.Build();

// 使用CORS中间件
app.UseCors("AllowAll");

// 添加一个基础的GET路由，返回"Hello World!"
app.MapGet("/", () => "Hello World!李默.net服务器");
//添加TestHub 管道
app.MapHub<TestHub>("/TestHub");


app.Run();