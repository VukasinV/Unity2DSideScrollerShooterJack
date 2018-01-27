using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ladder : MonoBehaviour {
    private CharacterWalk character;
    // Use this for initialization
    void Start () {
        character = FindObjectOfType<CharacterWalk>();
	}

    void OnTriggerEnter2D(Collider2D other)
    {

        if(other.name=="Character")
        {
            character.OnLadder = true;
            
        }

        
        
        
    }

    void OnTriggerExit2D(Collider2D other)
    {

        if (other.name == "Character")
        {
            character.OnLadder = false;

        }
    }
    


}
