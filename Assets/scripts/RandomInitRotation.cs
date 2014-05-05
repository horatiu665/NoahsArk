using UnityEngine;
using System.Collections;

public class RandomInitRotation : MonoBehaviour {

	// Use this for initialization
	void Start () {
		transform.rotation = Quaternion.Euler(0, Random.Range(0, 360f), 0);

	}
}
