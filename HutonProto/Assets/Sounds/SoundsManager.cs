using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundsManager : MonoBehaviour {

    public AudioClip sheepCry;
    public AudioClip walkFloor;
    private AudioClip audioClip;

    private AudioSource audioSource;

	// Use this for initialization
	void Start () {
        audioSource = GetComponent<AudioSource>();
    }
	
	// Update is called once per frame
	void Update () {
	}

    //羊が消えたとき(敵エネミーに殴られたとき)
    public void SheepCry()
    {
        audioSource.PlayOneShot(sheepCry);
    }

    //敵エネミ―が再出現するとき
    public void WalkFloor()
    {
        audioSource.PlayOneShot(walkFloor);
    }
}
