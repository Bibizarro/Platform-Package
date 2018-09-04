using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Pause : MonoBehaviour {

    bool paused;
    public GameObject pauseMenu;

	// Use this for initialization
	void Start () {

        paused = false;
        pauseMenu.SetActive(false);
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Escape) && !paused)
        {
            Time.timeScale = 0;
            paused = true;
            pauseMenu.SetActive(true);
        }

        else if (Input.GetKeyDown(KeyCode.Escape) && paused)
        {
            Time.timeScale = 1;
            paused = false;
            pauseMenu.SetActive(false);
        }
    }
   public void Unpausing()
    {
        Time.timeScale = 1;
        paused = false;
        pauseMenu.SetActive(false);
    }


}
