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
    // Use this for initialization
    void Start () {
        foreach(GameObject s in stagePrefabs)
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
