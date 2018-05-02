using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager : MonoBehaviour
{
    public static int cnt = 0;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    void FixedUpdate()
    {
        cnt++;  //判定の回数をへらす
        if (cnt % 30 == 0)
        {
            cnt = 0;
        }
    }
}
