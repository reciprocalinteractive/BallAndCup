using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowPath : MonoBehaviour {
    public float mult = 0.5f;
    private int vertices = 100;

    private Accelerometer acc;
    private Vector3 velocity;
    private Vector3 trajectory;

	void Start () {
        acc = GameObject.FindGameObjectWithTag("Accelerometer").GetComponent<Accelerometer>();
	}
	
	void Update () {
        trajectory = this.transform.position;
        velocity = acc.flyVector * (acc.thrust * mult);

        DrawTrajectory(trajectory, velocity);
	}

    void DrawTrajectory(Vector3 traj, Vector3 vel)
    {
        var line = this.GetComponent<LineRenderer>();
        line.positionCount = vertices;

        for (int i = 0; i < line.positionCount; i++)
        {
            line.SetPosition(i, traj);
            vel += Physics.gravity * Time.fixedDeltaTime;
            traj += vel * Time.fixedDeltaTime;
        }

    }
}
