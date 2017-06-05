using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class AccelerationText : MonoBehaviour
{
    public Accelerometer acc;
    public float updateInterval = 0.1f;

    private Text text;

    void Start()
    {
        text = GetComponent<Text>();
        StartCoroutine(UpdateText());
    }

    IEnumerator UpdateText()
    {
        while (true)
        {
            text.text = string.Format("Acceleration.x = {0:0.00}\nAcceleration.y = {1:0.00}\nAcceleration.z = {2:0.00}",
                acc.FilteredX,
                acc.FilteredY,
                acc.FilteredZ);
                //Input.acceleration.x,
                //Input.acceleration.y,
                //Input.acceleration.z
                //);
            yield return new WaitForSeconds(updateInterval);
        }
    }
}