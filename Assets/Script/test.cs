using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour {
    int cnt = 0;
    public SerialHandler serialHandler;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        serialHandler.Write(cnt.ToString());
        cnt++;
    }
}
