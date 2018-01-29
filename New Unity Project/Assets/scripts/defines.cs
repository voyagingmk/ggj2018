using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataTuple
{
    public DataTuple(string _a, string _b, string _c = "")
    {
        a = _a;
        b = _b;
        c = _c;
    }
    public string a;
    public string b;
    public string c;
};

public class Subtitle
{
    public Subtitle(string _zh, string _en, int _begin, int _end)
    {
        zh = _zh;
        en = _en;
        begin = _begin;
        end = _end;
    }
    public string zh;
    public string en;
    public int begin;
    public int end;
};



public class defines
{
    static public List<DataTuple> datas = new List<DataTuple> {
    new DataTuple("经典力学是真理", "相对论吊打一切"),
    new DataTuple("宝物之国", "紫罗兰的永恒乌托邦"),
	new DataTuple("你为什么那么熟练", "明明是我先"),
    new DataTuple("多冷啊♪ 我在东北玩泥巴♪", "鱼♪ 好大的鱼♪ 虎纹鲨鱼♪"),
    new DataTuple("这个世界需要更多英雄", "大吉大利，今晚吃鸡"),
    new DataTuple("直到我膝盖中了一箭", "不来盘昆特牌吗"),
    new DataTuple("我从河北省来", "美国 圣地亚戈"),
    new DataTuple("红红火火何厚滑", "韩韩会画画后悔画韩宏"),
	new DataTuple("万物皆可无双", "你可能是正版游戏的受害者"),
	new DataTuple("叽嘟", "不可吱"),
	new DataTuple("《黄冈兵法》", "《5年高考3年模拟》"),
	new DataTuple("彩虹小马", "彩虹喵"),
	new DataTuple("真水浒无双", "真水浒无双·猛将传"),
	new DataTuple("哎呀老娘好气啊", "大连有个阿瓦隆"),
    new DataTuple("苟利国家生死以", "富强、民主、文明、和谐", "+1s"),
    };
    static public float MoveSpeed = 12.0f;   //移动速度
    static public float changeDelay = 2.5f; //检测出关卡结束了，多少秒后开始下黑幕
    static public float FadeSpd = 2.0f; //黑幕渐变速度  x alpha/s   1就是1秒从全白到全黑
    static public float TweenTime = 0.5f; //镜头从一个人转到另一个的时间间隔
    static public GameObject circlePrefab = null;
    static public bool boss = false;
    static public float bossr = 20.0f;

    static public Dictionary<int, List<Subtitle>> Subtitles = new Dictionary<int, List<Subtitle>> {
        {
            0, // 关卡编号 从0开始数
            new List<Subtitle> {
                new Subtitle("字幕测试", "Subtitle Test", 0, 10),
            }
        }
    };
}
