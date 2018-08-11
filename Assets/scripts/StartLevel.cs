using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartLevel : MonoBehaviour {

	public static bool showHelp = true;
	// Use this for initialization
	void Start () {
		if (StartLevel.showHelp) {
			Time.timeScale = 0;
		} else {
			Time.timeScale = 1;
			gameObject.SetActive (false);
		}
	}
	
	// Update is called once per frame
	void LateUpdate () {
		if (Input.GetKeyDown(KeyCode.Space)) {
			Time.timeScale = 1;
			gameObject.SetActive (false);
		}
	}

	public void ToggleHelpDisplay(){
		StartLevel.showHelp = !StartLevel.showHelp;
		Debug.Log (StartLevel.showHelp);
	}
}
