using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gyroscope : MonoBehaviour
{

    public GameObject platform;

    public float smoothingValue = 0.2f;

    private LowPassFilter filterX;
    private LowPassFilter filterY;
    private LowPassFilter filterZ;

    public Vector3 filteredGyro { get; private set; }

    void Start()
    {
        filterX = new LowPassFilter(smoothingValue);
        filterY = new LowPassFilter(smoothingValue);
        filterZ = new LowPassFilter(smoothingValue);

        filteredGyro = Vector3.zero;

        Input.gyro.enabled = true;
    }

    void Update()
    {
        float filteredX = filterX.NextStep(Time.deltaTime, Input.gyro.rotationRateUnbiased.x);
        float filteredY = filterY.NextStep(Time.deltaTime, Input.gyro.rotationRateUnbiased.y);
        float filteredZ = filterZ.NextStep(Time.deltaTime, Input.gyro.rotationRateUnbiased.z);

        filteredGyro = filteredGyro + new Vector3(-filteredX, -filteredY, filteredZ);

        Camera.main.transform.localEulerAngles = filteredGyro;
    }

    // FOR DEBUGGING GYROSCOPE
    //void OnGUI()
    //{
    //    GUI.skin.label.fontSize = Screen.width / 20;

    //    GUILayout.Label("Orientation: " + Screen.orientation);
    //    GUILayout.Label("input.gyro.attitude: " + Input.gyro.rotationRateUnbiased);
    //}

    /************  IF USING GYRO.ATTITUDE  ********************************/

    // The Gyroscope is right-handed.  Unity is left handed.
    // Make the necessary change to the camera.
    //void GyroModifyCamera()
    //{
    //    Camera.main.transform.rotation = GyroToUnity(Input.gyro.attitude);
    //}

    //private static Quaternion GyroToUnity(Quaternion q)
    //{
    //    return new Quaternion(q.x, q.y, -q.z, -q.w);
    //}
}