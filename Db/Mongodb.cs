using System.Text.Json;
using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Net7服务器.Db;

public class Mongodb
{
    private readonly IMongoDatabase _database;
    private readonly IMongoCollection<BsonDocument> _旧订单;  // 添加属性

    // 构造函数，用于初始化MongoDB连接和数据库
    public Mongodb(string connectionString, string databaseName)
    {
        var client = new MongoClient(connectionString);
        _database = client.GetDatabase(databaseName);
        _旧订单 = _database.GetCollection<BsonDocument>("旧订单");  // 在构造函数里实例化一次
    }

    // 一个简单的方法，用于查询集合中的第一个文档
    public BsonDocument GetFirstDocument(string collectionName)
    {
        var collection = _database.GetCollection<BsonDocument>(collectionName);
        return collection.Find(new BsonDocument()).FirstOrDefault();
    }
    public BsonDocument 查找按订单号(string 订单号)
    {
        var filter = Builders<BsonDocument>.Filter.Eq("订单号", 订单号); // 这里假设你的文档中有一个叫做"订单号字段"的字段用于存储订单号
        return _旧订单.Find(filter).FirstOrDefault();
    }
    public List<BsonDocument> 获取所有旧订单()
    {
        return _旧订单.Find(new BsonDocument()).ToList(); // 这里用一个空的BsonDocument作为查询参数，意味着不进行任何过滤，返回集合中的所有文档
    }
    // 在这里添加更多用于处理数据库的方法，例如增加、删除、修改等

}

