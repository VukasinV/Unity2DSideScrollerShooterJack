using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollows : MonoBehaviour {

	[SerializeField]
	private float xMax = 4.7f;

	[SerializeField]
	private float yMax = 10.58f;

	[SerializeField]
	private float xMin = -26.28f;

	[SerializeField]
	private float yMin = 4.47f;

	private Transform target;
	// Use this for initialization
	void Start () 
	{
		target = GameObject.Find ("Character").transform;
	}
	
	// Update is called once per frame
	void LateUpdate () 
	{
		transform.position = new Vector3(Mathf.Clamp(target.position.x,xMin,xMax),Mathf.Clamp(target.position.y,yMin,yMax), transform.position.z);
	}
}
