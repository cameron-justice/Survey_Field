// Author: Cameron Justice
// Github: https://github.com/cameron-justice

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Clearing {
	public int startX;
	public int endX;
	public int startZ;
	public int endZ;
}

public class CreateGridOfObject : MonoBehaviour {

	[SerializeField, Tooltip("Object to be used to make the grid")]
	private GameObject objectPrefab;

	[SerializeField]
	private Vector3 origin;
	[SerializeField, Tooltip("Spacing between object instances in the X field")]
	private float xSpacing = 1;
	[SerializeField, Tooltip("Spacing between object instances in the Y field")]
	private float ySpacing = 1;
	[SerializeField, Tooltip("Spacing between object instances in the Z field")]
	private float zSpacing = 1;

	[SerializeField, Tooltip("Amount of instances made in the positive X direction past the origin")]
	private int positiveXSpread = 0;	
	[SerializeField, Tooltip("Amount of instances made in the negative X direction past the origin")]
	private int negativeXSpread = 0;	
	[SerializeField, Tooltip("Amount of instances made in the positive Y direction past the origin")]
	private int positiveYSpread = 0;
	[SerializeField, Tooltip("Amount of instances made in the negative Y direction past the origin")]
	private int negativeYSpread = 0;
	[SerializeField, Tooltip("Amount of instances made in the positive Z direction past the origin")]
	private int positiveZSpread = 0;
	[SerializeField, Tooltip("Amount of instances made in the negative Z direction past the origin")]
	private int negativeZSpread = 0;

	[Header("Clearings")]
	public List<Clearing> clearings;

	[HideInInspector]
	public List<GameObject> clones;

	// Use this for initialization
	void Awake () {

		clones = new List<GameObject>();

		GameObject parent = new GameObject(objectPrefab.name + " clones");
		bool place = true;
		bool inX;
		bool inZ;

		for(int i = -negativeXSpread; i <= (positiveXSpread); i++){ // X loop
			for(int j = -negativeYSpread; j <= (positiveYSpread); j++){ // Y loop
				for(int k = -negativeZSpread; k <= (positiveZSpread); k++){ // Z loop
					// Loop through every Clearing and see if this point falls within one. If so, don't place the object.
					for(int l = 0; l < clearings.Count; l++){

						inX = clearings[l].startX <= i && i <= clearings[l].endX;
						inZ = clearings[l].startZ <= k && k <= clearings[l].endZ;
						if(inX && inZ){
							place = false;
						}
					}

					if(place){
						// Place the object instance at the x,y,z location in relation to the origin with spacing applied and rotation same as prefab
						GameObject go = Instantiate(objectPrefab, origin + new Vector3(i*xSpacing,j*ySpacing,k*zSpacing), Quaternion.identity);
						// Organize the object instances under the parent
						go.transform.parent = parent.transform;
						clones.Add(go);
					}
					
					// Reset place
					place = true;

				}
			}
		}	
	}
}
