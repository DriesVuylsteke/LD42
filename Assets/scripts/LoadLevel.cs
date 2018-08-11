using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Button))]
public class LoadLevel : MonoBehaviour {

	Button but;
	public int levelID;

	// Use this for initialization
	void Start () {
		but = GetComponent<Button> ();
		but.onClick.AddListener (delegate {
			SceneLoad(levelID);
		});
	}

	private void SceneLoad(int levelID){
		SceneManager.LoadScene(levelID);
	}
}
