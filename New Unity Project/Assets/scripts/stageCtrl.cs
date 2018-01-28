using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class stageCtrl : MonoBehaviour {
    public cameraFollow camFol;
    public GameObject stage;
    public List<GameObject> stagePrefabs;
    public List<GameObject> stagePrefabs2;
    public List<GameObject> stagePrefabCur;
    public int stageIdx = 0;
    public bool end = false;
    public Text text;
    public int changeDelay = 3;
    public List<Button> btns;
    public int gameType = -1;
    public Image blackBg;
    public float fadeSpd = 0;
    public float fadeSpdConstant;
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
        fadeSpdConstant = 1.0f;
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
                    Invoke("FadeAndEnterStage", changeDelay);
                }
            }
            return;
        }
        end = true;
       if (lastOne) {
            camFol.newFollow = lastOne.gameObject;
        }
        stageIdx += 1;
        Invoke("FadeAndEnterStage", changeDelay);
    }

    public void FadeAndEnterStage()
    {
        blackBg.enabled = true;
        fadeSpd = fadeSpdConstant;
        blackBg.color = new Color(blackBg.color.r, blackBg.color.g, blackBg.color.b, 0);
    }

    public void OnFadeOut()
    {
        Debug.Log("OnFadeOut");
        fadeSpd = -fadeSpdConstant;
        blackBg.color = new Color(blackBg.color.r, blackBg.color.g, blackBg.color.b, 1.0f);
        EnterStage();
        Invoke("OnFadeIn", changeDelay);
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
        camFol.newFollow = null;
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
