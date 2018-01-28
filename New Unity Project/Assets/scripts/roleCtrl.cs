using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class roleCtrl : MonoBehaviour {
    public int type = 1; 
    public bool main = false; // 是否主角
    public bool lastOne = false; // 是否是下一关的主角
    public bool boss = false;
    public int inputTimes = 1; // 输入次数
    public int maxInputTimes;
    public int outputTimes = 1; // 输出次数
    public int maxOutputTimes;
    public float outputDelay = 2.0f;// 输出时间间隔
    public int circleRadius = 15; // 输出的圈消失时的半径
    public TextMesh textmesh;
    public SpriteRenderer sp;
    public List<Sprite> splist;
    public GameObject bg;
    public TextMesh sayText;
    // Use this for initialization
    void Start () {
        bg.SetActive(false);
        sayText.gameObject.SetActive(false);
        switch (type) {
            case 1:
                textmesh.text = "家里蹲";
                sp.sprite = splist[0];
                inputTimes = 1; 
                outputTimes = 2;
                circleRadius = 8;
                break;
            case 2:
                textmesh.text = "上班族";
                sp.sprite = splist[1];
                inputTimes = 1;
                outputTimes = 1;
                circleRadius = 16;
                break;
            case 3:
                textmesh.text = "老顽固";
                sp.sprite = splist[2];
                inputTimes = 7;
                outputTimes = 7;
                circleRadius = 12;
                break;
            case 4:
                textmesh.text = "大V";
                sp.sprite = splist[3];
                inputTimes = 13;
                outputTimes = 1;
                circleRadius = 80;
                break;
            case 5:
                textmesh.text = "疯子";
                sp.sprite = splist[4];
                inputTimes = 1;
                outputTimes = 1;
                circleRadius = 2;
                break;
        }
        maxInputTimes = inputTimes;
        maxOutputTimes = outputTimes;
        if (main)
        {
            keyboardCtrl ctrl = this.gameObject.GetComponentInChildren<keyboardCtrl>();
            ctrl.enabled = true;
            ctrl.gameObject.tag = "mainrole";
            inputTimes = 0;
        } else
        {
            if (maxInputTimes > 0)
            {
                sayText.gameObject.SetActive(true);
                sayText.text = "0 / " + maxInputTimes;
            }
        }
    }
    public keyboardCtrl GetKCtrl()
    {
        return this.gameObject.GetComponentInChildren<keyboardCtrl>();
    }
    // Update is called once per frame
    void Update ()
    {
        GameObject camObj = GameObject.FindGameObjectWithTag("MainCamera");
        cameraFollow camFol = camObj.GetComponent<cameraFollow>();
        Transform camTrans = camObj.transform;
        transform.position = new Vector3(transform.position.x, 0, transform.position.z);
        transform.LookAt(camTrans.position + camTrans.forward * 1000);
        GameObject[] circles = GameObject.FindGameObjectsWithTag("circle");
        for(int i = 0; i < circles.Length; i++)
        {
            circleCtrl ctrl = circles[i].GetComponent<circleCtrl>();
          //  Debug.Log("role:" + ctrl.roleID + "," + gameObject.GetInstanceID());
            if (ctrl.roleID == gameObject.GetInstanceID())
            {
                continue;
            }
            if (ctrl.collideList.Contains(gameObject.GetInstanceID())) {
                continue;
            }
            float dis = Vector3.Distance(transform.position, ctrl.transform.position);
           // Debug.Log(dis + "," + ctrl.r);
            if (dis < ctrl.r + 1.0)
            {
                ctrl.collideList.Add(gameObject.GetInstanceID());
                Jump();
                camFol.tweenToNext(gameObject);
                if (ctrl.boss)
                {
                    GetKCtrl().circlePrefab = defines.circlePrefab;
                    outputTimes = 2;
                    beginEmitJump();
                    continue;
                }
                if (inputTimes <= 0)
                {
                    continue;
                }
                inputTimes -= 1;
                sayText.text = (maxInputTimes - inputTimes) + " / " + maxInputTimes;
                if (inputTimes <= 0 && outputTimes > 0)
                {
                    beginEmitJump();
                }
            }
        }
    }
    public void beginEmitNoJump()
    {
        beginEmit(false);
    }

    public void beginEmitJump()
    {
        beginEmit(true);
    }

    void beginEmit(bool jump)
    {
        if(outputTimes <= 0)
        {
            return;
        }
        outputTimes -= 1;
        keyboardCtrl kCtrl = this.gameObject.GetComponentInChildren<keyboardCtrl>();
        kCtrl.emitCircle(circleRadius, jump);
        Invoke("beginEmitJump", outputDelay);
    }

    public void Say(int gameType, bool jump)
    {
        DataTuple tuple = defines.datas[gameType];
        string str = tuple.a;
        if (tuple.c.Length > 0) str = defines.boss ? tuple.b : tuple.c;
        bg.SetActive(true);
        sayText.gameObject.SetActive(true);
        sayText.text = str;
        Invoke("SayEnd", 2);
        if (!jump)
        {
            return;
        }
        Jump();
    }

    public void Jump()
    {

        keyboardCtrl kCtrl = this.gameObject.GetComponentInChildren<keyboardCtrl>();
        if (kCtrl.ySpd >= 0)
        {
            kCtrl.ySpd = 1.0f;
        }
    }

    void SayEnd()
    {
        bg.SetActive(false);
        sayText.gameObject.SetActive(false);
    }

    public void bossEmit()
    {
        keyboardCtrl kCtrl = this.gameObject.GetComponentInChildren<keyboardCtrl>();
        kCtrl.emitCircle(circleRadius, false);
        Invoke("bossEmit", 1.0f);
    }
}
