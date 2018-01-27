using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class stageCtrl : MonoBehaviour {
    public GameObject stage;
    public List<GameObject> stagePrefabs;
    public int stageIdx = 0;
    public bool end = false;
    public Text text;
    public int changeDelay = 3;
    // Use this for initialization
    void Start () {
        stage = null;
        EnterStage(0);
    }
	
	// Update is called once per frame
	void Update () {
        if(!stage || end) {
            return;
        }
        int checkNum = 0;
        roleCtrl[] roleCtrls = stage.GetComponentsInChildren<roleCtrl>();
        for(int i = 0; i < roleCtrls.Length; i++)
        {
            if(roleCtrls[i].check)
            {
                checkNum += 1;
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
