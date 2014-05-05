using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class SoundOnCollision : MonoBehaviour {

	public AudioClip[] clips;
	public Vector2 pitches = new Vector2(0.85f, 1.2f);
	public string[] tags;
	
	void OnCollisionEnter(Collision hit)
	{
		if (tags.Contains(hit.transform.tag)) {
			audio.pitch = Random.Range(pitches.x, pitches.y);
			audio.PlayOneShot(clips[Random.Range(0, clips.Length)]);

		}
		
	}
}
