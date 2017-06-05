using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeScript : MonoBehaviour {

    public float time_sec;

	// Use this for initialization
	public void Start () {
	}
	
	// Update is called once per frame
	public void Update () {
        time_sec -= Time.deltaTime;

        if (time_sec < 0) time_sec = 0;
	}
}
