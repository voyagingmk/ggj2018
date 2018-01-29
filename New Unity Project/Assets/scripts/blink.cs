using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class blink : MonoBehaviour {

	// Use this for initialization
	void Start () {
        Text t = GetComponent<Text>();
        Sequence sequence = DOTween.Sequence();
        sequence.Append(t.DOColor(new Color(0, 0, 0, 0), 0.5f));
        sequence.Append(t.DOColor(new Color(0, 0, 0, 1), 0.5f));
        sequence.SetLoops(-1);
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
