/*
 This file is part of Replay Framework for Unity .

    Mindset reader for Unity is free software: you can redistribute it and/or modify
    it under the terms of the GNU General Public License as published by
    the Free Software Foundation, either version 3 of the License, or
    (at your option) any later version.

    Foobar is distributed in the hope that it will be useful,
    but WITHOUT ANY WARRANTY; without even the implied warranty of
    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
    GNU General Public License for more details.

    You should have received a copy of the GNU General Public License
    along with Foobar.  If not, see <http://www.gnu.org/licenses/>.
*/


using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

/// <summary>
/// Put this component on the gameobject that you want to add replay feature. 
/// </summary>
public class Replayable : MonoBehaviour
{

	/// <summary>
	/// Set replay group. Only selected replay group will be recorded and play.
	/// Replay group is selected by <see cref="ReplayManager"/>
	/// </summary>
	public int replayGroup = 0;

	[HideInInspector]
	public bool recording = false;
	[HideInInspector]
	public float recordStartTime;
	[HideInInspector]
	public int recordCount = 0;

	[HideInInspector]
	public bool replaying = false;
	[HideInInspector]
	public float replayStartTime;
	[HideInInspector]
	public int replayCount = 0;

	//set by ReplayManager
	[HideInInspector]
	public float duration;

	public void StartRecording ()
	{
		StopCoroutine ("Record");
		SendMessage ("RecordStarting", SendMessageOptions.DontRequireReceiver);
		recordStartTime = Time.deltaTime;
		recordCount = 0;
        // レコードを取るかのフラグ
		recording = true;
		StartCoroutine ("Record");
	}

	public void StopRecording ()
	{
		recording = false;
		SendMessage ("RecordStopped", SendMessageOptions.DontRequireReceiver);
	}

	private IEnumerator Record ()
	{
		while (recording) {
			SendMessage ("ReplayRecording", recordCount++, SendMessageOptions.DontRequireReceiver);
			
			yield return new WaitForSeconds (duration);
		}
	}

	private IEnumerator Replay ()
	{
		
		while (replaying) {
			bool atTheEnd = false;
			
			int idx = replayCount + 1;
			
			//It's already arrived at the end
			if (atTheEnd) {
				SendMessage ("ReplayPlaying", idx, SendMessageOptions.DontRequireReceiver);
			} else {
				SendMessage ("ReplayPlayingComplete", Time.time - replayStartTime, SendMessageOptions.DontRequireReceiver);
				
				if (replayCount + 1 < recordCount - 1) {
					replayCount++;
				} else {
					atTheEnd = true;
				}
			}
			yield return new WaitForSeconds (0);
		}
		
	}

	private IEnumerator PlaybackWorker (int idx)
	{
		while (!recording && !replaying) {
			SendMessage ("ReplayPlaying", idx, SendMessageOptions.DontRequireReceiver);
			
			yield return new WaitForSeconds (0);
		}
		
	}

	public void Playback (int i)
	{
		if (i < recordCount && i > 0) {
			StopCoroutine ("PlaybackWorker");
			recording = false;
			replaying = false;
			StartCoroutine ("PlaybackWorker", i);
		}
	}

	public void StartReplay ()
	{
		if (recordCount <= 0)
			return;
		
		StopCoroutine ("Replay");
		SendMessage ("ReplayStarting", SendMessageOptions.DontRequireReceiver);
		replayStartTime = Time.time;
		replayCount = 0;
		replaying = true;
		StartCoroutine ("Replay");
	}

	public void StopReplay ()
	{
		replaying = false;
		SendMessage ("ReplayStopped", SendMessageOptions.DontRequireReceiver);
	}
	
	
}
