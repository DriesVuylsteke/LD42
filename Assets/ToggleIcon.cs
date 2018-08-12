using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Toggle))]
public class ToggleIcon : MonoBehaviour {

	public Image imageToChange;
	public Sprite onIcon, offIcon;
	public bool state;

	void Start(){
		state = GetComponent<Toggle> ().isOn;
		if (state) {
			imageToChange.sprite = onIcon;
		} else {
			imageToChange.sprite = offIcon;
		}
	}

	public void Toggle(){
		state = GetComponent<Toggle> ().isOn;

		if (state) {
			imageToChange.sprite = onIcon;
		} else {
			imageToChange.sprite = offIcon;
		}
	}
}
