using UnityEngine;
using System.Collections;

public class FollowCam : MonoBehaviour {

	static public FollowCam S;   //singleton
	
	public float easing = 0.05f;
	public Vector2 minXY;
	public bool _________________________;
	
	public GameObject poi;    //point of interest
	public float camZ;        //desired z position of the camera
	
	// Use this for initialization
	void Start () {
		S = this;
		camZ = this.transform.position.z;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		Vector3 destination;
		if (poi == null) {
			destination = Vector3.zero;        
		} else {
			destination = poi.transform.position;
			if(poi.tag == "Projectile"){
				if(poi.GetComponent<Rigidbody>().IsSleeping()){
					poi = null;
					return;
				}
			}
		}
		destination.x = Mathf.Max (minXY.x, destination.x);
		destination.y = Mathf.Max (minXY.y, destination.y);
		destination = Vector3.Lerp (transform.position, destination, easing);
		destination.z = camZ;
		transform.position = destination;
		this.GetComponent<Camera>().orthographicSize = destination.y + 10;
	}
}