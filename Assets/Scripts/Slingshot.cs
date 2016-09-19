using UnityEngine;
using System.Collections;

public class Slingshot : MonoBehaviour {
	//fields set in the Unity Insector pane
	public GameObject 	prefabProjectile;
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
}
