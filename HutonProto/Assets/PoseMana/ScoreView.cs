using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreView : MonoBehaviour
{

    public Sprite[] _numimage;
    public List<int> number = new List<int>();

    // フェード関連
    private Image _score;
    private float r, g, b, alpha;
    private float _fiTime; // フェードインに使う時間
    private float _foTime; // フェードアウトで使う時間
    private float _fadeTime; // フェードでかかる時間

    void Start()
    {
        _score = GameObject.Find("ScoreImage").GetComponent<Image>();
        r = _score.GetComponent<Image>().color.r;
        g = _score.GetComponent<Image>().color.g;
        b = _score.GetComponent<Image>().color.b;
        alpha = _score.GetComponent<Image>().color.a;
    }

    public void View(int score)
    {
        _score.GetComponent<Image>().color = new Color(r, g, b, alpha);

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
            var scorePrefab = (GameObject)Resources.Load("Prefab/ScoreImage");
            RectTransform scoreimage = (RectTransform)Instantiate(GameObject.Find("ScoreImage")).transform;
            scoreimage.SetParent(this.transform, false);
            scoreimage.localPosition = new Vector3(
                scoreimage.localPosition.x - scoreimage.localScale.x - 5 * i,
                scoreimage.localPosition.y,
                scoreimage.localPosition.z);
            scoreimage.GetComponent<Image>().sprite = _numimage[number[i]];
        }
    }

    public void TotalView(int score)
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

        GameObject.Find("ScoreImage").GetComponent<Image>().sprite = _numimage[number[0]];
        for (int i = 0; i < number.Count; i++)
        {
            // 複製
            var scorePrefab = (GameObject)Resources.Load("Prefab/ScoreImage");
            RectTransform scoreimage = (RectTransform)Instantiate(scorePrefab).transform;
            scoreimage.SetParent(this.transform, false);
            scoreimage.localPosition = new Vector3(
                scoreimage.localPosition.x - scoreimage.localScale.x - 5 * i,
                scoreimage.localPosition.y,
                scoreimage.localPosition.z + 15f);
            scoreimage.GetComponent<Image>().sprite = _numimage[number[i]];
        }
    }

    public void FadeOut()
    {
        _foTime -= Time.deltaTime;
        float alpha = _foTime / _fadeTime;
        var color = _score.color;
        color.a = alpha;
        _score.color = color;
    }

    public void FadeIn()
    {
        _fiTime += Time.deltaTime;
        float alpha = _fiTime / _fadeTime;
        var color = _score.color;
        color.a = alpha;
        _score.color = color;
    }
}
