using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArduinoMaster : MonoBehaviour
{
    [SerializeField]
    private string ArduinoNum; //接続されているArduinoのシリアルポート

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void SendArduino(int PinIN1, int PinIN2, int PinVREF, int RotateRate)
    {
        Debug.Log(ArduinoNum + " : " + PinIN1.ToString() + " : " + PinIN2.ToString() + " : " + PinVREF.ToString() + " : " + RotateRate.ToString());
    }
}
