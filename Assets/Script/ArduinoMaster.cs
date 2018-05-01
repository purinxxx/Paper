using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArduinoMaster : MonoBehaviour
{
    public SerialHandler serialHandler;

    [SerializeField]
    private GameObject ToiletPaper1;  //上から数える
    [SerializeField]
    private GameObject ToiletPaper2;
    [SerializeField]
    private GameObject ToiletPaper3;
    [SerializeField]
    private GameObject ToiletPaper4;
    [SerializeField]
    private GameObject ToiletPaper5;
    [SerializeField]
    private GameObject ToiletPaper6;

    private int[] PrevRotateRate = new int[6];
    private GameObject[] papers = new GameObject[6];
    private float speed = 0.1f;

    // Use this for initialization
    void Start ()
    {
        papers[0] = ToiletPaper1;
        papers[1] = ToiletPaper2;
        papers[2] = ToiletPaper3;
        papers[3] = ToiletPaper4;
        papers[4] = ToiletPaper5;
        papers[5] = ToiletPaper6;

        for(int i = 0; i < 6; i++)
        {
            PrevRotateRate[i] = papers[i].GetComponent<PaperMaster>().RotateRate;
        }
    
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            serialHandler.Write("0");
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            serialHandler.Write("1");
        }
    }

    public void SendArduino(int motor, int RotateRate)
    {
        if (RotateRate > PrevRotateRate[motor - 1])  //出す　正転
        {
            float s = (RotateRate - PrevRotateRate[motor - 1]) / 25;
            Debug.Log(s);
            StartCoroutine(kaiten(1, motor, s*speed));
        }
        else if (RotateRate < PrevRotateRate[motor - 1])  //巻き取る 逆転
        {
            float s = (PrevRotateRate[motor - 1] - RotateRate) / 25;
            Debug.Log(s);
            StartCoroutine(kaiten(2, motor, s * speed));
        }
        PrevRotateRate[motor-1] = RotateRate;
        Debug.Log(motor.ToString() + " : " + RotateRate.ToString());
    }

    private IEnumerator kaiten(int mode, int motor ,float s)
    {
        s = Mathf.Abs(s);
        yield return new WaitForSeconds(0.1f);
        serialHandler.Write((mode*10 + motor).ToString());
        Debug.Log((mode * 10 + motor).ToString());
        Debug.Log(s);
        yield return new WaitForSeconds(s);
        serialHandler.Write((30 + motor).ToString());
        Debug.Log((30 + motor).ToString());
    }
}
