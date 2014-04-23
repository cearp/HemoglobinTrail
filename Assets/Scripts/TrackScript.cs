using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TrackScript : MoveScript {

	public GameObject target;	//Home in on this thing
	public Vector3 targetPos; //This is where
	public float magnitude;		//How fast do you home in on the target
	public float accelerate;	//How fast does this speed up
	public float maxSpeed;		//Max speed?
	public bool findNew;		//When old target is gone, do you find a new one?

	private float angle;
	private bool isEnemy;
	private GameObject[] enemies;

	// Use this for initialization	
	void Start () {
		isEnemy = gameObject.GetComponent<ShotScript> ().isEnemyShot;

		if (!isEnemy) {
			enemies = FindObjectsOfType(typeof(GameObject)) as GameObject[];

			float nearestDistanceSqr = Mathf.Infinity; 
			// loop through each tagged object, remembering nearest one found
			foreach (GameObject go in enemies) {
				EnemyScript checkEnemy = go.GetComponent<EnemyScript>();
				if (checkEnemy != null){
					Transform objectPos = go.transform;
					float distanceSqr = (objectPos.position - transform.position).sqrMagnitude;
					
					if (distanceSqr < nearestDistanceSqr) {
						target = go;
						targetPos = target.transform.position;
						nearestDistanceSqr = distanceSqr;
					}
				}
			}
		}
		if (target == null) 
			targetPos = new Vector3 (transform.position.x, transform.position.y, -2.5f);
	}
	
	// Update is called once per frame
	void Update () {

		//if (Mathf.FloorToInt() % 2)
			//target = FindObjectOfType (target);

		if (target == null && findNew) {
			if (enemies.Length > 0)
				Start();

		} else if (target != null) {
				setTargetPos(target.transform.position);
				
				//movement = new Vector2 (Mathf.Cos (transform.eulerAngles.z) * magnitude, Mathf.Sin (transform.eulerAngles.z) * magnitude);
				if (magnitude < maxSpeed) magnitude += accelerate * Time.deltaTime;
				else if (magnitude > maxSpeed) magnitude = maxSpeed;
		} else {
			setTargetPos(transform.position + new Vector3(rigidbody2D.velocity.x, rigidbody2D.velocity.y));
			if (magnitude < maxSpeed) magnitude += accelerate * Time.deltaTime;
			else if (magnitude > maxSpeed) magnitude = maxSpeed;
			//movement = new Vector2 (Mathf.Cos(transform.eulerAngles.z) * magnitude, Mathf.Sin(transform.eulerAngles.z) * magnitude);
		}


	}

	void setTargetPos(Vector3 thisSpot){
		targetPos = thisSpot;
		var dir = transform.position - targetPos;
		var angle = Mathf.Atan2 (dir.y, dir.x) * Mathf.Rad2Deg;
		var newRotation = Quaternion.AngleAxis (angle + 90, Vector3.forward);
		transform.rotation = Quaternion.Slerp (transform.rotation, newRotation, Time.deltaTime * Random.Range( turnSpeed * .5f, turnSpeed * 2f));

	}

	void FixedUpdate()
	{
		// Apply movement to the rigidbody
		//rigidbody2D.velocity = movement;

		rigidbody2D.velocity = transform.up * magnitude;
		
	}
}
