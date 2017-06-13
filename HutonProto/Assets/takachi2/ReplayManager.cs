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

public class ReplayManager : MonoBehaviour
{
	[HideInInspector]
	public Replayable[] replayables = null;

	/// <summary>
	/// 0 - Record as much as possible
	/// </summary> 
	public int maxRecordPerSecond = 0;

	/// <summary>
	/// Set group number to denote handling replayables 
	/// </summary>
	public int replayGroup = 0;

	/// <summary>
	/// Start Recording on each replayables
	/// </summary>
	public void StartRecording ()
	{
		if (replayables == null)
			return;
		
		float f = (maxRecordPerSecond == 0) ? 0 : 1f / maxRecordPerSecond;
		foreach (Replayable r in replayables) {
			if (r.replayGroup != replayGroup)
				continue;
			
			r.duration = f;
            // ÉåÉRÅ[Éh
			r.StartRecording ();
		}
	}

	/// <summary>
	/// Stop Recording on each replayables
	/// </summary>
	public void StopRecording ()
	{
		if (replayables == null)
			return;
		
		foreach (Replayable r in replayables) {
			if (r.replayGroup != replayGroup)
				continue;
			
			r.StopRecording ();
		}
	}

	/// <summary>
	/// Start Replaying on each replayables
	/// </summary>
	public void StartReplay ()
	{
		
		if (replayables == null)
			return;
		
		foreach (Replayable r in replayables) {
			if (r.replayGroup != replayGroup)
				continue;
			
			r.StartReplay ();
		}
	}

	/// <summary>
	/// Stop Replaying on each replayables
	/// </summary>
	public void StopReplay ()
	{
		if (replayables == null)
			return;
		
		foreach (Replayable r in replayables) {
			if (r.replayGroup != replayGroup)
				continue;
			
			r.StopReplay ();
		}
	}

	/// <summary>
	/// Collect all replayables in the scene (replayables have same replayGroup number)
	/// Call ReplyReset on each replayables
	/// </summary>
	public void Initialize ()
	{
		replayables = FindObjectsOfType (typeof(Replayable)) as Replayable[];
		foreach (Replayable r in replayables) {
			if (r.replayGroup != replayGroup)
				continue;
			
			r.gameObject.SendMessage ("ReplayReset", SendMessageOptions.DontRequireReceiver);
		}
		
	}

	/// <summary>
	/// Set frame on each replayables
	/// </summary>
	public void Playback (int i)
	{
		if (replayables == null)
			return;
		
		foreach (Replayable r in replayables) {
			if (r.replayGroup != replayGroup)
				continue;
			
			r.Playback (i);
		}
	}


	/// <summary>
	/// Get max recorded number in the group
	/// </summary>
	public int MaxRecordCount ()
	{
		int max = 0;
		
		foreach (Replayable r in replayables) {
			if (r.replayGroup != replayGroup)
				continue;
			
			max = Mathf.Max (max, r.recordCount);
		}
		return max;
	}
}
