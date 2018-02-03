using DG.Tweening;
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
    public GameObject circlePrefab = null;
    public List<GameObject> stagePrefabCur;
    public int stageIdx = 0;
    public bool end = false;
    public Text text;
    public List<Button> btns;
    public int gameType = -1;
    public Image blackBg;
    public float fadeSpd = 0;
    public Text subtitleZh;
    public Text subtitleEn;
    public Text clickToStart;
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
        defines.circlePrefab = circlePrefab;
        roleBoss.SetActive(false);
        blackBg.enabled = false;
        blackBg.color = new Color(blackBg.color.r, blackBg.color.g, blackBg.color.b, 0);
        InitBtns();
        foreach (GameObject s in stagePrefabs)
        {
            s.SetActive(false);
        }
        if (stagePrefabs2.Count == 0) stagePrefabs2 = stagePrefabs;
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
        clickToStart.gameObject.SetActive(false);
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
        if (oldAlpha > 0.0f && blackBg.color.a <= 0.0f)
        {
            OnFadeIn();
        }

		if (Input.GetKeyDown (KeyCode.F3)){

			btns [14].gameObject.SetActive (true);
			return;
		}
        if (!stage || end) {
            return;
        }
        int checkNum = 0;
        roleCtrl main = null;
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
            if (roleCtrls[i].main)
            {
                main = roleCtrls[i];
            }
            if (roleCtrls[i].boss)
            {
                main = roleCtrls[i];
            }
        }

		if (Input.GetKeyDown(KeyCode.F1))
		{
			end = true;
			FadeAndEnterStage();
			return;
		}
		if (Input.GetKeyDown(KeyCode.F2))
        {
            defines.changeDelay = 1.0f;
            Win(lastOne);
            return;
        }
        GameObject[] circles = GameObject.FindGameObjectsWithTag("circle");
        //
        if (!main.GetKCtrl().hasPress)
        {
            return;
        }
       // Debug.Log("check " + (checkNum < roleCtrls.Length)  + (circles.Length == 0));
        // 失败检测
        if (checkNum < roleCtrls.Length && circles.Length == 0)
        {
            int outputing = 0;
            for (int i = 0; i < roleCtrls.Length; i++)
            {
                outputing += (roleCtrls[i].outputTimes != roleCtrls[i].maxOutputTimes && roleCtrls[i].outputTimes > 0) ?1:0;
            }
           // Debug.Log("outputing " + outputing);
            if (outputing == 0)
            { 
                // 没人在输出
                end = true;
                Invoke("FadeAndEnterStage", defines.changeDelay);
            }
            return;
        }

        // 都被激活了
        if (checkNum == roleCtrls.Length)
        {
            Win(lastOne);
        }
    }

    public void Win(roleCtrl lastOne)
    {
        end = true;
        if (lastOne)
        {
            camFol.lastOne = lastOne.gameObject;
        }
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
		if (!roleBoss) {
			return;
		}
		GameObject[] circles = GameObject.FindGameObjectsWithTag ("circle");
		for (int i = 0; i < circles.Length; i++) {
			Destroy (circles [i]);
		}
		roleCtrl[] roles = gameObject.GetComponentsInChildren<roleCtrl>();
		for (int i = 0; i < roles.Length; i++) {
			roleCtrl r = roles [i];
			r.outputTimes = r.maxOutputTimes;
			r.inputTimes = r.maxInputTimes;
			r.CancelInvoke ();
			r.SayEnd ();
		} 
        defines.boss = true;
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
    }

	// 关卡黑幕消失时调用，关卡淡入
    public void OnFadeIn()
    {
        blackBg.enabled = false;
        // 开始播放字幕
        if (defines.Subtitles.ContainsKey(stageIdx))
        {
            List<Subtitle> lst = defines.Subtitles[stageIdx];
            foreach(Subtitle s in lst)
            {
                StartCoroutine(ShowSubTitle(s.zh, s.en, s.begin, s.end));
            }
        }
    }

	public void ShowCircleSubTitile() {
		if (defines.Subtitles1.ContainsKey(stageIdx))
		{
			defines.Subtitles[stageIdx]= null;
			List<Subtitle> lst = defines.Subtitles1[stageIdx];
			foreach(Subtitle s in lst)
			{
				StartCoroutine(ShowSubTitle(s.zh, s.en, s.begin, s.end));
			}
		}
	
	}

	public void ShowBossSubTitile() {
		if (defines.Subtitles2.ContainsKey(stageIdx))
		{
			//defines.Subtitles1[stageIdx]= null;
			List<Subtitle> lst = defines.Subtitles2[stageIdx];
			foreach(Subtitle s in lst)
			{
				StartCoroutine(ShowSubTitle(s.zh, s.en, s.begin, s.end));
			}
		}

	}

    public IEnumerator ShowSubTitle(string zh, string en, int begin, int end)
    {
        Debug.Log(zh + "," + en + "," + begin + "," + end);
        if (begin > 0)
        {
            yield return new WaitForSeconds(begin);
        }
		string zhNew = zh.Replace("[XXX]", defines.datas[gameType].a);
		zhNew = zhNew.Replace("[YYY]", defines.datas[gameType].b);
		string enNew = en.Replace("[XXX]", defines.datas[gameType].a);
		enNew = enNew.Replace("[YYY]", defines.datas[gameType].b);
        // 显示字幕
		subtitleZh.text = zhNew;
		subtitleEn.text = enNew;
        subtitleZh.DOColor(new Color(0, 0, 0, 1), defines.SubtitleFadeTime);
        subtitleEn.DOColor(new Color(0, 0, 0, 1), defines.SubtitleFadeTime);
        yield return new WaitForSeconds(end - begin);
        // 隐藏字幕 
        subtitleZh.DOColor(new Color(0, 0, 0, 0), defines.SubtitleFadeTime);
        subtitleEn.DOColor(new Color(0, 0, 0, 0), defines.SubtitleFadeTime);
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
        camFol.lastOne = null;
        camFol.tweening = false;
        camFol.t = 0;
        end = false;
        if (stagePrefabCur.Count <= stageIdx)
        {
            return;
        }
        text.text = "第 " + (stageIdx + 1) + " 关";
        stage = Instantiate(stagePrefabCur[stageIdx]);
        stage.transform.parent = transform;
        stage.SetActive(true);
    }

}
