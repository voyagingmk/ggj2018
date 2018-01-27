using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DataTuple {
    public DataTuple(string _a, string _b)
    {
        a = _a;
        b = _b;
    }
    public string a;
    public string b;
};


public class stageCtrl : MonoBehaviour {
    public cameraFollow camFol;
    public GameObject stage;
    public List<GameObject> stagePrefabs;
    public int stageIdx = 0;
    public bool end = false;
    public Text text;
    public int changeDelay = 3;
    public List<DataTuple> datas;
    public List<Button> btns;
    void InitBtns()
    {
        datas = new List<DataTuple> {
            new DataTuple("牛顿经典力学是真理", "相对论吊打一切"),
            new DataTuple("叽嘟", "不可吱"),
            new DataTuple("宝物之国", "紫罗兰的永恒乌托邦"),
            new DataTuple("_(:зゝ∠)_", "(・ω・=)"),
            new DataTuple("哎呀老娘好气啊", "大连有个阿瓦隆"),
            new DataTuple("彩虹小马", "彩虹喵喵、喵喵喵喵……"),
            new DataTuple("鱼♪好大的鱼♪虎纹鲨鱼♪", "多冷啊♪我在东北玩泥巴♪ "),
            new DataTuple("这个世界需要更多英雄", "大吉大利，今晚吃鸡"),
            new DataTuple("真水浒无双", "真水浒无双·猛将传"),
            new DataTuple("黄冈兵法", "《5年高考3年模拟》"),
            new DataTuple("直到我膝盖中了一箭", "不来盘昆特牌吗"),
            new DataTuple("万物皆可无双", "你可能是正版游戏的受害者"),
            new DataTuple("我从河北省来", "美国 圣地亚戈"),
            new DataTuple("红红火火何厚滑", "韩韩会画画后悔画韩宏"),
            new DataTuple("苟利国家生死以", "爱国、民主……"),
        };
        int i = 0;
        foreach (Button b in btns)
        {
            b.GetComponentInChildren<Text>().text = datas[i].a;
            i++;
        }
    }
    // Use this for initialization
    void Start () {
        InitBtns();
        foreach (GameObject s in stagePrefabs)
        {
            s.SetActive(false);
        }
        stage = null;
        EnterStage(0);
    }
	
	// Update is called once per frame
	void Update () {
        if(!stage || end) {
            return;
        }
        int checkNum = 0;
        roleCtrl lastOne = null;
        roleCtrl[] roleCtrls = stage.GetComponentsInChildren<roleCtrl>();
        for(int i = 0; i < roleCtrls.Length; i++)
        {
            if(roleCtrls[i].check)
            {
                checkNum += 1;
            }
            if(roleCtrls[i].lastOne)
            {
                lastOne = roleCtrls[i];
            }
        }
        if(checkNum != roleCtrls.Length)
        {
            if(checkNum > 0)
            {
                GameObject[] circles = GameObject.FindGameObjectsWithTag("circle");
                if(circles.Length == 0)
                {
                    end = true;
                    Invoke("ResetStage", changeDelay);
                }
            }
            return;
        }
        end = true;
       if (lastOne) {
            camFol.newFollow = lastOne.gameObject;
        }
        Invoke("NextStage", changeDelay);
    }

    public void ResetStage()
    {
        if (stage)
        {
            Destroy(stage);
            stage = null;
        }
        EnterStage(stageIdx);
    }

    public void EnterStage(int idx)
    {
        camFol.newFollow = null;
        camFol.t = 0;
        end = false;
        text.text = "第" + (idx + 1) + "关";
        stage = Instantiate(stagePrefabs[idx]);
        stage.transform.parent = transform;
        stage.SetActive(true);
    }

    public void NextStage() {
        if (stage)
        {
            Destroy(stage);
            stage = null;
        }
        stageIdx += 1;
        if (stagePrefabs.Count > stageIdx) {
            EnterStage(stageIdx);
        }
    }

}
