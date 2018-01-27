using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class dog : MonoBehaviour {

    public float speed = 0;
	private Animator anim;
    private SpriteRenderer sprite;
    private AudioSource audio;


	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator> ();
        sprite = GetComponent<SpriteRenderer>();
        audio = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
        transform.Translate(Vector3.right * Time.deltaTime * speed);

        if (transform.position.x >= 2.5f)
        {
            sprite.flipX = true;
            speed = -1;
        }

        if (transform.position.x <= -2)
        {
            sprite.flipX = false;
            speed = 1;
        }
	}

	private void OnTriggerEnter2D (Collider2D col)
	{
		if (col.tag == "Player") 
		{
			anim.SetBool ("awakend", true);
            anim.SetBool("enemyHere", true);
            speed = 0;
            audio.Play();
        }
	}

    private void OnTriggerExit2D (Collider2D col)
    {
        if(col.tag == "Player")
        {
            audio.Stop();
            anim.SetBool("enemyHere", false);

            if (!sprite.flipX)
            {
                speed = 1;
            } else
            {
                speed = -1;
            }
        }
    }
    
}
