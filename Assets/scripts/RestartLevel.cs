using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartLevel : MonoBehaviour {

	void Start(){
		//Debug.Log (Physics2D.gravity);
		//Physics2D.gravity = new Vector2 (0, -9.81f);
	}

	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.R)) {
			SceneManager.LoadScene (SceneManager.GetActiveScene ().buildIndex);
			Physics2D.gravity = new Vector2 (0, -9.81f);
		}
	}
}
