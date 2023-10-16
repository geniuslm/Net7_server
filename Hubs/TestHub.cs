using Microsoft.AspNetCore.SignalR;
using MongoDB.Bson.Serialization;
using System.Diagnostics;
using Net7服务器.Db;
using Net7服务器.自建类;
using System.Runtime.Serialization.Formatters.Binary;
namespace Net7服务器.Hubs;


public class TestHub : Hub
{
    private readonly Mongodb _MongoDB服务; // MongoDB服务
    Stopwatch 计时器 = new Stopwatch();

    public TestHub(Mongodb mongodb) // 依赖注入
    {
        _MongoDB服务 = mongodb;
    }

    public async Task 测试()
    {
        计时器.Restart();
        string connectionId = Context.ConnectionId;
        await Clients.Client(connectionId).SendAsync("测试", $"测试延迟：{计时器.Elapsed.TotalMilliseconds} 毫秒");

        计时器.Stop();
        Console.WriteLine($"测试延迟：{计时器.Elapsed.TotalMilliseconds} 毫秒");
    }

    public async Task 获取所有旧订单的基础数据()
    {

        计时器.Restart();
        var orders = _MongoDB服务.获取所有旧订单的基础数据();
        // 转换成List<订单类>
        var 订单列表 = orders.Select(order => BsonSerializer.Deserialize<订单类>(order)).ToList();

        // 获取当前连接的 ConnectionId 并返回
        string connectionId = Context.ConnectionId;
        await Clients.Client(connectionId).SendAsync("全部订单", 订单列表);


        计时器.Stop();
        Console.WriteLine($"基础数据 执行用时：{计时器.Elapsed.TotalMilliseconds} 毫秒");
    }

    public async Task 全部订单()
    {

        计时器.Restart();
        var orders = _MongoDB服务.获取所有旧订单();
        // 转换成List<订单类>
        var 订单列表 = orders.Select(order => BsonSerializer.Deserialize<订单类>(order)).ToList();

        // 获取当前连接的 ConnectionId 并返回
        string connectionId = Context.ConnectionId;
        await Clients.Client(connectionId).SendAsync("全部订单", 订单列表);


        计时器.Stop();
        Console.WriteLine($"全部订单 执行用时：{计时器.Elapsed.TotalMilliseconds} 毫秒");
    }

    public async Task 订单号查询(string 订单号)
    {


        计时器.Restart();
        var order = _MongoDB服务.查找按订单号(订单号);
        // 转换成订单类
        var 订单 = BsonSerializer.Deserialize<订单类>(order);

        // 获取当前连接的 ConnectionId 并返回
        string connectionId = Context.ConnectionId;
        await Clients.Client(connectionId).SendAsync("订单号查询", 订单);

        //打印订单得旺旺名 和订单号 和执行用时
        计时器.Stop();
        Console.WriteLine($"订单号查询 旺旺名：{订单.旺旺名} 订单号：{订单.订单号}执行用时：{计时器.Elapsed.TotalMilliseconds} 毫秒");


    }

    public async Task 订单简略表()
    {
        计时器.Restart();
        var orders = _MongoDB服务.获取所有旧订单的基础数据();
        // 转换成List<订单类>
        var 订单列表 = orders.Select(order => BsonSerializer.Deserialize<订单类>(order)).ToList();

        // 获取当前连接的 ConnectionId 并返回
        string connectionId = Context.ConnectionId;
        await Clients.Client(connectionId).SendAsync("全部订单", 订单列表);


        计时器.Stop();
        Console.WriteLine($"全部订单 执行用时：{计时器.Elapsed.TotalMilliseconds} 毫秒");
    }
}