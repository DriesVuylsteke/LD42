using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Collider2D))]
public class WinGamePortal : MonoBehaviour {

	void OnTriggerEnter2D(Collider2D other){
		if (other.CompareTag ("Player")) {
			int maxScenes = SceneManager.sceneCountInBuildSettings;
			int curScene = SceneManager.GetActiveScene ().buildIndex;
			if (curScene == maxScenes - 1) {
				SceneManager.LoadScene (0);
			} else {
				SceneManager.LoadScene (curScene + 1);	
			}
		}
	}
}
