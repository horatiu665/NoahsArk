using UnityEngine;
using System.Collections;

public class ScreenShaker : MonoBehaviour
{

	static ScreenShaker instance;
	public float duration;
	public AnimationCurve force;

	void Start()
	{
		instance = this;
	}

	public static void ShakeScreen()
	{
		instance.StartCoroutine(instance.SSC());
	}

	IEnumerator SSC()
	{
		Vector3 change = Vector3.zero;
		Vector3 initPos = transform.position;
		yield return StartCoroutine(pTween.To(duration, 0, 1, t => {
			initPos = transform.position - change;
			change = Random.insideUnitSphere * force.Evaluate(t);
			transform.position = initPos + change;

		}));

		transform.position = initPos - change;
	}
}
