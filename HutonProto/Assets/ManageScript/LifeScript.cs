using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeScript : MonoBehaviour {

    public int lifeDownTime_sec;  //ライフが減り始める時間

    
	// Use this for initialization
	void Start () {
        lifeDownTime_sec *= 60;
    }
	
	// Update is called once per frame
	void Update () {

	}
}
