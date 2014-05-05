using UnityEngine;
using System.Collections;

public class DestroyInS : MonoBehaviour {

	public float seconds;

	// Use this for initialization
	void Start () {
		Destroy(gameObject, seconds);
	}
}
