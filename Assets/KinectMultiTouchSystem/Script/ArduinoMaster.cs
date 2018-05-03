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
    [SerializeField]
    [Range(0.1f, 2f)]
    private float speed = 0.1f;
    private List<int> commands = new List<int>();
    public bool moving = false;
    private int cnt = 0;

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
            serialHandler.Write("11");
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            serialHandler.Write("21");
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            serialHandler.Write("31");
        }
        if (Input.GetKeyDown(KeyCode.F))
        {
            serialHandler.Write("2541");
        }
        if (Input.GetKeyDown(KeyCode.G))
        {
            serialHandler.Write("25541");
        }
    }
    void FixedUpdate()
    {
        cnt++;  //判定の回数をへらす
        if (cnt % 30 == 0)
        {
            cnt = 0;
            if (moving)
            {

            }
            else
            {
                if (commands.Count > 0)
                {
                    foreach (int c in commands)
                    {
                        Debug.Log("commandList : " + c.ToString());
                    }
                    StartCoroutine(Send(commands));
                }
            }
        }
    }

    public void SendArduino(int motor, int RotateRate)
    {
        if (RotateRate > PrevRotateRate[motor - 1])  //出す　正転
        {
            int s = Mathf.Abs((RotateRate - PrevRotateRate[motor - 1]) / 25);
            //Debug.Log(s);
            commands.Add(1 * 100 + motor * 10 + s);
        }
        else if (RotateRate < PrevRotateRate[motor - 1])  //巻き取る 逆転
        {
            int s = Mathf.Abs((PrevRotateRate[motor - 1] - RotateRate) / 25);
            //Debug.Log(s);
            commands.Add(2 * 100 + motor * 10 + s);
        }
        PrevRotateRate[motor - 1] = RotateRate;
        //Debug.Log(motor.ToString() + " : " + RotateRate.ToString());
    }

    private IEnumerator kaiten(int command)
    {
        int time = command % 10;
        int motor = command /10 % 10;
        int mode = command / 100 % 10;
        serialHandler.Write((mode * 10 + motor).ToString());
        //Debug.Log(command.ToString() + "  :  " + (mode * 10 + motor).ToString());
        //Debug.Log(s);
        //float waittiem = (float)time * speed;
        yield return new WaitForSeconds(timespeed(command, speed));
        serialHandler.Write((30 + motor).ToString());
        Debug.Log(command.ToString() + "  :  " + (30 + motor).ToString());
    }

    private IEnumerator Send(List<int> cs)
    {
        moving = true;
        float t = 0;
        foreach (int c in cs)    //モード、モーター、タイム
        {
            t = Mathf.Max(t, timespeed(c, speed));
            Debug.Log("command : " + c.ToString());
            StartCoroutine(kaiten(c));
            yield return new WaitForSeconds(0.1f);
        }
        yield return new WaitForSeconds(t * speed + cs.Count * 0.1f);
        yield return new WaitForSeconds(0.5f);
        commands.Clear();
        Debug.Log("Clear");
        moving = false;
    }

   private  float timespeed(int command, float speed)
    {
        int time = command % 10;
        float ans = 0;
        if (time == 1) ans = 1.3f;
        else if (time == 2) ans = 1f;
        else if (time == 3) ans = 0.9f;
        else if (time == 4) ans = 0.8f;
        if (command/100%10==2) ans*=1.4f;   //増やすと巻取り時の秒数伸びる
        return ans * (float)time * speed;
    }

}
