using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndOfLevel : MonoBehaviour {

    public GameObject endOfLevelUI;
	// Use this for initialization
	void Start () {
	}

	private void OnTriggerEnter2D (Collider2D col)
	{
		if (col.tag == "Player") 
		{
            endOfLevelUI.SetActive(true);
		}
	}

    private void OnTriggerExit2D (Collider2D col)
    {
        endOfLevelUI.SetActive(false);
    }
}
