// Author: Cameron Justice
// Github: https://github.com/cameron-justice

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

/// <summary>
/// Used to choose in inspector which controller will be used for certain actions
/// NOTE: Currently only used in P_ControllerEventListener.cs
/// </summary>
public enum ControllerSides {LeftController, RightController}

public class P_PlayerController : MonoBehaviour {

	[Header("Gameplay Values")]
	public float moveSpeed = 5f;
    public int rotateAngle = 180;
	public float returnFromFallPoint = -5f;

	[Header("Headset References and Options")]
	public bool disableHeadsetPositionTracking = true;	// Whether or not the player's real world movements should move the character
	public GameObject vrCurrentRig; 					// Since VRTK won't give me the rig, this will hold a reference to the rig being tested at the moment
	public GameObject centerEye; 						// Reference to the center eye camera of the VR rig

	private Vector2 currentMoveAxisValue; 				// Value the player should move every frame
	private Vector3 spawnPoint; 						// Wherever the player started
	private Quaternion spawnRotation;					// Where the player was facing at start

	VRTK_HeadsetFade headsetFade;						// Manager for headset fading / screen fading

	// Use this for initialization
	void Start () {
		
		// Disable the user's ability to move the player by moving themselves
		if(disableHeadsetPositionTracking){
			UnityEngine.XR.InputTracking.disablePositionalTracking = true;
		}

		spawnPoint = vrCurrentRig.transform.position;	// Save spawn location
		spawnRotation = vrCurrentRig.transform.rotation;// Save spawn rotation

		headsetFade = vrCurrentRig.GetComponent<VRTK_HeadsetFade>();
	}
	
	// Update is called once per frame
	void Update () {
		float currZ = currentMoveAxisValue.y;
		float currX = currentMoveAxisValue.x;

        Vector3 move = (centerEye.transform.forward * currZ) + (centerEye.transform.right * currX);
		move *= moveSpeed * Time.deltaTime;
		if(currentMoveAxisValue != Vector2.zero){
			vrCurrentRig.transform.position += new Vector3(move.x, 0, move.z);
		}

		// Make sure the player resets to the top if they fall off
		if(vrCurrentRig.transform.position.y < returnFromFallPoint){
			StartCoroutine("ResetToSpawn");
		}

		if(Input.GetKeyDown(KeyCode.P)){
			TakeScreenshot();
		}
	}

	public void HandleAnalogMovement(Vector2 move){
		currentMoveAxisValue = move;
	}

	public void HandleAnalogRotation(float x){
		if(x > 0){
            vrCurrentRig.transform.Rotate(-Vector3.down * rotateAngle, Space.Self);
            Debug.Log("X is >0");
		}
		else if(x < 0){
			vrCurrentRig.transform.Rotate(Vector3.down * rotateAngle, Space.Self);
		}
	}

	public void TakeScreenshot(){
		ScreenCapture.CaptureScreenshot(string.Format("{0}/SS_{1}.png", ScreenshotManager.SSM.screenshotPath, System.DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss")));
		Debug.Log("Taking Screenshot");
	}

	public IEnumerator ResetToSpawn(){
		if(headsetFade.IsFaded()){
			headsetFade.Unfade(2);
			
			vrCurrentRig.transform.position = spawnPoint;
			vrCurrentRig.transform.rotation = spawnRotation;
			yield return null;
		}
		else{
			headsetFade.Fade(Color.black, 2);
			yield return new WaitForSeconds(2);
		}
	}
}
