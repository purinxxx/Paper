using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArduinoMaster : MonoBehaviour {

    [SerializeField]
    private string ArduinoNum; //接続されているArduinoのシリアルポート
    [SerializeField]
    private int PinIN1; //接続されているピン番号
    [SerializeField]
    private int PinIN2; //接続されているピン番号
    [SerializeField]
    private int PinVREF; //接続されているピン番号
    [SerializeField]
    private int RotateRate;   //モーターの回転率　0なら完全に引っ込めた状態、100なら床まで出し切った状態、-1なら巻取り続ける
    [SerializeField]
    private GameObject Uniduino;
    [SerializeField]
    private GameObject paper1;  //上から数える
    [SerializeField]
    private GameObject paper2;
    [SerializeField]
    private GameObject paper3;
    [SerializeField]
    private GameObject paper4;

    private int tmp = -1;

    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
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
            Uniduino.GetComponent<Uniduino>().SendArduino(PinIN1, PinIN2, PinVREF, RotateRate);   //モーターを動かす情報を引数で渡す
        }
    }
}
