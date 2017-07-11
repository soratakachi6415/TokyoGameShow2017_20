using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

/// <summary> 一定値移動したら次のページに遷移するようにスクロールバーの値を変えるコンポーネントです </summary>
[RequireComponent(typeof(Scrollbar))]
public class SlideExtension : MonoBehaviour
{
    [Tooltip("分割する数"), SerializeField]
    private int       splitValue = 1;

    [Tooltip("現在のページから下の数値以上離れたら次のページに遷移")]
    [Range(0.0f, 1.0f), SerializeField]
    private float     distance;

    [Tooltip("スクロールするスピード")]
    [Range(0.0f, 60.0f), SerializeField]
    private float     scrollSpeed;

    private int       prevTouchCount;           //前フレームのタッチした(タップした)数

    private float     targetValue;              //現在のページのスクロールバーのvalueに当たる値

    private Scrollbar scroll;                   //スクロールバーコンポーネント
    private bool      isScrolls     = false;    //スクロール中か
    private bool      isEnableFrame = false;    //このコンポーネントが有効になった瞬間のフレームか？

    private bool      isDrag        = false;    //ドラッグ中か

    private int       target;                   //現在のページ



    //----- ----- -----
    // プロパティ
    //----- ----- -----

    /// <summary> 現在のページ </summary>
    public int currentPage
    {
        get { return target; }
    }

    /// <summary> 一番最初のページか </summary>
    public bool isStartPage
    {
        get { return target == 0; }
    }

    /// <summary> 一番最後のページか </summary>
    public bool isEndPage
    {
        get { return target == splitValue - 1; }
    }



    //----- ----- -----
    // 関数(public)
    //----- ----- -----

    /// <summary> 次のページに飛びます </summary>
    public void NextPage()
    {
        if (isEndPage) { return; }

        target++;
        if (isEndPage)
        {
            targetValue = 1.0f;
        }
        else
        {
            targetValue = (float)target / (float)(splitValue - 1);
        }

        isDrag    = false;
        isScrolls = true;
    }

    /// <summary> 前のページに飛びます </summary>
    public void PrevPage()
    {
        if (isStartPage) { return; }

        target--;
        if (isStartPage)
        {
            targetValue = 0.0f;
        }
        else
        {
            targetValue = (float)target / (float)(splitValue - 1);
        }
        isDrag    = false;
        isScrolls = true;
    }



    //----- ----- -----
    // 関数(private)
    //----- ----- -----

    private void OnValidate()
    {
        splitValue = Mathf.Max(splitValue, 1);
    }

    private void Awake()
    {
        scroll = GetComponent<Scrollbar>();

        float value = scroll.value;
        target = (int)Mathf.Floor(value * splitValue);

        //splitValue -> 0以下にはならない
        targetValue = target / splitValue;
    }

    private void Update()
    {
        if (CanScrollStop())
        {
            isDrag    = true;
            isScrolls = false;
        }

        if (isScrolls)
        {
            Scroll();
        }
    }

    private void LateUpdate()
    {
#if UNITY_EDITOR
        if (Input.GetMouseButtonUp(0))
        {
            if (isDrag) { CheckDistance(); }
        }
#else
        if (Input.touchCount == 0 &&
            prevTouchCount   != 0)
        {
            if (isDrag) { CheckDistance(); }
        }
#endif

        prevTouchCount = Input.touchCount;

        if (!isEnableFrame) { return; }

        target = 0;
        targetValue = 0.0f;
        scroll.value = 0.0f;

        isEnableFrame = false;
    }

    private void OnEnable()
    {
        isEnableFrame = true;
    }

    /// <summary> スクロールを止める? </summary>
    private bool CanScrollStop()
    {
        if (Input.GetMouseButtonDown(0)) { return true;  }

        if (Input.touchCount != 0)
        {
            if (prevTouchCount == 0)
            {
                return true;
            }
        }

        return false;
    }

    /// <summary> スクロールの値が一定値以上になれば次のページのフラグを立てます </summary>
    private void CheckDistance()
    {
        float dis = scroll.value - targetValue;
        if (Mathf.Abs(dis) >= distance)
        {
            target += (int)Mathf.Sign(dis);
            target = Mathf.Clamp(target, 0, splitValue - 1);
            Debug.Log(target);

            if (isStartPage)
            {
                targetValue = 0.0f;
            }
            else
            if (isEndPage)
            {
                targetValue = 1.0f;
            }
            else
            {
                targetValue = (float)target / (float)(splitValue - 1);
            }
        }
        isScrolls = true;
    }

    /// <summary> スクロールします </summary>
    private void Scroll()
    {
        scroll.value = Mathf.Lerp(scroll.value, targetValue, scrollSpeed*Time.deltaTime);

        if (Mathf.Abs(scroll.value - targetValue) < 0.001f)
        {
            scroll.value = targetValue;
            isScrolls = false;
        }
    }
}
