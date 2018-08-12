using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenLevelSelect : MonoBehaviour {

	public GameObject levelSelectScreen;
	public GameObject canvas;

	public void OpenLevelSelectScreen(){
		levelSelectScreen.SetActive (true);
		canvas.SetActive (false);
	}
}
