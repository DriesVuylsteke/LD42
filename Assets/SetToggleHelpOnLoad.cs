using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetToggleHelpOnLoad : MonoBehaviour {

	// Use this for initialization
	void Start () {
		bool old = MusicManager.showHelp;
		GetComponent<Toggle> ().isOn = old;
		MusicManager.showHelp = old;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
