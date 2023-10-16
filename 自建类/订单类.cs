using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace Net7服务器.自建类;

[BsonIgnoreExtraElements]
public class 订单类
{
    [BsonId]  // MongoDB主键标识
    public ObjectId _id { get; set; }

    // 基础信息
    public string? 订单号 { get; set; } = "";
    public string? 删除信息 { get; set; } = "";
    public string? 收件人 { get; set; } = "";
    public string? 旺旺名 { get; set; } = "";
    public string? 镜片 { get; set; } = "";

    // 镜片信息
    public string? 右近视 { get; set; } = "";
    public string? 右散光 { get; set; } = "";
    public string? 右轴向 { get; set; } = "";
    public string? 右瞳距 { get; set; } = "";

    public string? 左近视 { get; set; } = "";
    public string? 左散光 { get; set; } = "";
    public string? 左轴向 { get; set; } = "";
    public string? 左瞳距 { get; set; } = "";

    public string? 备注 { get; set; } = "";

    // 镜片日期和供应商信息
    public string? 镜片下单日 { get; set; } = "";
    public string? 镜片订货日 { get; set; } = "";
    public string? 右镜片订货日 { get; set; } = "";
    public string? 左镜片订货日 { get; set; } = "";
    public string? 镜片备好日 { get; set; } = "";
    public string? 右镜片备好日 { get; set; } = "";  // 新增属性
    public string? 左镜片备好日 { get; set; } = "";  // 新增属性
    public string? 镜片供应商 { get; set; } = "";
    public string? 右镜片供应商 { get; set; } = "";  // 新增属性
    public string? 左镜片供应商 { get; set; } = "";  // 新增属性

    // 镜片价格信息
    public decimal? 镜片进货价 { get; set; } = 0;
    public decimal? 镜片售价 { get; set; } = 0;

    // 镜框信息
    public string? 镜框选项 { get; set; } = "";
    public string? 选定镜框 { get; set; } = "";  // 新增属性
    public string? 镜框运单号 { get; set; } = "";
    public string? 镜框下单日 { get; set; } = "";
    public string? 镜框发货日 { get; set; } = "";
    public string? 镜框备好日 { get; set; } = "";

    // 镜框价格信息
    public decimal? 镜框进货价 { get; set; } = 0;
    public decimal? 镜框售价 { get; set; } = 0;

    // 订单信息
    public string? 订单进度 { get; set; } = "";
    public string? 订单完成日 { get; set; } = "";

    // 利润和优惠信息
    public decimal? 镜片利润 { get; set; } = 0;
    public decimal? 镜框利润 { get; set; } = 0;
    public decimal? 优惠 { get; set; } = 0;
    public decimal? 总利润 { get; set; } = 0;

    // 其他
    public List<string>? 试戴镜框 { get; set; } = new();
    public List<string>? 编辑记录 { get; set; } = new();
    public List<string>? 购买记录 { get; set; } = new();
}
