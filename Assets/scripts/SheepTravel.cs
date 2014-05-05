using UnityEngine;
using System.Collections;
using System.Linq;
using System.Collections.Generic;

public class SheepTravel : MonoBehaviour
{

	public Vector3 target;
	float targetChangeTimer;
	public Vector2 targetChangeDuration;

	public float nearbySheepDistanceSqr = 10f;
	public float targetRandomness = 1;
	public float pushForce = 10f;
	private bool rotating = false;

	public bool isInFence = false;

	// average location of the other sheep;s target
	void FindCommonTarget()
	{
		Vector3 commonTarget = Vector3.zero;
		int count = 0;
		foreach (var s in GameObject.FindGameObjectsWithTag("Sheep").Where(c => (c.transform.position - transform.position).sqrMagnitude < nearbySheepDistanceSqr)) {
			SheepTravel st = s.GetComponent<SheepTravel>();
			commonTarget += st.target;
			count++;

		}
		commonTarget /= count;
		commonTarget += Random.insideUnitSphere * targetRandomness;

		target = commonTarget;

	}

	void FindRandomTarget()
	{
		target = new Vector3(Random.Range(-256, 256f), 0, Random.Range(-256, 256));
		
	}

	void SetTargetChangeTimer()
	{
		targetChangeTimer = Time.time + Mathf.Abs(
					Random.Range(-targetChangeDuration.y, targetChangeDuration.y)
					+ Random.Range(-targetChangeDuration.y, targetChangeDuration.y)
					) / 2 + targetChangeDuration.x;

	}

	void FixedUpdate()
	{
		Vector3 force = (target - transform.position);
		force.Scale(new Vector3(1, 0, 1));
		rigidbody.AddForce(force.normalized * pushForce, ForceMode.Force);

		if (targetChangeTimer < Time.time) {
			if (Random.Range(0, 100) < chanceForGoAway) {
				FindRandomTarget();
				SetTargetChangeTimer();
			}
			if (Random.Range(0, 100) < chanceForFollowOthers) {
				FindCommonTarget();
				SetTargetChangeTimer();
			}

		} 

	}

	IEnumerator Start()
	{
		if (chanceForGoAway > 0) {
			FindRandomTarget();
			yield return 0;
			FindCommonTarget();
		}
	}

	void Update()
	{
		if ((target - transform.position).sqrMagnitude > 9) {
			RotateTowards(target);

		}

	}

	public float chanceForGoAway = 1f;
	public float chanceForFollowOthers = 5f;
	
	public float rotDuration = 0.2f;

	private IEnumerator RotateSmoothCoroutine(Vector3 direction, float rotationTime)
	{
		if (direction == Vector3.zero)
			direction = Vector3.right;
		direction.y = 0;

		transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, 0);
		float angle = Quaternion.Angle(transform.rotation, Quaternion.LookRotation(direction));
		float tempTime = Time.time;
		if (angle == 180) {
			transform.rotation = transform.rotation * Quaternion.Euler(0, 1, 0);
		}
		// angle is between 0 and 180.
		rotating = true;

		Quaternion initRot = transform.rotation;
		while (angle > 1 && !(Time.time - tempTime > rotationTime)) {

			transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, 0);

			transform.rotation = Quaternion.Lerp(initRot,
				Quaternion.LookRotation(direction),
				(Time.time - tempTime) / rotationTime);

			angle = Quaternion.Angle(transform.rotation, Quaternion.LookRotation(direction));

			yield return 0;
		}

		rotating = false;

		transform.rotation = Quaternion.LookRotation(direction);
	}

	public void RotateTowards(Vector3 pos, float rotationTime = 0.2f)
	{
		if (!rotating) {
			StartCoroutine(RotateSmoothCoroutine(pos - transform.position, rotationTime));

		}
	}

	void OnDrawGizmosSelected()
	{
		Gizmos.color = Color.green;
		Gizmos.DrawLine(transform.position, target);

	}

}
