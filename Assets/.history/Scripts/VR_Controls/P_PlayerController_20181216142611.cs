using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

/// <summary>
/// Used to choose in inspector which controller will be used for certain actions
/// </summary>
public enum ControllerSides {LeftController, RightController}

public class P_PlayerController : MonoBehaviour {

	[Header("Gameplay Values")]
	public float moveSpeed = 5f;
    public int rotateAngle = 180;

	public bool disableHeadsetPositionTracking = true;
	public ControllerSides positionalMovementController = ControllerSides.LeftController;

	public GameObject vrCurrentRig;

    [SerializeField]
	private GameObject vrMasterObject;
    [SerializeField]
    private GameObject centerEye;
	private GameObject leftCon;
	private GameObject rightCon;
	private GameObject primaryMoveCon; // The Controller that will 
	private VRTK_SDKSetup currentSetup;

	private Vector2 currentMoveAxisValue;


	// Use this for initialization
	void Start () {
		// Disable the user's ability to move the player by moving themselves
		if(disableHeadsetPositionTracking){
			UnityEngine.XR.InputTracking.disablePositionalTracking = true;
		}


		leftCon = VRTK.VRTK_DeviceFinder.GetControllerLeftHand();
		rightCon = VRTK.VRTK_DeviceFinder.GetControllerRightHand();

		// Set which controller will be used for positional movement
		if(positionalMovementController == ControllerSides.RightController){
			primaryMoveCon = rightCon;
		} else {
			primaryMoveCon = leftCon;
		}
	}
	
	// Update is called once per frame
	void Update () {
		float currZ = currentMoveAxisValue.y;
		float currX = currentMoveAxisValue.x;

        Vector3 move = (centerEye.transform.forward * currZ) + (centerEye.transform.right * currX);
		move *= moveSpeed * Time.deltaTime;
		if(currentMoveAxisValue != Vector2.zero){
			vrMasterObject.transform.position += new Vector3(move.x, 0, move.z);
		}
	}

	public void HandleAnalogMovement(Vector2 move){
		currentMoveAxisValue = move;
	}

	public void HandleAnalogRotation(float x){
		if(x > 0){
            vrMasterObject.transform.Rotate(-Vector3.down * rotateAngle, Space.Self);
            Debug.Log("X is >0");
		}
		else if(x < 0){
			vrMasterObject.transform.Rotate(Vector3.down * rotateAngle, Space.Self);
		}
	}
}
