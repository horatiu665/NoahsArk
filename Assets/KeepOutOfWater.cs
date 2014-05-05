using UnityEngine;
using System.Collections;

public class KeepOutOfWater : MonoBehaviour
{
	public float minHeight = -1f;
	Vector3 prevPos;

	void Start()
	{
		prevPos = transform.position;

	}

	// at each physics step, make sure the player does not go below a certain depth. if he does, move him to his previous position.
	void FixedUpdate()
	{
		if (transform.position.y < minHeight) {
			if (transform.position.y < prevPos.y) {
				transform.position = prevPos;
			}
		}



		prevPos = transform.position;
	}
}
