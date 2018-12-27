// Author: Cameron Justice
// Github: https://github.com/cameron-justice

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashObject : MonoBehaviour {

	[SerializeField]
	private Light light;

	// Use this for initialization
	void Start () {
		light = GetComponent<Light>();
	}

	public void Activate(){
		StartCoroutine(Flash());
	}

	public IEnumerator Flash(){
		for(int i = 0; i < 2; i++){
			if(light.enabled){
				light.enabled = false;
				yield return null;
			}
			else{
				light.enabled = true;
				yield return new WaitForSeconds(2);
			}
		}
	}
}
