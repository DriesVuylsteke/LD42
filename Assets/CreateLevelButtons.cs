using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CreateLevelButtons : MonoBehaviour {

	public GameObject levelSelectButton;
	public GameObject levelContainer;

	// Use this for initialization
	void Start () {
		int totalLevels = SceneManager.sceneCountInBuildSettings;

		int curLevel = 1;

		Debug.Log ("There are " + totalLevels + " levels"); 

		while (curLevel < totalLevels) {
			GameObject go = Instantiate (levelSelectButton, levelContainer.transform);
			go.GetComponentInChildren<Text> ().text = curLevel.ToString ();
			Button but = go.GetComponent<Button> ();
			but.onClick.RemoveAllListeners ();
			int refIssue = curLevel;
			but.onClick.AddListener (delegate {
				LoadLevel(refIssue);
			});
			curLevel++;
		}
	}

	void LoadLevel(int level){
		SceneManager.LoadScene (level);
	}
}
