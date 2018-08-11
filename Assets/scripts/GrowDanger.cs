using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrowDanger : MonoBehaviour {

	public Vector3 growSpeed;
	
	// Update is called once per frame
	void Update () {
		Vector3 curScale = transform.localScale;
		curScale += growSpeed * Time.deltaTime;
		transform.localScale = curScale;
	}
}
