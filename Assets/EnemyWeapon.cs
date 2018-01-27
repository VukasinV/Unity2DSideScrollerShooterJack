using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWeapon : MonoBehaviour {
    public Rigidbody2D rb;
    CharacterWalk player = new CharacterWalk();
    public int damage;

    
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.transform.CompareTag("Player"))
        {
            col.transform.SendMessage("takeDamage",damage);
            Destroy(gameObject);
        }
        
    }
}
