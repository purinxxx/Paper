using UnityEngine;
using System.Collections;
using Uduino;

public class MultipleArduino : MonoBehaviour
{
    UduinoDevice firstDevice;
    UduinoDevice secondDevice;

    void Start()
    {
        //Set up the pinMode of the boards should not be done on Start() or Awake() 
        // but on the delegate function OnBoardConnected(). It should be called only if
        // you use several boards with the default Uduino arduino sketch.
        UduinoManager.Instance.OnBoardConnected += OnBoardConnected;
    }

    void Update()
    {
        if (UduinoManager.Instance.hasBoardConnected())
        {
            Debug.Log(firstDevice.name);
            Debug.Log(secondDevice.name);
        }
    }
    

    void OnBoardConnected(UduinoDevice connectedDevice)
    {
        if (connectedDevice.name == "FirstBoard")
        {
            Debug.Log("First");
            firstDevice = connectedDevice;
            StartCoroutine("motor");
        }
        else if (connectedDevice.name == "SecondBoard")
        {
            Debug.Log("Second");
            secondDevice = connectedDevice;
            StartCoroutine("motor2");
        }
    }

    private IEnumerator motor()
    {
        yield return new WaitForSeconds(5.0f);
        //firstDevice = UduinoManager.Instance.GetBoard("FirstBoard");
        yield return new WaitForSeconds(1.0f);
        UduinoManager.Instance.digitalWrite(firstDevice, 4, State.HIGH);
        yield return new WaitForSeconds(1.0f);
        UduinoManager.Instance.analogWrite(firstDevice, 9, 55);
        yield return new WaitForSeconds(1.0f);
        UduinoManager.Instance.digitalWrite(firstDevice, 8, State.HIGH);
        yield return new WaitForSeconds(1.0f);
        UduinoManager.Instance.digitalWrite(firstDevice, 7, State.LOW);
        yield return new WaitForSeconds(1.0f);

    }

    private IEnumerator motor2()
    {
        yield return new WaitForSeconds(5.0f);
        //secondDevice = UduinoManager.Instance.GetBoard("SecondBoard");
        yield return new WaitForSeconds(1.0f);
        UduinoManager.Instance.analogWrite(secondDevice, 9, 255);
        yield return new WaitForSeconds(1.0f);
        UduinoManager.Instance.digitalWrite(secondDevice, 8, State.LOW);
        yield return new WaitForSeconds(1.0f);
        UduinoManager.Instance.digitalWrite(secondDevice, 7, State.HIGH);
        yield return new WaitForSeconds(1.0f);
    }

}
