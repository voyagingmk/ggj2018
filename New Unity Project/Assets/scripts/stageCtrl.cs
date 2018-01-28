using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class stageCtrl : MonoBehaviour {
    public cameraFollow camFol;
    public GameObject stage;
    public GameObject roleBoss;
    public List<GameObject> stagePrefabs;
    public List<GameObject> stagePrefabs2;
    public List<GameObject> stagePrefabCur;
    public int stageIdx = 0;
    public bool end = false;
    public Text text;
    public List<Button> btns;
    public int gameType = -1;
    public Image blackBg;
    public float fadeSpd = 0;
    void InitBtns()
    {
        int i = 0;
        foreach (Button b in btns)
        {
            buttonCtrl ctrl = b.gameObject.AddComponent<buttonCtrl>();
            ctrl.type = i;
            b.GetComponentInChildren<Text>().text = defines.datas[i].a;
            b.onClick.AddListener(delegate () {
                this.OnEnterStage(b);
            });
            i++;
        }
    }
    // Use this for initialization
    void Start () {
        roleBoss.SetActive(false);
        blackBg.enabled = false;
        blackBg.color = new Color(blackBg.color.r, blackBg.color.g, blackBg.color.b, 0);
        InitBtns();
        foreach (GameObject s in stagePrefabs)
        {
            s.SetActive(false);
        }
        foreach (GameObject s in stagePrefabs2)
        {
            s.SetActive(false);
        }
        stage = null;
    }

    public void OnEnterStage(Button btn)
    {
        buttonCtrl ctrl = btn.GetComponent<buttonCtrl>();
        Debug.Log(ctrl.type + ":" + btn.GetComponentInChildren<Text>().text);
        gameType = ctrl.type;
        btns[0].transform.parent.gameObject.SetActive(false);
        stagePrefabCur = gameType != 14 ? stagePrefabs : stagePrefabs2;
        FadeAndEnterStage();

    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.Home))
        {
            Application.Quit();
        }
        float oldAlpha = blackBg.color.a;
        blackBg.color = new Color(blackBg.color.r, blackBg.color.g, blackBg.color.b, oldAlpha + fadeSpd * Time.deltaTime);
      //  Debug.Log("blackBg.color.a " + blackBg.color.a);
        if (oldAlpha < 1.0f && blackBg.color.a >= 1.0f)
        {
            OnFadeOut();
        }

        if (!stage || end) {
            return;
        }
        int checkNum = 0;
        roleCtrl lastOne = null;
        roleCtrl[] roleCtrls = stage.GetComponentsInChildren<roleCtrl>();
        for(int i = 0; i < roleCtrls.Length; i++)
        {
            if(roleCtrls[i].inputTimes == 0)
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
            // 不是全部人都被激活，开始检测失败
            if(checkNum > 0)
            {
                int outputCount = 0;
                for (int i = 0; i < roleCtrls.Length; i++)
                {
                    outputCount += roleCtrls[i].outputTimes;
                }  
                if(outputCount == 0)
                { 
                    // 输出都为0了，失败了
                    end = true;
                    Invoke("FadeAndEnterStage", defines.changeDelay);
                }
            }
            return;
        }

        // 胜利 checkNum == roleCtrls.Length 所有人都被激活
        end = true;
        /*
        if (lastOne) {
            camFol.tweenToNext(lastOne.gameObject);
        }*/
        stageIdx += 1;
        if (stagePrefabCur.Count <= stageIdx)
        {
            BeginLastStage();
            return;
        }
        Invoke("FadeAndEnterStage", defines.changeDelay);
    }

    public void BeginLastStage()
    {
        if (!roleBoss)
        {
            return;
        }
        roleBoss.SetActive(true);
        keyboardCtrl kCtrl = roleBoss.GetComponent<keyboardCtrl>();
        roleCtrl rCtrl = roleBoss.GetComponent<roleCtrl>();
        rCtrl.bossEmit();
    }

    public void FadeAndEnterStage()
    {
        blackBg.enabled = true;
        fadeSpd = defines.FadeSpd;
        blackBg.color = new Color(blackBg.color.r, blackBg.color.g, blackBg.color.b, 0);
    }

    public void OnFadeOut()
    {
        Debug.Log("OnFadeOut");
        fadeSpd = -defines.FadeSpd;
        blackBg.color = new Color(blackBg.color.r, blackBg.color.g, blackBg.color.b, 1.0f);
        EnterStage();
        Invoke("OnFadeIn", defines.changeDelay);
    }
    public void OnFadeIn()
    {
        blackBg.enabled = false;
    }

    public void EnterStage()
    {
        if (stage)
        {
            GameObject[] circles = GameObject.FindGameObjectsWithTag("circle");
            for (int i = 0; i < circles.Length; i++)
            {
                Destroy(circles[i]);
            }
            Destroy(stage);
            stage = null;
        }
        camFol.tweening = false;
        camFol.t = 0;
        end = false;
        if (stagePrefabCur.Count <= stageIdx)
        {
            return;
        }
        text.text = "第" + (stageIdx + 1) + "关";
        stage = Instantiate(stagePrefabCur[stageIdx]);
        stage.transform.parent = transform;
        stage.SetActive(true);
    }

}
