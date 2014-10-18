using UnityEngine;
using System.Collections;

public class GyroTest : MonoBehaviour
{
	public bool gyroOn = false;

    private Gyroscope gyo1;
    private bool gyoBool, gyoOn;
	private Quaternion origQuaternion;
    //private Quaternion rotFix;

    // Use this for initialization
    void Start ()
    {
        gyoBool = SystemInfo.supportsGyroscope;
		gyoOn = false;
		origQuaternion = transform.rotation;

        Debug.Log ("is Gyro support: " + gyoBool.ToString ());

        if(gyoBool)
        {
            gyo1 = Input.gyro;
            gyo1.enabled = true;
        }
    }

    // Update is called once per frame
    void Update ()
    {
		if (!gyoOn)
		{
			return;
		}

        gyo1=Input.gyro;

        float x = gyo1.rotationRate.x;
        float y = gyo1.rotationRate.y;
        float z = gyo1.rotationRate.z;

        transform.Rotate(new Vector3(-x, -y, z));
    }

    void OnGUI ()
    {
        if (gyoBool != null) 
        {
            GUI.Label (new Rect (10, Screen.height / 2 - 50, 100, 100), gyoBool.ToString ());
            if (gyoBool == true) 
            {
                GUI.Label (new Rect (10, Screen.height / 2-100, 500, 100), "gyro supported");
                GUI.Label (new Rect (10, Screen.height / 2, 500, 100), "rotation rate:" + gyo1.rotationRate.ToString ());
                GUI.Label (new Rect (10, Screen.height / 2 + 50, 500, 100), "gravity:      " + gyo1.gravity.ToString ());
                GUI.Label (new Rect (10, Screen.height / 2 + 100, 500, 100), "attitude:     " + gyo1.attitude.ToString ());
                GUI.Label (new Rect (10, Screen.height / 2 + 150, 500, 100), "type:         " + gyo1.GetType ().ToString ());
            } 
            else
                GUI.Label (new Rect (Screen.width / 2 - 100, Screen.height / 2, 100, 100), "not supported");
        }
    }

	public void startGyro()
	{
		gyoOn = true;
		GameObject.FindGameObjectWithTag ("Player").GetComponent<MummyController> ().gyroOn = true;
	}

	public void endGyro()
	{
		gyoOn = false;
		transform.rotation = origQuaternion;
		GameObject.FindGameObjectWithTag ("Player").GetComponent<MummyController> ().gyroOn = false;
	}
}  