// Author: Cameron Justice
// Github: https://github.com/cameron-justice

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class ScreenshotManager : MonoBehaviour {

	public string screenshotPath = "Screenshots";

	public static ScreenshotManager SSM;

	// Use this for initialization
	void Start () {

		if(SSM == null){
			SSM = this;
		}
		else
		{
			DestroyImmediate(this.gameObject);
		}

		if(!Directory.Exists(screenshotPath)){
			Directory.CreateDirectory(screenshotPath);
		}
	}
}
