using UnityEngine;
using System.Collections;
using System.Linq;

public class PositionOnObject : MonoBehaviour
{

	public Transform target;
	public Transform parentTo;
	public float height = 0;

	// Use this for initialization
	void Start()
	{
		if (Physics.Raycast(transform.position, target.position - transform.position, 500)) {
			// instead of this, find better way to reposition stuff.
			RaycastHit[] rg = Physics.RaycastAll(transform.position, target.position - transform.position, 500);
			if (rg.Any(r => r.transform == target)) {

				// rotate so it is looking towards normal of intersection with target
				transform.rotation = Quaternion.LookRotation(target.position - transform.position, Random.onUnitSphere);// *Quaternion.Euler(0, 90, 0);


				if (height != 0) {
					// move over to target
					EaseTo(rg.First(r => r.transform == target).point + (transform.position - target.position).normalized * height);
				} else {
					EaseTo(rg.First(r => r.transform == target).point);
				}
				//transform.position = rg.First(r => r.transform.tag == "Terrain").point;
			} else {
				Destroy(gameObject);
			}
		} else {
			Destroy(gameObject);
		}

		if (parentTo != null) {
			transform.parent = parentTo;
		}
	}

	void EaseTo(Vector3 finalPos)
	{
		//Vector3 initPos = transform.position;

		transform.position = finalPos;

	}

}
