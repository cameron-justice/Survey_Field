// Author: Cameron Justice
// Github: https://github.com/cameron-justice

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

public class PointAtPlayer : MonoBehaviour {

	[SerializeField]
	private GameObject player;

	void Start()
	{
		player = GameObject.FindGameObjectWithTag("Player").GetComponent<P_PlayerController>().vrCurrentRig;
	}

	// Update is called once per frame
	void Update () {
		transform.LookAt(player.transform);
		transform.rotation = new Quaternion(0, transform.rotation.y, 0, transform.rotation.w);
	}
}
