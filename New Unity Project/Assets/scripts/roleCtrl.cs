﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class roleCtrl : MonoBehaviour {
    public int type = 1; 
    public int main = 0; // 是否主角
    public int inputTimes = 1; // 输入次数
    public int outputTimes = 1; // 输出次数
    public int circleRadius = 15; // 输出的圈消失时的半径
    public TextMesh textmesh;
    public SpriteRenderer sp;
    public List<Sprite> splist;
    // Use this for initialization
    void Start () {
        switch (type) {
            case 1:
                textmesh.text = "上班族";
                sp.sprite = splist[0];
                inputTimes = 1; 
                outputTimes = 1;
                circleRadius = 15;
                break;
            case 2:
                textmesh.text = "家里蹲";
                sp.sprite = splist[1];
                inputTimes = 1;
                outputTimes = 1;
                circleRadius = 15;
                break;
            case 3:
                textmesh.text = "老顽固";
                sp.sprite = splist[2];
                inputTimes = 1;
                outputTimes = 1;
                circleRadius = 15;
                break;
            case 4:
                textmesh.text = "疯子";
                sp.sprite = splist[3];
                inputTimes = 1;
                outputTimes = 1;
                circleRadius = 15;
                break;
            case 5:
                textmesh.text = "大V";
                sp.sprite = splist[4];
                inputTimes = 1;
                outputTimes = 1;
                circleRadius = 15;
                break;
        }
        if(main == 1)
        {
            keyboardCtrl ctrl = this.gameObject.GetComponentInChildren<keyboardCtrl>();
            ctrl.enabled = true;
            ctrl.gameObject.tag = "mainrole";
        }

	}
	
	// Update is called once per frame
	void Update () {
        Transform camTrans = GameObject.FindWithTag("MainCamera").transform;
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
            if (inputTimes <= 0 || outputTimes <= 0)
            {
                continue;
            }
            if (ctrl.collideList.Contains(gameObject.GetInstanceID())) {
                continue;
            }
            float dis = Vector3.Distance(transform.position, ctrl.transform.position);
           // Debug.Log(dis + "," + ctrl.r);
            if (dis < ctrl.r)
            {
                ctrl.collideList.Add(gameObject.GetInstanceID());
                inputTimes -= 1;
                if (outputTimes > 0)
                {
                    outputTimes -= 1;
                    keyboardCtrl keyboardCtrl = this.gameObject.GetComponentInChildren<keyboardCtrl>();
                    keyboardCtrl.emitCircle(circleRadius);
                }
            }
        }
    }
}
