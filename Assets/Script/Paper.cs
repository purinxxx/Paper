using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paper : MonoBehaviour {

    public bool CollisionFlag;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        //if (CollisionFlag) CollisionFlag = false;
    }

    void OnTriggerEnter(Collider other)
    {
        CollisionFlag = true;
    }

    void OnTriggerStay(Collider other)
    {
        if (!CollisionFlag) CollisionFlag = true;
    }

    void OnTriggerExit(Collider other)
    {
        CollisionFlag = false;
    }

}
