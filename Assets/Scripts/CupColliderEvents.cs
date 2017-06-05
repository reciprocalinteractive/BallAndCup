using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CupColliderEvents : MonoBehaviour {

    private LineRenderer line;
    public int cupCount { get; private set; }

    private GameController gameController;

    private void Awake()
    {
        gameController = GameObject.FindWithTag("GameController").GetComponent<GameController>();
        //line = GameObject.FindGameObjectWithTag("Player").GetComponent<LineRenderer>();
        //line.enabled = false;
        cupCount = 0;
    }

    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log("Entered the Cup Collider.");
        //line.enabled = true;
        cupCount++;
        //Debug.Log("Cup Count: " + cupCount);

        if (cupCount > 1)
        {
            gameController.SetScore();
        }
    }

    private void OnTriggerStay(Collider other)
    {
        //Debug.Log("Ball is in the cup.");
    }

    private void OnTriggerExit(Collider other)
    {
        Debug.Log("Ball has left the cup.");
        //line.enabled = false;
    }
}
