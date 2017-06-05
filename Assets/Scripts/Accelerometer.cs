using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Accelerometer : MonoBehaviour
{
#region Declarations
    public GameObject platform;
    public GameObject thrownObject;
    public Slider forceSlider;
    public float threshold = 0.15f;

    public float thrust = 225.0f;
    public float smoothingValue = 0.2f;

    private Rigidbody rb;
    private LowPassFilter filterX;
    private LowPassFilter filterY;
    private LowPassFilter filterZ;

    private float filteredX;
    private float filteredY;
    private float filteredZ;

    public Vector3 flyVector { get; private set; }

    public float FilteredX
    {
        get
        {
            return filteredX;
        }
    }

    public float FilteredY
    {
        get
        {
            return filteredY;
        }
    }

    public float FilteredZ
    {
        get
        {
            return filteredZ;
        }
    }

    #endregion

    void Awake()
    {
        // create filters to accelerometer's x-axis and y-axis readings
        filterX = new LowPassFilter(smoothingValue);
        filterY = new LowPassFilter(smoothingValue);
        filterZ = new LowPassFilter(smoothingValue);

        rb = thrownObject.GetComponent<Rigidbody>();

        flyVector = Vector3.zero;
    }

    void Update()
    {
        SetThrust();

        // calculate new filtered values
        filteredX = filterX.NextStep(Time.deltaTime, Input.acceleration.x);
        filteredY = filterY.NextStep(Time.deltaTime, Input.acceleration.y);
        filteredZ = filterZ.NextStep(Time.deltaTime, Input.acceleration.z);

        // If this does not work, set filteredX, etc. to Input.acceleration.x...
        flyVector = new Vector3(filteredX, Mathf.Abs(filteredY), -filteredZ);
        //flyVector = new Vector3(Input.acceleration.x, Mathf.Abs(Input.acceleration.y), Input.acceleration.z);

        //if (Mathf.Abs(Input.acceleration.z) < 0.9f) // disable rotation if gravitation acceleration lays on -z axis
        //{
        //    RotatePlatform();
        //}

        if (Mathf.Abs(filteredY + 1) > threshold)
        {
            PlatformForce(flyVector, thrust);
        }

    }

    private void RotatePlatform()
    {
            // Calculations for rotating the platform object
            float clampedTiltX = Mathf.Clamp(-filteredX * Mathf.Rad2Deg, -45f, 45f); // clamp X axis
            //float clampedTiltY = Mathf.Clamp(-filteredY * Mathf.Rad2Deg, -45f, 45f); // clamp Y axis
            float clampedTiltZ = Mathf.Clamp(-filteredZ * Mathf.Rad2Deg, -45f, 45f); // clamp Z axis

            platform.transform.rotation = Quaternion.Euler(clampedTiltZ, 0f, clampedTiltX); // rotate platform on X and Z axes.
    }

    private void PlatformForce(Vector3 dir, float thrust)
    {
        // Clamp filteredValues to control flight path
        // code

        rb.AddForce(dir * thrust);
    }

    public void SetThrust()
    {
        thrust = forceSlider.value;
    }
}