using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FailColliderEvents : MonoBehaviour {

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Entered the Fail Zone.");
        Image button = GameObject.FindGameObjectWithTag("RestartButton").GetComponent<Image>();
        button.color = Color.green;
    }
}
