using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TrackScript : MoveScript {

	public GameObject target;	//Home in on this thing
	public Transform targetPos; //This is where
	public float magnitude;		//How fast do you home in on the target
	public float accelerate;	//How fast does this speed up
	public float maxSpeed;		//Max speed?

	private float angle;
	private bool isEnemy;

	// Use this for initialization	
	void Start () {
		isEnemy = gameObject.GetComponent<ShotScript> ().isEnemyShot;

		if (!isEnemy) {
			GameObject[] enemies = FindObjectsOfType(typeof(GameObject)) as GameObject[];

			float nearestDistanceSqr = Mathf.Infinity; 
			// loop through each tagged object, remembering nearest one found
			foreach (GameObject go in enemies) {
				EnemyScript checkEnemy = go.GetComponent<EnemyScript>();
				if (checkEnemy != null){
					Transform objectPos = go.transform;
					float distanceSqr = (objectPos.position - transform.position).sqrMagnitude;
					
					if (distanceSqr < nearestDistanceSqr) {
						target = go;
						targetPos = target.transform;
						nearestDistanceSqr = distanceSqr;
					}
				}
			}
		}
		if (target == null) 
			targetPos.position = new Vector3 (transform.position.x, transform.position.y, -2.5f);
		magnitude = 0;
	}
	
	// Update is called once per frame
	void Update () {

		//if (Mathf.FloorToInt() % 2)
			//target = FindObjectOfType (target);

		if (target != null) {
			targetPos.position = target.transform.position;
			var dir = transform.position - targetPos.position;
			var angle = Mathf.Atan2 (dir.y, dir.x) * Mathf.Rad2Deg;
			var newRotation = Quaternion.AngleAxis (angle + 180, Vector3.forward);
			transform.rotation = Quaternion.Slerp (transform.rotation, newRotation, Time.deltaTime * turnSpeed);

			Debug.DrawLine (targetPos.position, transform.position, Color.yellow);
		}

		if (magnitude < maxSpeed) magnitude += accelerate * Time.deltaTime;
		else if (magnitude > maxSpeed) magnitude = maxSpeed;
		
		movement = new Vector2 (Mathf.Cos(transform.eulerAngles.z) * magnitude, Mathf.Sin(transform.eulerAngles.z) * magnitude);

	}

	void FixedUpdate()
	{
		// Apply movement to the rigidbody
		rigidbody2D.velocity = movement;

	}
}
