using UnityEngine;
using System.Collections;

public class Slingshot : MonoBehaviour {
	//fields set in the Unity Insector pane
	public GameObject 	prefabProjectile;
	public float 		velocityMult = 4f;
	public bool 		________________;
	// fields are set dynamically
	public GameObject	launchPoint;
	public Vector3		launchPos;
	public GameObject	projectile;
	public bool			aimingMode;

	void Awake() {
		Transform launchPointTrans = transform.Find ("LaunchPoint");
		launchPoint = launchPointTrans.gameObject;
		launchPoint.SetActive (false);
		launchPos = launchPointTrans.position;
	}

	void OnMouseEnter() {
		//print ("Slingshot:OnMouseEnter()");
	}

	void OnMouseExit() {
		//print ("Slingshot:OnMouseExit()");
	}

	void OnMouseDown() {
		// The player has pressed the mouse button while over slingshot
		aimingMode = true;
		// Insantiate a projectile
		projectile = Instantiate (prefabProjectile) as GameObject;
		// Start at the launchPoint
		projectile.transform.position = launchPos;

		// Set it to isKinematic for now
		projectile.GetComponent<Rigidbody>().isKinematic = true;
	}

	void Update() {
		// If slingshot is not in aiming mode - do not run this code
		if (!aimingMode) return;
		// Get the current mouse position in the 2D screen coordinates
		Vector3 mousePos2D = Input.mousePosition;
		// Convert the mouse position to 3D world coordinates
		mousePos2D.z = -Camera.main.transform.position.z;
		Vector3 mousePos3D = Camera.main.ScreenToWorldPoint (mousePos2D); 
		// Find the delta from the launchPos to the mousePos3D
		Vector3 mouseDelta = mousePos3D-launchPos;
		// Limit mouseDelta to the radius of the slingshot SpereCollider
		float maxMagnitude = this.GetComponent<SphereCollider>().radius;
		if (mouseDelta.magnitude > maxMagnitude) {
			mouseDelta.Normalize ();
			mouseDelta *= maxMagnitude;
		}
		// Move the projectile to this new position
		Vector3 projPos = launchPos + mouseDelta;
		projectile.transform.position = projPos;

		if (Input.GetMouseButtonUp (0) ) {
			// The mouse has been released
			aimingMode = false;
			projectile.GetComponent<Rigidbody>().isKinematic = false;
			projectile.GetComponent<Rigidbody>().velocity = -mouseDelta * velocityMult;
			projectile = null; 
		}
	}
}
