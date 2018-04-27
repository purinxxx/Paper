using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Windows.Kinect;

public class KinectMaster : MonoBehaviour
{
    [SerializeField]
    BodySourceManager bodySourceManager;
    
    public static Body[] BodyList;
    public static Quaternion comp;

    // Use this for initialization
    void Start()
    { 

    }

    // Update is called once per frame
    void Update()
    {
        try
        {
            BodyList = bodySourceManager.GetData();
        }
        catch
        {

        }

        // Kinectを斜めに置いてもまっすぐにするようにする
        var floorPlane = bodySourceManager.FloorClipPlane;
        comp = Quaternion.FromToRotation(
            new Vector3(-floorPlane.X, floorPlane.Y, floorPlane.Z), Vector3.up);
    }
}
