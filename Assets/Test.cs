using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour {

	int testInt = 0;

	// Use this for initialization
	void Start () {

		Debug.LogWarning (testInt);
		GetIntValue (testInt);
		Debug.LogWarning (testInt);

	}

	private void GetIntValue(int outValue)
	{
		//outValue++;
		outValue += 1;
	}

	// Update is called once per frame
	void Update () {
		
	}
}
