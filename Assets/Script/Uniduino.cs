using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Uniduino : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void SendArduino(int PinIN1, int PinIN2, int PinVREF, int RotateRate)
    {
        Debug.Log(PinIN1.ToString() + " : " + PinIN2.ToString() + " : " + PinVREF.ToString() + " : " + RotateRate.ToString());
    }
}
