using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScript : MonoBehaviour
{
    List<GameObject> obj = new List<GameObject>();
    List<GameObject> Initialize_Parts = new List<GameObject>();
    private SleepGageScript sleepGauge;
    //敵と衝突したか
    private bool hit;
    //難易度変動時に体と手足が連動して動くか？
    public bool is_activeArmLeg;
    //速度制限の有効時間
    private float checkTime;
    //速度制限測定用座標
    private Vector3 fromPosition;
    private Vector3 toPosition;
    //タッチ強制解除
    public bool touchCancel;
    void Start()
    {
        hit = false;
        sleepGauge = GameObject.Find("ScriptController").GetComponent<SleepGageScript>();
        /*新プレイヤーモデル*/
        obj.Add(GameObject.Find("Player_mixamorig:LeftUpLeg"));     //0
        obj.Add(GameObject.Find("Player_mixamorig:LeftLeg"));       //1
        obj.Add(GameObject.Find("Player_mixamorig:RightUpLeg"));    //2
        obj.Add(GameObject.Find("Player_mixamorig:RightLeg"));      //3
        obj.Add(GameObject.Find("Player_mixamorig:LeftForeArm"));   //4
        obj.Add(GameObject.Find("Player_mixamorig:LeftArm"));       //5
        obj.Add(GameObject.Find("Player_mixamorig:RightForeArm"));  //6
        obj.Add(GameObject.Find("Player_mixamorig:RightArm"));      //7
        obj.Add(GameObject.Find("Player_mixamorig:Spine"));         //8
        obj.Add(GameObject.Find("Player_mixamorig:Head"));          //9
        obj.Add(GameObject.Find("Player_mixamorig:Spine1"));        //10
        obj.Add(GameObject.Find("Player_mixamorig:Spine2"));        //11
        obj.Add(GameObject.Find("Player_mixamorig:Hips"));          //12
        obj.Add(GameObject.Find("Player_mixamorig:LeftShoulder"));  //13
        obj.Add(GameObject.Find("Player_mixamorig:RightShoulder")); //14
    }

    void OnMouseDown()
    {
        for(int i = 0;i < obj.Count; i++)    
        {
            obj[i].GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
        }

        if (gameObject.name.ToString().Contains("Leg")) //触れたオブジェクトと関連するY座標だけ固定する
        {
            if (gameObject.name.ToString().Contains("Left"))
            {
                if(gameObject.name.ToString().Contains("Up")) obj[0].GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePositionY;
                obj[1].GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePositionY;
            }
            else if (gameObject.name.ToString().Contains("Right"))
            {
                if (gameObject.name.ToString().Contains("Up")) obj[2].GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePositionY;
                obj[3].GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePositionY;
            }
        }
        else if (gameObject.name.ToString().Contains("Arm"))
        {
            if (gameObject.name.ToString().Contains("Left"))
            {
                obj[4].GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePositionY;
                if(!gameObject.name.ToString().Contains("Fore"))obj[5].GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePositionY;
            }
            else if (gameObject.name.ToString().Contains("Right"))
            {
                obj[6].GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePositionY;
                if (!gameObject.name.ToString().Contains("Fore")) obj[7].GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePositionY;
            }
        }
        else if (gameObject.name.ToString().Contains("Spine") || gameObject.name.ToString().Contains("Head"))
        {//体か頭を触ると全身の回転とY座標を固定。
            //ボディを触るとコネクトボディをくっつける
            obj[0].GetComponent<HingeJoint>().connectedBody = obj[12].GetComponent<Rigidbody>();
            obj[2].GetComponent<HingeJoint>().connectedBody = obj[12].GetComponent<Rigidbody>();
            obj[5].GetComponent<HingeJoint>().connectedBody = obj[13].GetComponent<Rigidbody>();
            obj[7].GetComponent<HingeJoint>().connectedBody = obj[14].GetComponent<Rigidbody>();
            obj[8].GetComponent<HingeJoint>().connectedBody = obj[12].GetComponent<Rigidbody>();
            for (int i = 0; i < obj.Count; i++)
            {
                obj[i].GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezeRotation;
            }
        }

        //上半身と頭と肩を固定
        obj[8].GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezeRotation;
        obj[9].GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezeRotation;
        obj[10].GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezeRotation;
        obj[11].GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezeRotation;
        obj[13].GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezeRotation;
        obj[14].GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezeRotation;

        //チェックタイム,タッチ強制解除,座標などの初期化
        checkTime = 0.05f;        
        fromPosition = gameObject.transform.position;
        touchCancel = false;
    }
    
    void OnMouseDrag()
    {
        //プレイヤー操作時の移動速度に制限
        //SpeedCheck();

        //プレイヤーの間接操作時に制限
        DistanceLock();
    }

    void OnMouseUp()
    {
        //特定のジョイントのコネクトボディを剥がす
        obj[0].GetComponent<HingeJoint>().connectedBody = null;
        obj[2].GetComponent<HingeJoint>().connectedBody = null;
        obj[5].GetComponent<HingeJoint>().connectedBody = null;
        obj[7].GetComponent<HingeJoint>().connectedBody = null;
        obj[8].GetComponent<HingeJoint>().connectedBody = null;
        //オブジェクトを離すと全てのオブジェクトをY座標固定のみへ
        for (int i = 0; i < obj.Count; i++)
        {
            if (obj[i].GetComponent<Rigidbody>().constraints == RigidbodyConstraints.FreezeRotation
                || obj[i].GetComponent<Rigidbody>().constraints == RigidbodyConstraints.FreezeRotationY
                || obj[i].GetComponent<Rigidbody>().constraints == RigidbodyConstraints.FreezeAll)
            {
                obj[i].GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePositionY;
            }
        }
        obj[12].GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePositionY;
    }
    void Update()
    {

    }

    /*プレイヤーの移動速度を確認し、速度超過で移動を制限
    プレイヤーを素早く移動させた時にモデルがぐちゃぐちゃ
    になりゲームが進行不能になるのを未然に防ぐため*/
    private void SpeedCheck()
    {
        //速度超過でreturn
        if (touchCancel) return;


        if (checkTime < 0.0f)
        {
            //0.05秒経った後の座標を取得
            toPosition = gameObject.transform.position;

            if (Vector3.Distance(fromPosition, toPosition) >= 4.0f)
            {//0.05秒で座標が4.0以上変化していたら制限
                touchCancel = true;
                Debug.Log("速度超過！初期座標は" + fromPosition + "。0.05秒で移動した座標は" + toPosition + "。");
            }
            else
            {//移動量が4.0未満なら初期化
                fromPosition = gameObject.transform.position;
                checkTime = 0.05f;
            }
        }
        checkTime -= Time.deltaTime;
    }

    /*腕や足の2パーツ(計4セット)が一定距離離れたときタッ
    チ移動を強制解除する
    さらに、肩と腕、太腿とお尻の距離も判定し同様の条件
    でタッチ移動を強制解除する

    こちらもゲーム進行不能を未然に防ぐため*/
    private void DistanceLock()
    {
        //リスト番号格納
        int i = 0, j = 0;
        List<int> k = new List<int>();
        List<int> m = new List<int>();

        /*このプログラムは難易度ハードの時に
        「体を動かすと手足も動くプログラム」
        の妨害になる可能性あり↓*/
        //constraintsがY座標固定になっているパーツを2つ抽出
        for (int ii = 0;ii < obj.Count; ii++)
        {
            if (obj[ii].GetComponent<Rigidbody>().constraints == RigidbodyConstraints.FreezePositionY)
            {
                if (i == 0) i = ii;
                else if(j == 0) j = ii;
            }
        }
        

        //抽出した2パーツの距離を判定
        if (obj[i].name.ToString().Contains("Leg") && obj[j].name.ToString().Contains("Leg"))
        {//脚
            if (Vector3.Distance(obj[i].transform.position, obj[j].transform.position) >= 18.0f)
            {//指定距離離れていたらタッチ解除
                //Debug.Log("脚の関節間距離が許容範囲を超えました。" +  obj[i].name + obj[i].transform.position + " " + obj[j] + obj[j].transform.position);
                touchCancel = true;
            }
        }
        if (Vector3.Distance(obj[4].transform.position, obj[5].transform.position) >= 11.0f)
        {//指定距離離れていたらタッチ解除
         //Debug.Log("腕の関節間距離が許容範囲を超えました");
            touchCancel = true;
        }
        if (Vector3.Distance(obj[6].transform.position, obj[7].transform.position) >= 11.0f)
        {//指定距離離れていたらタッチ解除
         //Debug.Log("腕の関節間距離が許容範囲を超えました");
            touchCancel = true;
        }



        //モデルの仕様上ゲーム進行不能になり得るパーツを抽出
        for (int ii = 0; ii < obj.Count; ii++)
        {
            if (obj[ii].name.ToString().Contains("UpLeg"))
            {
                k.Add(ii);
            }
            else if(obj[ii].name.ToString().Contains("RightArm"))
            {
                m.Add(ii);
            }
            else if(obj[ii].name.ToString().Contains("LeftArm"))
            {
                m.Add(ii);
            }
        }

        //念の為ソート
        k.Sort();
        m.Sort();

        /*脚と足の付け根の距離を判定*/
        for (int ii = 0; ii < k.Count; ii++)
        {
            if (Vector3.Distance(obj[k[ii]].transform.position, obj[12].transform.position) >= 6.0f)
            {//指定距離離れていたらタッチ解除
                //Debug.Log("腰と足の付け根関節間の距離が許容範囲を超えました");
                touchCancel = true;
            }
        }

        /*腕と肩付近の距離を判定*/
        if (Vector3.Distance(obj[7].transform.position, obj[14].transform.position) >= 8.0f)
        {//指定距離離れていたらタッチ解除
            //Debug.Log("右腕と右肩の関節間距離が許容範囲を超えました");
            touchCancel = true;
        }
        else if (Vector3.Distance(obj[5].transform.position, obj[13].transform.position) >= 8.0f)
        {//指定距離離れていたらタッチ解除
            //Debug.Log("左腕と左肩の関節間距離が許容範囲を超えました");
            touchCancel = true;
        }
    }

    void OnCollisionStay(Collision other)
    {
        //敵との衝突判定
        if (other.collider.tag == "Enemy" || other.collider.tag == "Enemy2")
        {
            if (!hit)
            {
                //衝突時快眠ポイントを減らす
                sleepGauge.hitEnemy(true);
            }
            hit = true;
        }
    }

    void OnCollisionExit(Collision other)
    {
        hit = false;
    }
}
