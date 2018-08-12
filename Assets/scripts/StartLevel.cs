using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartLevel : MonoBehaviour {


	// Use this for initialization
	void Start () {
		if (SceneManager.GetActiveScene ().buildIndex == 0)
			return;
		if (MusicManager.showHelp) {
			Time.timeScale = 0;
		} else {
				Time.timeScale = 1;
				gameObject.SetActive (false);
		}
	}
	
	// Update is called once per frame
	void LateUpdate () {
		if (Input.GetKeyDown(KeyCode.Space) && SceneManager.GetActiveScene().buildIndex != 0) {
			Time.timeScale = 1;
			gameObject.SetActive (false);
		}
	}

	public void ToggleHelpDisplay(){
		MusicManager.showHelp = !MusicManager.showHelp;
		Debug.Log (MusicManager.showHelp);
	}
}
