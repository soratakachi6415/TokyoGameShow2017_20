using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResultScore : MonoBehaviour {

    public Sprite[] _numimage;
    public List<int> number = new List<int>();

    public bool _on = false;
    // Use this for initialization
    void Start () {
        if(ScoreManager._totalscore >= 1)
        {
            View(ScoreManager._totalscore);
        }
	}
    
    // Update is called once per frame
    void Update () {

        /*リザルトで実際に表示できるか試運転*/
        //if (Input.GetKeyDown(KeyCode.X) || Input.GetKeyDown(KeyCode.Z) || Input.GetMouseButtonDown(0))
        //{
        //    var random = Random.Range(1, 100000000);
        //    /*いままで表示されてたスコアオブジェクト削除*/
        //    var objs = GameObject.FindGameObjectsWithTag("Score");
        //    foreach (var obj in objs)
        //    {
        //        if (0 <= obj.name.LastIndexOf("Clone"))
        //        {
        //            Destroy(obj);
        //        }
        //    }
        //    View(random);
        //}
    }

    public void View(int score)
    {
        var digit = score;
        // 要素数0には一桁目の値が格納
        number = new List<int>();
        while (digit != 0)
        {
            score = digit % 10;
            digit = digit / 10;
            number.Add(score);
        }

        GameObject.Find("ResultScoreImage").GetComponent<Image>().sprite = _numimage[number[0]];
        for (int i = 0; i < number.Count; i++)
        {
            // 複製
            RectTransform scoreimage = (RectTransform)Instantiate(GameObject.Find("ResultScoreImage")).transform;
            scoreimage.SetParent(this.transform, false);
            scoreimage.localPosition = new Vector3(
                scoreimage.localPosition.x - scoreimage.sizeDelta.x * i,
                scoreimage.localPosition.y,
                scoreimage.localPosition.z);
            scoreimage.GetComponent<Image>().sprite = _numimage[number[i]];
        }
    }
}
