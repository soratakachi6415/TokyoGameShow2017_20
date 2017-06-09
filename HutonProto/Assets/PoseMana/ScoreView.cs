using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreView : MonoBehaviour
{

    public Sprite[] _numimage;
    public List<int> number = new List<int>();
    
    private Image _score;
    private float r, g, b, alpha;

    void Start()
    {
       _score = GameObject.Find("ScoreImage").GetComponent<Image>();
        r = _score.GetComponent<Image>().color.r;
        g = _score.GetComponent<Image>().color.g;
        b = _score.GetComponent<Image>().color.b;
        alpha = _score.GetComponent<Image>().color.a;
    }

    void Update()
    {
        _score.GetComponent<Image>().color = new Color(r, g, b, alpha);
        alpha = 0;
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
                scoreimage.localPosition.x - scoreimage.localScale.x - 5 * i,
                scoreimage.localPosition.y,
                scoreimage.localPosition.z);
            scoreimage.GetComponent<Image>().sprite = _numimage[number[i]];
        }
    }
}
