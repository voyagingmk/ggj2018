using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class stageCtrl : MonoBehaviour {
    public cameraFollow camFol;
    public GameObject stage;
    public List<GameObject> stagePrefabs;
    public int stageIdx = 0;
    public bool end = false;
    public Text text;
    public int changeDelay = 3;
    public List<Button> btns;
    public int gameType = -1;
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
        InitBtns();
        foreach (GameObject s in stagePrefabs)
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
