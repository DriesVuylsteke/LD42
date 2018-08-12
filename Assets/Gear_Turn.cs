using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gear_Turn : MonoBehaviour {

	[System.Serializable]
	public enum Direction{
		Clockwise,CounterClockwise
	}

	public Direction dir;
	public float speed;
	
	// Update is called once per frame
	void Update () {
		if(dir == Direction.Clockwise)
			transform.Rotate (Vector3.back * Time.deltaTime * speed);
		else
			transform.Rotate (-Vector3.back * Time.deltaTime * speed);
	}
}
