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
/// This component provide replay recording for transform. 
/// </summary>
[RequireComponent(typeof(Replayable))]
public class TransformReplay : MonoBehaviour
{

	[HideInInspector]
	public List<Data> data = new List<Data> ();

	[Serializable]
	public struct Data
	{
		public float time;
		public Vector3 pos;
		public Quaternion rot;
		public Vector3 scl;
	}

	private Data Get ()
	{
		Data d = new Data ();
		d.time = Time.time - rep.recordStartTime;
		d.pos = transform.position;
		d.rot = transform.rotation;
		d.scl = transform.localScale;
		return d;
	}

	private void Set (Data d, Data d2, float dt)
	{
		
		transform.position = Vector3.Lerp (d.pos, d2.pos, dt);
		transform.rotation = Quaternion.Lerp (d.rot, d2.rot, dt);
		transform.localScale = Vector3.Lerp (d.scl, d2.scl, dt);
	}

	private void Set (Data d)
	{
		Set (d, d, 1);
	}


	private Replayable rep;

	void Start ()
	{
		rep = GetComponent<Replayable> ();
	}

	public void RecordStarting ()
	{
		data.Clear ();
	}

	public void RecordStopped ()
	{
	}

	public void ReplayStarting ()
	{
	}

	public void ReplayStopped ()
	{
	}

	public void ReplayReset ()
	{
		if (data.Count > 0)
			Set (data[0]);
	}

	public void ReplayRecording (int dataIdx)
	{
		data.Add (Get ());
	}

	public void ReplayPlaying (int dataIdx)
	{
		Set (data[dataIdx]);
	}


	public void ReplayPlayingComplete (float t)
	{
		int dataIdx = 0;
		for (int i = 1; i < data.Count; i++) {
			if (data[i].time > t) {
				dataIdx = i - 1;
				break;
			}
		}
		if (dataIdx < 1)
			return;
		
		Data d = data[dataIdx - 1];
		Data d2 = data[dataIdx];
		if (rep.replayCount < dataIdx)
			rep.replayCount = dataIdx;
		
		float dt = d2.time - t / (d2.time - d.time);
		Set (d, d2, dt);
		
	}
	
	
}
