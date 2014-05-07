using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerGun : MonoBehaviour {

	public Camera playerCam;
	
	private Ray playerAim;
	private Transform objectHeld;
	private bool isObjectHeld;
	
	void Start () {
		isObjectHeld = false;
		objectHeld = null;
		Screen.lockCursor = true;
	}
	
	void Update () {
		if(Input.GetKey(KeyCode.E)){
			if(!isObjectHeld){
				tryPickObject();
			} else {
				holdObject();
			}
		}
		
		if(Input.GetKeyUp(KeyCode.E) && isObjectHeld){
			isObjectHeld = false;
			objectHeld.rigidbody.useGravity = true;
		}
	}
	
	private void tryPickObject(){
				Ray playerAim = playerCam.ScreenPointToRay (new Vector3 (playerCam.pixelWidth / 2, playerCam.pixelHeight / 2, 0));
				RaycastHit hit;

		
				Physics.Raycast (playerAim, out hit,10);
				if (hit.collider != null) {
						if (hit.collider.tag == "Animal") {
								isObjectHeld = true;

				objectHeld=hit.transform;
								//objectHeld = pickable;
								objectHeld.rigidbody.useGravity = false;
						}
		
				}
		}
	
	private void holdObject(){
		Ray playerAim = playerCam.ViewportPointToRay(new Vector3(playerCam.pixelWidth/2, playerCam.pixelHeight/2, 0));
		
		Vector3 nextPos = playerCam.transform.position + playerCam.transform.forward * 2;
		Vector3 currPos = objectHeld.transform.position;
		
		objectHeld.rigidbody.velocity = (nextPos - currPos) * 10;
		//objectHeld.transform.position = nextPos;
	}
}
