using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplayNextbutton : MonoBehaviour {
    //ゲームオブジェクトを最初消しておいて
    //一定時間後に表示させる

    //表示させるまでの時間指定
    public float Displaytime;

    //一定時間消しておきたいオブジェクト
    public GameObject Displayobj;

	void Start () {
        Displayobj.SetActive(false);
	}
	
	void Update () {
        if (Displaytime > 0)
        {
            Displaytime -= 1.0f * Time.deltaTime;
        }
        //条件を満たしたら表示する関数を呼び出す
        if (Displaytime <= 0)
        {
            OnDisplay();
        }
	}

    //表示する
    void OnDisplay()
    {
        Displayobj.SetActive(true);
    }
}
