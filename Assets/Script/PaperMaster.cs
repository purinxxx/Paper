﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaperMaster : MonoBehaviour {

    [SerializeField]
    private int PinIN1; //接続されているピン番号
    [SerializeField]
    private int PinIN2; //接続されているピン番号
    [SerializeField]
    private int PinVREF; //接続されているピン番号
    [SerializeField]
    private int RotateRate = 100;   //モーターの回転率　0なら完全に引っ込めた状態、100なら床まで出し切った状態、-1なら巻取り続ける
    [SerializeField]
    private GameObject ArduinoMaster;
    [SerializeField]
    private GameObject paper1;  //上から数える
    [SerializeField]
    private GameObject paper2;
    [SerializeField]
    private GameObject paper3;
    [SerializeField]
    private GameObject paper4;

    private int tmp = -1;
    private float on = 0f;
    private float off = 0.5f;

    // Use this for initialization
    void Start()
    {
        StartCoroutine("starttrue");
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        RotateRate = 100;
        if (paper4.GetComponent<Paper>().CollisionFlag)
        {
            RotateRate = 75;
        }
        if (paper3.GetComponent<Paper>().CollisionFlag)
        {
            RotateRate = 50;
        }
        if (paper2.GetComponent<Paper>().CollisionFlag)
        {
            RotateRate = 25;
        }
        if (paper1.GetComponent<Paper>().CollisionFlag)
        {
            RotateRate = 0;
        }

        if (RotateRate!=tmp)    //前フレームと値が変わっている場合
        {
            tmp = RotateRate;
            ArduinoMaster.GetComponent<ArduinoMaster>().SendArduino(PinIN1, PinIN2, PinVREF, RotateRate);   //モーターを動かす情報を引数で渡す
            if (RotateRate == 100)
            {
                paper1.GetComponent<MeshRenderer>().enabled = true;
                paper2.GetComponent<MeshRenderer>().enabled = true;
                paper3.GetComponent<MeshRenderer>().enabled = true;
                paper4.GetComponent<MeshRenderer>().enabled = true;
            }
            if (RotateRate == 75)
            {
                paper1.GetComponent<MeshRenderer>().enabled = true;
                paper2.GetComponent<MeshRenderer>().enabled = true;
                paper3.GetComponent<MeshRenderer>().enabled = true;
                paper4.GetComponent<MeshRenderer>().enabled = false;
            }
            if (RotateRate == 50)
            {
                paper1.GetComponent<MeshRenderer>().enabled = true;
                paper2.GetComponent<MeshRenderer>().enabled = true;
                paper3.GetComponent<MeshRenderer>().enabled = false;
                paper4.GetComponent<MeshRenderer>().enabled = false;
            }
            if (RotateRate == 25)
            {
                paper1.GetComponent<MeshRenderer>().enabled = true;
                paper2.GetComponent<MeshRenderer>().enabled = false;
                paper3.GetComponent<MeshRenderer>().enabled = false;
                paper4.GetComponent<MeshRenderer>().enabled = false;
            }
            if (RotateRate == 0)
            {
                paper1.GetComponent<MeshRenderer>().enabled = false;
                paper2.GetComponent<MeshRenderer>().enabled = false;
                paper3.GetComponent<MeshRenderer>().enabled = false;
                paper4.GetComponent<MeshRenderer>().enabled = false;
            }
        }
    }
    private IEnumerator starttrue()
    {
        // 1秒待つ  
        yield return new WaitForSeconds(1.0f);
        paper4.GetComponent<MeshRenderer>().enabled = true;
        paper3.GetComponent<MeshRenderer>().enabled = true;
        paper2.GetComponent<MeshRenderer>().enabled = true;
        paper1.GetComponent<MeshRenderer>().enabled = true;
    }
}
