// Author: Cameron Justice
// Github: https://github.com/cameron-justice

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomActivation : MonoBehaviour {

    [SerializeField, Tooltip("The number that will be 1 in X. This is X")]
    private int oddsMax;

    [SerializeField]
    private List<GameObject> toActivate;

    [SerializeField]
    private int framesBetweenRoll = 50;
    private int framesPassed = 0;
    private int numberToRoll;

    private bool canActivate = true;
    private int framesBetweenCanActivate = 50;

    private int rollCount;
    private ActivateEmission[] activat;

    // Use this for initialization
    void Start() {
        Random.InitState((int)Time.time);
        numberToRoll = (int)(Random.value * oddsMax);
        toActivate = GetComponent<CreateGridOfObject>().clones;
        oddsMax = toActivate.Count; rollCount = oddsMax / 8 * Random.Range(1,8);
        activat = new ActivateEmission[oddsMax];
        for(int i = 0; i < oddsMax; i++){
            activat[i] = toActivate[i].GetComponentInChildren<ActivateEmission>();
        }
    }
	
	// Update is called once per frame
	void Update () {
        if(framesPassed == framesBetweenRoll && canActivate)
        {
            int[] roll = new int[rollCount];
            for(int i = 0; i < rollCount; i++){
                roll[i] = Random.Range(0, oddsMax);
            }

            for(int i = 0; i < rollCount; i++){
                activat[roll[i]].Activate();
            }
            
            framesPassed = 0;
            canActivate = false;
        }
        else if(framesPassed == framesBetweenCanActivate && !canActivate)
        {
            canActivate = true;
            framesPassed = 0;
        }
        else
        {
            framesPassed++;
        }
    }
}
