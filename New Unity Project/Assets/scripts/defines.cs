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
	public Subtitle(string _zh, string _en, double _begin, double _end)
    {
        zh = _zh;
        en = _en;
        begin = _begin;
        end = _end;
    }
    public string zh;
    public string en;
	public double begin;
	public double end;
};



public class defines
{
    static public List<DataTuple> datas = new List<DataTuple> {
    new DataTuple("来自地狱", "紫罗兰的永恒乌托邦"),
	new DataTuple("Made in Hell", "Violet Everutopia"),
	new DataTuple("你为什么那么熟练", "本应该是双份的快乐……为什么"),
    new DataTuple("多冷啊♪ 我在东北玩泥巴♪", "鱼♪ 好大的鱼♪ 虎纹鲨鱼♪"),
    new DataTuple("这个世界需要更多先驱", "恭喜发财，晚上吃鹅"),
    new DataTuple("直到我膝盖中了一箭", "不来盘昆特牌吗"),
	new DataTuple("I took an arrow in the knee", "Wouldn't mind a few rounds of gwent"),
	new DataTuple("红红火火何厚滑", "韩韩会画画后悔画韩宏"),
    new DataTuple("我从河北省来", "美国 圣地亚戈"),
	new DataTuple(":D", "_(:зゝ∠)_"),
	new DataTuple("《传记霸业》", "《贪玩青月》"),
	new DataTuple("红色有角三倍速", "他变秃了，也变强了"),
	new DataTuple("彩虹小马", "彩虹喵"),
	new DataTuple("真水浒无双", "真水浒无双·猛将传"),
	new DataTuple("哎呀老娘好气啊", "大连有个阿瓦隆"),
	new DataTuple("经典力学是真理", "相对论吊打一切"),
    new DataTuple("eeeeeeeeeeee", "aaaaaaaaaaaaaaa"),
    };
    static public float MoveSpeed = 12.0f;   //移动速度
    static public float changeDelay = 2.5f; //检测出关卡结束了，多少秒后开始下黑幕
    static public float FadeSpd = 2.0f; //黑幕渐变速度  x alpha/s   1就是1秒从全白到全黑
    static public float TweenTime = 0.5f; //镜头从一个人转到另一个的时间间隔
    static public GameObject circlePrefab = null;
    static public bool boss = false;
    static public float bossr = 20.0f;
    static public float SubtitleFadeTime = 0.1f;
	static public float lastSceneTime = 21.0f;

    static public Dictionary<int, List<Subtitle>> Subtitles = new Dictionary<int, List<Subtitle>> {
        {
            0, // 关卡编号 从0开始数
            new List<Subtitle> {
				new Subtitle("[←↑↓→] or [WSAD] to Move", "[SPACE] to Post", 0, 3.5), // begin  end
				new Subtitle("这位穿着蓝白西装、眼神明亮、头发极度偏分的是一名上班族。", "This man in blue is an employee.", 4, 6.5), 
				new Subtitle("上班族要上班，没有那么多时间使用社交网络。", "An employee must work, and doesn't have much time using social network.", 7, 9.5), 
				new Subtitle("所以他经常做的一件事，就是转发。", "So he always forwards.", 10, 12), 
            }
        },
        {
            2,
            new List<Subtitle> {
				new Subtitle("这只穿着很土、眼神呆滞、走路都得蹲着的是一名家里蹲。", "This sitting guy is a NEET.", 1, 3),
				new Subtitle("家里蹲不用上班，时间很多。", "A NEET doesn't work, and has a lot of time.", 3.5, 5.5),
				new Subtitle("他使用社交网络的时候，不仅会转发，还会和别人讨论。", "Using social network, a NEET not only forwards, but also discusses with each other.", 6, 8.5),
				new Subtitle("这就是为什么他能发多一次波。", "That's why he can send message twice, in this game.", 9, 11),
            }
		},
		{
			3,
			new List<Subtitle> {
				new Subtitle("这里有一只上一关也见到过的奇怪生物，全身肉色看起来好像连衣服都没穿。", "The guy with white hair is a madman.", 1, 3.5),
				new Subtitle("你们不要理会他，他是一只疯子。", "Just...leave him alone.", 4, 6),
				new Subtitle("疯子可以接收信息，但是没有人会相信疯子说的话。", "A madman can receive message, but no one believe what he says.", 6.5, 9),
				new Subtitle("所以他发的波只有他自己能听到。", "His words are all said to himself.", 9.5, 11.5),
			}
		},
		{
			6,
			new List<Subtitle> {
				new Subtitle("介绍完了五个角色，但其实他们都不是主角。", "Five roles have been introduced, but none of them is the leading role of this game.", 1, 3.5),
			}
		}
    };
	static public Dictionary<int, List<Subtitle>> Subtitles1 = new Dictionary<int, List<Subtitle>> {
		{
			1,
			new List<Subtitle> {
				new Subtitle("当然，他有时候也会发到群里，或者朋友圈微博，让更多的人看到。", "Sometimes, the message he forwarded was got by more guys.", 0, 3),
			}
		},
		{
			4,
			new List<Subtitle> {
				new Subtitle("这个头发都没有，拄着拐杖的是一名老顽固。", "This bald guy is a stubborn old man.", 0, 2.5),
				new Subtitle("他要接受一个信息7次，才会成为这个信息的传播者。", "A stubborn old man must receive a message 7 times, before he became a disseminator.", 3, 5.5),
			}
		},
		{
			5,
			new List<Subtitle> {
				new Subtitle("诶，这人怎么回事啊？怎么穿得好像蒙面超人一样，头上还有个大大的V。", "This man with a big V on his head, is an opinion leader.", 0, 2.5),
				new Subtitle("没错，他就是大V。", "※opinion leader = big V, in chinese.", 3, 5),
				new Subtitle("大V每天都收到大量的信息，在这些信息中，取出一个切面来发表。", "An opinion leader receives a looooooot of messages everyday.", 5.5, 8),
				new Subtitle("这家伙有完没完……", "They are just not going to stop, do they?", 15, 17.5),
				new Subtitle("大V的影响力非常大，所以他的波也很大。", "And he has rather a large influence.", 18, 21),
			}
		},
		{
			6,
			new List<Subtitle> {
				new Subtitle("我们的主角是“[XXX]”。", "Our leading role is \"[XXX]\".", 0.5, 3.5),
				new Subtitle("这个梗，经过了纵向传播，也经过圈子的传播。。", "This message, this meme has been spread person to person, group to group, opinion leader to opinion leader.", 4, 7),
				new Subtitle("达到了前所未有的繁荣。", "Its dynasty has been bulit.", 7.5, 9.5),
				new Subtitle("它就像海浪，而我们则像冲浪者。", "Meme is wave, and we are surfers.", 10, 12.5),
				new Subtitle("没有人可以与海浪搏斗。", "No one can fight waves.", 13, 15),
				new Subtitle("冲浪者只能在海浪中顺势浮起，", "Waves float surfers,", 15.5, 17.5),
				new Subtitle("或被水淹没。", "or drown them.", 18, 20),
			}
		}
	};
	static public List<Subtitle> Subtitles2 = new List<Subtitle> {
		new Subtitle("没有“人”可以，除了。。。另一个梗。", "No one can. Unless...., the other wave comes.", 4, 7.5),
		new Subtitle("被另一个梗所覆盖，之前的梗热度消散，被遗忘，淡出我们的视野。", "After being replaced, the meme was forgot, went out of our sights.", 8, 11.5),
		new Subtitle("这就是一个梗的一生。", "This is meme's life.", 12, 14.5),
		new Subtitle("这就是我们所处的时代。", "This is our century.", 15, 17),
		new Subtitle("这就是我们游戏，模因王座/玩梗狂潮。", "This is our game, meme wave/meme throne.", 17.5, 20),
	};
}
