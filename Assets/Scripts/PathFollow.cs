﻿using UnityEngine;
using System.Collections;

public class PathFollow : Vehicle {
	public float inFront;
	public float future;
	public float seekWt;
	public float width;
	GameObject[] path;
	Vector3 target;
	Vector3 futurePosition;
	Vector3 closestPoint;
	float closestDistance;
	GameObject wp;

	// Use this for initialization
	void Start () {
		base.Start (); 
		path = GameObject.FindGameObjectsWithTag ("WP");
		getClosestPoint ();
	}
	
	protected override void CalcSteeringForce(){
		Vector3 force = Vector3.zero;
		nextClosestPoint ();
		target = closestPoint + wp.GetComponent<WayPoint> ().unitVec * inFront;
		if (closestDistance > width/2) {
			force += seekWt * Seek (target);
		}
		//limit force to maxForce and apply
		force = Vector3.ClampMagnitude (force, maxForce);
		ApplyForce(force);
		
		//show force as a blue line pushing the guy like a jet stream
		Debug.DrawLine(transform.position, transform.position - force,Color.blue);
		//red line to the target which may be out of sight
		Debug.DrawLine (transform.position, target,Color.red);
		
	}

	private void nextClosestPoint(){
		GameObject[] nextPoints = new GameObject[2];
		nextPoints[0] = wp.gameObject;
		nextPoints [1] = nextPoints[0].GetComponent<WayPoint>().next;
		closestDistance = 1000;
		float curClosestDistance;
		Vector3 curClosestPoint;
		futurePosition = transform.position + velocity * future;
		for (int i=0; i < 2; i++) {
			GameObject curWP = nextPoints[i];
			curClosestPoint = curWP.GetComponent<WayPoint>().closestPoint(futurePosition);
			curClosestDistance = Vector3.Distance(curClosestPoint, futurePosition);
			if(curClosestDistance < closestDistance){
				closestDistance = curClosestDistance;
				closestPoint = curClosestPoint;
				wp = curWP;
			}
		}
	}

	private void getClosestPoint(){
		closestDistance = 1000;
		float curClosestDistance;
		Vector3 curClosestPoint;
		futurePosition = transform.position + velocity * future;
		for (int i=0; i < path.Length; i++) {
			GameObject curWP = path[i];
			curClosestPoint = curWP.GetComponent<WayPoint>().closestPoint(futurePosition);
			curClosestDistance = Vector3.Distance(curClosestPoint, futurePosition);
			if(curClosestDistance < closestDistance){
				closestDistance = curClosestDistance;
				closestPoint = curClosestPoint;
				wp = curWP;
			}
		}
	}
}