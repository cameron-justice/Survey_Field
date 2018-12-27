using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateEmission : MonoBehaviour {

	private Renderer rend;

	// Use this for initialization
	void Start () {
		rend = GetComponent<Renderer>();
	}

	public void Activate(){
		StartCoroutine(Flash());
	}

	public IEnumerator Flash(){
		for(int i = 0; i < 2; i++){
			if(rend.material.IsKeywordEnabled("_EMISSION")){
				rend.material.DisableKeyword("_EMISSION");
				yield return null;
			}
			else{
				rend.material.EnableKeyword("_EMISSION");
				yield return new WaitForSeconds(2);
			}
		}
	}
}
