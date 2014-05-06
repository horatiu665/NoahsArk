using UnityEngine;
using System.Collections;

public class Wiggle : MonoBehaviour {
	public bool S_tart;
	[Range(0,1)]
	public float speed;
	[Range(0,1)]
	public float Xangle;
	[Range(0,1)]
	public float Yangle;
	[Range(0,1)]
	public float Zangle;
	private float x,y,z;


	// Use this for initialization
	void Start () {
		S_tart = false;
		x = 1;
		y = 1;
		z = 1;
	
	}
	
	// Update is called once per frame
	void Update () {
		//angle for when to turn back for X
		if(transform.rotation.x>Xangle){
			x = speed*-1;
		}else if(transform.rotation.x<-Xangle){
			x = speed;
		}
		//angle for when to turn back for Z
		if(transform.rotation.y>Yangle){
			y = speed*-1;
		}else if(transform.rotation.y<-Yangle){
			y = speed;
		}
		//angle for when to turn back for Z
		if(transform.rotation.z>Zangle){
			z = speed*-1;
		}else if(transform.rotation.z<-Zangle){
			z = speed;
		}

		if(S_tart){
			if(Xangle>0&&Yangle>0&&Zangle>0){
				transform.Rotate(new Vector3(x,y,z));

			}else if(Xangle==0&&Yangle>0&&Zangle>0){
				transform.Rotate(new Vector3(0,y,z));

			}else if(Xangle>0&&Yangle==0&&Zangle>0){
				transform.Rotate(new Vector3(x,0,z));
				
			}else if(Xangle>0&&Yangle>0&&Zangle==0){
				transform.Rotate(new Vector3(x,y,0));

			}else if(Xangle>0&&Yangle==0&&Zangle==0){
				transform.Rotate(new Vector3(x,0,0));
				
			}else if(Xangle==0&&Yangle>0&&Zangle==0){
				transform.Rotate(new Vector3(0,y,0));

			}else if(Xangle==0&&Yangle==0&&Zangle>0){
				transform.Rotate(new Vector3(0,0,z));
			}
		}


	}
}
