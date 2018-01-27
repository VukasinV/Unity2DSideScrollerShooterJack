using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RestardGame : MonoBehaviour {

    public float restartTime;
    bool restartNow = false;
    float resetTime;

	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (restartNow == true && resetTime <= Time.time)
        {
            System.Threading.Thread.Sleep(1000);
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
	}

    public void restartTheGame()
    {
        restartNow = true;
        resetTime =  restartTime;
    }
}
