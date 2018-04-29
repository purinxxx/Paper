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
        //this.gameObject.transform.localScale = new Vector3(0.114f, 0.5f, 0.1f);
    }

    void OnTriggerStay(Collider other)
    {
        if (!CollisionFlag) CollisionFlag = true;
    }

    void OnTriggerExit(Collider other)
    {
        CollisionFlag = false;
        //this.gameObject.transform.localScale = new Vector3(0.114f, 0.1f, 0.1f);
    }

}
