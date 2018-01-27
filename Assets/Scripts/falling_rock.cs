using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class falling_rock : MonoBehaviour {

	SpriteRenderer sr;
    public Transform target;
    Animator anim;
    Transform firstTrans;
    Rigidbody2D rockRB;
    Animator animator;
    private bool grounded;

    // Use this for initialization
    void Start () {
        animator = GetComponent<Animator>();
        rockRB = GetComponent<Rigidbody2D>();
		sr = GetComponent<SpriteRenderer> ();
        anim = GetComponent<Animator>();
        firstTrans = GetComponent<Transform>();
        grounded = false;

        firstTrans.position = new Vector3(transform.position.x, transform.position.y, transform.position.z);
    }
	
	// Update is called once per frame
	void Update () {
    }

	private void OnTriggerEnter2D (Collider2D col)
	{

		if (col.tag == "Player" && sr.sprite.name == "tileset_6") {
            target.GetComponent<CharacterWalk>().hp = 0;
		}

        

        if ((col.tag == "Mapa" || col.tag == "Player"))
        {
            
            grounded = true;
            anim.SetBool("onTheGound", true);
            rockRB.gravityScale = 0;
            rockRB.velocity = new Vector2(0, 0.000001f);
            
            
        }
        

	}

    private void VratiKamen()
    {
        transform.Translate(firstTrans.position.x - transform.position.x, firstTrans.position.y, transform.position.z);
        grounded = false;
    }


    private void OnTriggerExit2D(Collider2D col)
    {
        if ((col.tag == "Mapa" || col.tag == "Player"))
        {
            anim.SetBool("onTheGound", false);
            grounded = false;
            rockRB.gravityScale = 1f;
            rockRB.velocity = new Vector2(0, 0);
        }
    }

}
