using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CurrentLevelDisplay : MonoBehaviour {

	// Use this for initialization
	void Update () {
		GetComponent<Text> ().text = SceneManager.GetActiveScene ().buildIndex.ToString();
	}
}
