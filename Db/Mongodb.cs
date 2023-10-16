using System.Text.Json;
using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Diagnostics;

namespace Net7服务器.Db;

//作为单例注入了
public class Mongodb
{
    private readonly IMongoDatabase _database;
    private readonly IMongoCollection<BsonDocument> _旧订单;  // 添加属性

    // 构造函数，用于初始化MongoDB连接和数据库
    public Mongodb(string connectionString, string databaseName)
    {
        var client = new MongoClient(connectionString);
        _database = client.GetDatabase(databaseName);
        _旧订单 = _database.GetCollection<BsonDocument>("blazor");  // 在构造函数里实例化一次
    }

    // 一个简单的方法，用于查询集合中的第一个文档
    public BsonDocument GetFirstDocument(string collectionName)
    {
        var collection = _database.GetCollection<BsonDocument>(collectionName);
        return collection.Find(new BsonDocument()).FirstOrDefault();
    }

    //按照订单号查找
    public BsonDocument 查找按订单号(string 订单号)
    {
        var filter = Builders<BsonDocument>.Filter.Eq("订单号", 订单号); // 这里假设你的文档中有一个叫做"订单号字段"的字段用于存储订单号
        return _旧订单.Find(filter).FirstOrDefault();
    }

    //查找所以订单
    public List<BsonDocument> 获取所有旧订单()
    {
        return _旧订单.Find(new BsonDocument()).ToList(); // 这里用一个空的BsonDocument作为查询参数，意味着不进行任何过滤，返回集合中的所有文档
    }

    //分页查找
    public List<BsonDocument> 获取所有旧订单的基础数据()
    {
        Stopwatch stopwatch = new Stopwatch();
        stopwatch.Start();


        // 分页参数
        int 页数 = 1;
        int 每页数量 = 10;
        var projection = Builders<BsonDocument>.Projection.Include("订单号")
                                                         .Include("删除信息")
                                                         .Include("收件人")
                                                         .Include("旺旺名")
                                                         .Include("镜片");

        // 使用Find和Project获取数据，并添加Skip和Limit进行分页
        var orders = _旧订单.Find(new BsonDocument())
                          .Project(projection)
                          .Skip((页数 - 1) * 每页数量)
                          .Limit(每页数量)
                          .ToList();

        stopwatch.Stop();
        Console.WriteLine($"mongo 执行用时：{stopwatch.Elapsed.TotalMilliseconds} 毫秒");
        return orders;
    }


}

