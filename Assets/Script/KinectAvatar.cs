using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Windows.Kinect;

public class KinectAvatar : MonoBehaviour
{
    //自分の関節とUnityちゃんのボーンを入れるよう
    [SerializeField]
    GameObject UnityChan;
    [SerializeField]
    GameObject Ref;
    [SerializeField]
    GameObject LeftUpLeg;
    [SerializeField]
    GameObject LeftLeg;
    [SerializeField]
    GameObject RightUpLeg;
    [SerializeField]
    GameObject RightLeg;
    [SerializeField]
    GameObject Spine1;
    [SerializeField]
    GameObject LeftArm;
    [SerializeField]
    GameObject LeftForeArm;
    [SerializeField]
    GameObject LeftHand;
    [SerializeField]
    GameObject RightArm;
    [SerializeField]
    GameObject RightForeArm;
    [SerializeField]
    GameObject RightHand;

    [SerializeField]
    int num;

    private Body body;

    // Use this for initialization
    void Start()
    {
        UnityChan.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

        Quaternion SpineBase;
        Quaternion SpineMid;
        Quaternion SpineShoulder;
        Quaternion ShoulderLeft;
        Quaternion ShoulderRight;
        Quaternion ElbowLeft;
        Quaternion WristLeft;
        Quaternion HandLeft;
        Quaternion ElbowRight;
        Quaternion WristRight;
        Quaternion HandRight;
        Quaternion KneeLeft;
        Quaternion AnkleLeft;
        Quaternion KneeRight;
        Quaternion AnkleRight;

        Quaternion q;
        Quaternion comp2;
        CameraSpacePoint pos;


        try
        {
            body = KinectMaster.BodyList[num - 1];
            if (body.IsTracked && !UnityChan.activeSelf)
            {
                UnityChan.SetActive(true);
            }
            if (!body.IsTracked && UnityChan.activeSelf)
            {
                Ref.GetComponent<Rigidbody>().AddForce(-transform.forward * 100);  //トイレットペーパーの判定をexitするために１秒間ぶっ飛ばす
                StartCoroutine("SetActiveFalse");
            }
        }
        catch
        {

        }

        // 関節の回転を取得する
        if (body != null && body.IsTracked)
        {
            var joints = body.JointOrientations;
            SpineBase = joints[JointType.SpineBase].Orientation.ToQuaternion(KinectMaster.comp);
            SpineMid = joints[JointType.SpineMid].Orientation.ToQuaternion(KinectMaster.comp);
            SpineShoulder = joints[JointType.SpineShoulder].Orientation.ToQuaternion(KinectMaster.comp);
            ShoulderLeft = joints[JointType.ShoulderLeft].Orientation.ToQuaternion(KinectMaster.comp);
            ShoulderRight = joints[JointType.ShoulderRight].Orientation.ToQuaternion(KinectMaster.comp);
            ElbowLeft = joints[JointType.ElbowLeft].Orientation.ToQuaternion(KinectMaster.comp);
            WristLeft = joints[JointType.WristLeft].Orientation.ToQuaternion(KinectMaster.comp);
            HandLeft = joints[JointType.HandLeft].Orientation.ToQuaternion(KinectMaster.comp);
            ElbowRight = joints[JointType.ElbowRight].Orientation.ToQuaternion(KinectMaster.comp);
            WristRight = joints[JointType.WristRight].Orientation.ToQuaternion(KinectMaster.comp);
            HandRight = joints[JointType.HandRight].Orientation.ToQuaternion(KinectMaster.comp);
            KneeLeft = joints[JointType.KneeLeft].Orientation.ToQuaternion(KinectMaster.comp);
            AnkleLeft = joints[JointType.AnkleLeft].Orientation.ToQuaternion(KinectMaster.comp);
            KneeRight = joints[JointType.KneeRight].Orientation.ToQuaternion(KinectMaster.comp);
            AnkleRight = joints[JointType.AnkleRight].Orientation.ToQuaternion(KinectMaster.comp);
            

            // 関節の回転を計算する 
            q = transform.rotation;
            transform.rotation = Quaternion.identity;

            comp2 = Quaternion.AngleAxis(90, new Vector3(0, 1, 0)) *
                             Quaternion.AngleAxis(-90, new Vector3(0, 0, 1));

            Spine1.transform.rotation = SpineMid * comp2;

            RightArm.transform.rotation = ElbowRight * comp2;
            RightForeArm.transform.rotation = WristRight * comp2;
            RightHand.transform.rotation = HandRight * comp2;

            LeftArm.transform.rotation = ElbowLeft * comp2;
            LeftForeArm.transform.rotation = WristLeft * comp2;
            LeftHand.transform.rotation = HandLeft * comp2;

            RightUpLeg.transform.rotation = KneeRight * comp2;
            RightLeg.transform.rotation = AnkleRight * comp2;

            LeftUpLeg.transform.rotation = KneeLeft *
                            Quaternion.AngleAxis(-90, new Vector3(0, 0, 1));

            LeftLeg.transform.rotation = AnkleLeft *
                            Quaternion.AngleAxis(-90, new Vector3(0, 0, 1));

            // モデルの回転を設定する
            transform.rotation = q;

            // モデルの位置を移動する
            pos = body.Joints[JointType.SpineMid].Position;
            Ref.transform.localPosition = new Vector3(pos.X, pos.Y, -pos.Z);
        }
    }

    private IEnumerator SetActiveFalse()
    {
        // 1秒待つ  
        yield return new WaitForSeconds(1.0f);
        UnityChan.SetActive(false);
    }
}