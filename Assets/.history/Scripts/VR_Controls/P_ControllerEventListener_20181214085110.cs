using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

public class P_ControllerEventListener : MonoBehaviour {

	[SerializeField]
	private P_PlayerController pControl;

	[SerializeField]
	private ControllerSides isHand;

	private VRTK_ControllerEvents cEvents;

    private bool canReportRotation = false;



	// Use this for initialization
	void Start () {
		cEvents = GetComponent<VRTK_ControllerEvents>();


		// Adds all the event handlers for actions only the left controller should do
		if(isHand == ControllerSides.LeftController){
			cEvents.TouchpadTouchStart += new ControllerInteractionEventHandler(ReportAnalogMovement);
			cEvents.TouchpadAxisChanged += new ControllerInteractionEventHandler(ReportAnalogMovement);
		}
		else if(isHand == ControllerSides.RightController){
			cEvents.TouchpadTouchStart += new ControllerInteractionEventHandler(EnableAnalogRotation);
            cEvents.TouchpadAxisChanged += new ControllerInteractionEventHandler(ReportAnalogRotation);
		}

	}

	void ReportAnalogMovement(object sender, ControllerInteractionEventArgs e){
		pControl.HandleAnalogMovement(e.touchpadAxis);
	}

	void ReportAnalogRotation(object sender, ControllerInteractionEventArgs e){
        if (canReportRotation)
        {
            pControl.HandleAnalogRotation(e.touchpadAxis.x);
        }
        canReportRotation = false;
	}

    void EnableAnalogRotation(object sender, ControllerInteractionEventArgs e)
    {
        canReportRotation = true;
    }
}
