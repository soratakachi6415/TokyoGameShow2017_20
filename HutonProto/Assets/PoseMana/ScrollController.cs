using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScrollController : MonoBehaviour {

    [SerializeField]
    RectTransform prefab = null;

	// Use this for initialization
	void Start () {

		for(int i=0; i<13; i++)
        {
            var item = GameObject.Instantiate(prefab) as RectTransform;
            item.SetParent(transform, false);

            var text = item.GetComponentInChildren<Text>();
            text.text = "item:" + i.ToString();
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
