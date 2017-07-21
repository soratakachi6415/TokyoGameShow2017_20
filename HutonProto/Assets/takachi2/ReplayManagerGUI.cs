using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using UnityEngine.UI;

[RequireComponent(typeof(ReplayManager))]
public class ReplayManagerGUI : MonoBehaviour
{
    public enum State
    {
        Initialize,
        RecoodStart,
        Recooding,
        RecoodStop,
        ReplayStart,
        Replaying,
        ReplayStop
    }

    private Scene_manager currentsce;
    private ReplayManager man;
    private Clock clock;
	private bool recorded = false;
	public State nextMode = State.Initialize ;
	private string text = "Start Recording";
	private float frame = 0;
    private float replayCoolTime;
    private Replayable replayable;
    public GameObject imageObject;

    void Awake()
    {
        //SceneManager.sceneLoaded += SceneLoaded;
    }

    // Use this for initialization
    void Start ()
	{
		man = GetComponent<ReplayManager> ();
        currentsce = GameObject.FindGameObjectWithTag("Scenemanager").GetComponent<Scene_manager>();
        clock = GameObject.FindGameObjectWithTag("Clock").GetComponent<Clock>();
        replayable = GameObject.FindGameObjectWithTag("Respawn").GetComponent<Replayable>();
    }

    //public void SceneLoaded(Scene scene, LoadSceneMode sceneMode)
    //{
    //    if (scene.name == "Result")
    //    {
    //        man.StopRecording();
    //        //recorded = true;
    //        Debug.Log("stop");

    //        man.StartReplay();
    //        text = "Replaying...";
    //        this.nextMode = State.Replaying;
    //        Debug.Log("restart");
    //    }

    //    if (scene.name == "Title")
    //    {
    //        Destroy(gameObject);
    //    }
    //}

    // Update is called once per frame
    void Update ()
	{
        if (nextMode == State.Initialize)
        {
            man.Initialize();
            Debug.Log("Initialize");
            if (currentsce.currentscene == "main")
            {
                nextMode = State.RecoodStart;
            }
        }

        if (currentsce.currentscene == "main")
        {
            if (nextMode == State.RecoodStart)
            {
                man.StartRecording();
                text = "Recording...";
                nextMode = State.Recooding;
                Debug.Log("Recording...");
            }
        }

        if (nextMode == State.Recooding)
        {
            if (clock.hour == 0 && clock.timer == 0)
            {
                nextMode = State.RecoodStop;
                Debug.Log("Recooding Stop!!!");
            }
            else
            {
                Debug.Log("Recoording now!!!");
                nextMode = State.Recooding;
            }
        }

        if (nextMode == State.RecoodStop)
        {
            if (clock.hour == 0 && clock.timer == 0)
            {
                man.StopRecording();
                text = "recording stopped";
                nextMode = State.ReplayStart;
                Debug.Log("Recood Stop");
            }
        }

        if (currentsce.currentscene == "Result")
        {
            if (nextMode == State.ReplayStart)
            {
                man.StartReplay();
                Debug.Log("Replay Start!!!");
                nextMode = State.Replaying;
            }
        }

        if (nextMode == State.Replaying)
        {
            if (replayable.idx + 1 == man.MaxRecordCount())
            {
                nextMode = State.ReplayStop;
                Debug.Log("Stop‚µ‚Ü‚·!!!");
            }
            else
            {
                nextMode = State.Replaying;
                Debug.Log("Replaying");
            }
        }

        if (nextMode == State.ReplayStop)
        {
            if (replayable.idx + 1 == man.MaxRecordCount())
            {
                man.StopReplay();
                Debug.Log("Replay Stop!!!");
            }
        }
    }

    void OnGUI ()
    {
        GUILayout.Label(text);

        if (recorded)
        {
            int max = man.MaxRecordCount();
            GUILayout.Label("" + max + " Recorded");

            float f = GUILayout.HorizontalSlider(frame, 0, max);
            if (((int)f) != ((int)frame))
            {
                if (nextMode == State.ReplayStop)
                {
                    man.StopReplay();
                    nextMode = State.ReplayStart;
                }

                frame = f;
                man.Playback((int)frame);
            }
        }
    }
}
