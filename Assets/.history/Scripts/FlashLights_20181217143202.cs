// Author: Cameron Justice
// Github: https://github.com/cameron-justice

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashLights : MonoBehaviour {

	[SerializeField]
	private int secondsBetweenFlash = 2;

	[SerializeField]
	private List<GameObject> pointLights; //List of Actual lights

	private List<ActivateEmission> emissives; //List of AE scripts on objects to activate
	private List<FlashObject> flashes;		  //List of FO scripts on objects to activate


	// Use this for initialization
	void Start () {
		List<GameObject> objs = GetComponent<CreateGridOfObject>().clones;
		emissives = new List<ActivateEmission>();
		flashes = new List<FlashObject>();
		for(int i = 0; i < objs.Count; i++){
			ActivateEmission ae = objs[i].GetComponentInChildren<ActivateEmission>();
			emissives.Add(ae);
		}

		for(int i = 0; i < pointLights.Count; i++){
			FlashObject fo = pointLights[i].GetComponentInChildren<FlashObject>();
			flashes.Add(fo);
		}

		StartCoroutine("Flash");
	}
	
	public IEnumerator Flash(){
		while(true){
			for(int i = 0; i < emissives.Count; i++){
				emissives[i].Activate();
			}

			for(int i = 0; i < flashes.Count; i++){
				flashes[i].Activate();
			}

			yield return new WaitForSeconds(secondsBetweenFlash);
		}
	}
}
