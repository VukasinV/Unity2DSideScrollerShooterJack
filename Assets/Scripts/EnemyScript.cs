using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour {

	public LayerMask enemyMask;
	public float speed;
	private Rigidbody2D myBody;
	float myWidth;
	float myHeight;
	private Transform transform;


	private Animator animator;

	// Use this for initialization
	void Start () {
        transform = this.transform;
        animator = FindObjectOfType<Animator>();
		myBody = GetComponent<Rigidbody2D> ();
    }

	// Update is called once per frame
	void Update () {
		 myBody.transform.Translate (new Vector3(speed, 0, 0) * Time.deltaTime);

		if (speed > 0) {
			animator.SetInteger ("Walking", 1);
		} else if (speed < 0) {
			animator.SetInteger ("Walking", 1);
		} else 
		{
			animator.SetInteger ("Walking", 0);
		}

	}
}
