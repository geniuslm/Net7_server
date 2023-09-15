using Microsoft.AspNetCore.SignalR;
using Net7服务器.Db;

namespace Net7服务器.Hubs;


public class TestHub : Hub
{
    private readonly Mongodb _MongoDB服务; // MongoDB服务

    public TestHub(Mongodb mongodb) // 依赖注入
    {
        _MongoDB服务 = mongodb;
    }

    public async Task 方法_发出信息(string 用户名, string 消息)
    {
        // Clients.All是SignalR中的一个动态对象，用于向连接到这个Hub的所有客户端发送消息
        await Clients.All.SendAsync("事件_接收信息", 用户名, 消息);
        //打印到控制台
        Console.WriteLine($"用户{用户名}发送了消息：{消息}");
    }
    public async Task 方法_获取所有旧订单()
    {
        var orders = _MongoDB服务.获取所有旧订单(); // 假设这个方法返回所有"旧订单"的列表

        // 将订单发送到所有连接的客户端
        await Clients.All.SendAsync("事件_接收所有旧订单", orders);
    }
}