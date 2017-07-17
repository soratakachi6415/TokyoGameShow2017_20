using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreView : MonoBehaviour
{
    public PoseManager _pose_manager;

    public GameObject _lSholder;
    public GameObject _rcrotch;

    public Sprite[] _numimage;
    public List<int> number = new List<int>();

    public static bool _clear;

    void Start()
    {
        _pose_manager = GameObject.FindGameObjectWithTag("Posemanager").GetComponent<PoseManager>();

        // ここの取得は後で変更
        _lSholder = GameObject.Find("Player_LeftHand1");
        _rcrotch = GameObject.Find("Player_RightLeg1");
    }

    void Update()
    {
        
    }

    public void View(int score)
    {
        var objs = GameObject.FindGameObjectsWithTag("Score");
        foreach (var obj in objs)
        {
            if (0 <= obj.name.LastIndexOf("Clone"))
            {
                Destroy(obj);
            }
        }

        var objes = GameObject.FindGameObjectsWithTag("Point");
        foreach (var obj in objes)
        {
            if (0 <= obj.name.LastIndexOf("Clone"))
            {
                Destroy(obj);
            }
        }

        var digit = score;
        // 要素数0には一桁目の値が格納
        number = new List<int>();
        while (digit != 0)
        {
            score = digit % 10;
            digit = digit / 10;
            number.Add(score);
        }

        GameObject.Find("ScoreImage").GetComponent<Image>().sprite = _numimage[number[0]];
        for (int i = 0; i < number.Count; i++)
        {
            // 複製
            GameObject prefab = (GameObject)Resources.Load("Prefab/ScoreImage"); 
            RectTransform scoreimage = (RectTransform)Instantiate(prefab).transform;
            scoreimage.SetParent(this.transform, false);
            scoreimage.localPosition = new Vector3(
                scoreimage.localPosition.x - 5* i,
                scoreimage.localPosition.y,
                scoreimage.localPosition.z);
            scoreimage.GetComponent<Image>().sprite = _numimage[number[i]];
        }
        GameObject point = (GameObject)Resources.Load("Prefab/Point");
        RectTransform pointimage = (RectTransform)Instantiate(point).transform;
        pointimage.SetParent(this.transform, false);
        pointimage.localPosition = new Vector3(
            pointimage.localPosition.x,
            pointimage.localPosition.y,
            pointimage.localPosition.z);

    }
}
