using UnityEngine;
using UnityEngine.Video;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class MoviePlayer : MonoBehaviour
{
    float second = 30;

    WaitForSeconds interval;
    WaitForSeconds waitMovie;

    VideoPlayer player;

    Image hide;

    void Awake()
    {
        player = GetComponent<VideoPlayer>();

        interval  = new WaitForSeconds(second);
        waitMovie = new WaitForSeconds(player.clip.frameCount / (float)player.clip.frameRate);

        hide = transform.GetChild(0).GetChild(0).GetComponent<Image>();
    }

    IEnumerator Start()
    {
        while (true)
        {
            yield return interval;
            Debug.Log("Play");

            hide.enabled = true;
            for (float t = 0.0f; t < 1.0f; t += Time.deltaTime)
            {
                SetAlpha(t);
                yield return null;
            }
            SetAlpha(1.0f);

            player.Play();
            yield return null;
            yield return null;
            yield return null;
            hide.enabled = false;

            yield return waitMovie;
            Debug.Log("Stop");

            hide.enabled = true;
            player.Stop();

            for (float t = 1.0f; t > 0.0f; t -= Time.deltaTime)
            {
                SetAlpha(t);
                yield return null;
            }
            SetAlpha(1.0f);

            hide.enabled = false;
        }
    }


    void SetAlpha(float a)
    {
        Color col = hide.color;
        col.a = a;
        hide.color = col;
    }
}
