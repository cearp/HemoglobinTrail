using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using System.Collections;

public class SinScript : MonoBehaviour {


	public float Amp;

	//public int freq;
	// Use this for initialization
	public bool isLooping = false;
	
	/// <summary>
	/// 2 - List of children with a renderer.
	/// </summary>
	private List<Transform> backgroundPart;
	public Vector2 direction = new Vector2(-1, 0);
	public float x = 0;
	void Start () {
		if (isLooping)
		{
			// Get all the children of the layer with a renderer
			backgroundPart = new List<Transform>();
			
			for (int i = 0; i < transform.childCount; i++)
			{
				Transform child = transform.GetChild(i);
				
				// Add only the visible children
				if (child.renderer != null)
				{
					backgroundPart.Add(child);
				}
			}
			
			// Sort by position.
			// Note: Get the children from left to right.
			// We would need to add a few conditions to handle
			// all the possible scrolling directions.
			if (direction.x != 0)
				backgroundPart = backgroundPart.OrderBy(
					t => t.position.x
					).ToList();
			else if (direction.y != 0)
				backgroundPart = backgroundPart.OrderBy(
					t => t.position.y
					).ToList();
			
		}
	
	}
	
	// Update is called once per frame
	void Update () {
		x++;
		Vector3 movement = new Vector3 (
			Mathf.Sin(x)*Amp,
			0,
			0);
		movement *= Time.deltaTime;
		transform.Translate (movement);
		var dist = (transform.position - Camera.main.transform.position).z;
		
		var leftBorder = Camera.main.ViewportToWorldPoint(
			new Vector3(0, 0, dist)
			).x;
		
		var rightBorder = Camera.main.ViewportToWorldPoint(
			new Vector3(1, 0, dist)
			).x;
		
		transform.position = new Vector3(
			Mathf.Clamp(transform.position.x, leftBorder, rightBorder),
			transform.position.y,
			transform.position.z
			);
	}
}
