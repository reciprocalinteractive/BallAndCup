using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DepthMovement : MonoBehaviour {

    public GameObject platform;
    public float smoothingValue = 0.4f;
    public float speed = 1.0f;
    public float threshold = 0.15f;

    private Vector3 platformForce;
    private LowPassFilter filterZ;

    void Awake () {
        filterZ = new LowPassFilter(smoothingValue);
    }

    void Update () {
        float filteredZ = filterZ.NextStep(Time.deltaTime, Input.acceleration.z);

        platformForce = platform.transform.position;

        if(Mathf.Abs(filteredZ) > threshold)
        {
            platformForce.z = Mathf.Clamp((filteredZ * speed), 10.0f, 20.0f);
            platform.transform.position = platformForce;
        }
    }

    // FOR DEBUGGING ONLY
    void OnGUI()
    {
        GUI.skin.label.fontSize = Screen.width / 20;

        GUILayout.Label("platformForce: " + platformForce);
        GUILayout.Label("platform position: " + platform.transform.position);
    }

}