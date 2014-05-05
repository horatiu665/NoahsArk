using UnityEngine;
using System.Collections;
using System.Linq;

public class PositionOnTerrain : MonoBehaviour {

	public float timeOfFall = 0.3f;
	public AnimationCurve easeCurve;

	// Use this for initialization
	void Start () {
		if (Physics.Raycast(transform.position, Vector3.down * 500)) {
			// instead of this, find better way to reposition stuff.
			RaycastHit[] rg = Physics.RaycastAll(transform.position, Vector3.down, 500);
			if (rg.Any(r => r.collider.GetComponent<TerrainCollider>())) {
				StartCoroutine(EaseTo(rg.First(r => r.collider.GetComponent<TerrainCollider>()).point));
				//transform.position = rg.First(r => r.transform.tag == "Terrain").point;
			} else {
				Destroy(gameObject);
			}
		} else {
			Destroy(gameObject);
		}
	}

	IEnumerator EaseTo(Vector3 finalPos)
	{
		Vector3 initPos = transform.position;
		yield return StartCoroutine(pTween.To(timeOfFall, 0, 1, t => {
			transform.position = Vector3.Lerp(initPos, finalPos, easeCurve.Evaluate(t));

		}));
		transform.position = finalPos;

	}

}
