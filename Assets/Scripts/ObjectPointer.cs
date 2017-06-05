using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ObjectPointer : MonoBehaviour {
    public GameObject player;

    Vector3 screenPos;
    Vector2 direction;
    float max;
    Image image;

    void Start()
    {
        image = this.GetComponent<Image>();

    }

    void Update()
    {
        screenPos = Camera.main.WorldToViewportPoint(player.transform.position); //get viewport positions

        if (screenPos.x >= 0 && screenPos.x <= 1 && screenPos.y >= 0 && screenPos.y <= 1)
        {
            //Debug.Log("already on screen, don't bother with the rest!");
            image.enabled = false;
            //Debug.Log(screenPos);
            return;
        } else
        {
            image.enabled = true;

            if (screenPos.z < 0)
            {
                screenPos *= -1;
            }

            Vector3 screenCenter = new Vector3(Screen.width, Screen.height, 0) / 2;

            //direction = new Vector3(
            //    Mathf.Clamp(screenPos.x + screenCenter.x, 0f, 1f),
            //    Mathf.Clamp(screenPos.y + screenCenter.y, 0f, 1f),
            //    0);

            //transform.position = direction;

            float angle = Mathf.Atan2(screenPos.y, screenPos.x);
            angle -= 90 * Mathf.Deg2Rad;

            float cos = Mathf.Cos(angle);
            float sin = -Mathf.Sin(angle);

            screenPos = screenCenter + new Vector3(sin * 150, cos * 150, 0);

            float m = cos / sin;

            Vector3 screenBounds = screenCenter * 0.9f;

            if (cos > 0)
            {
                screenPos = new Vector3(screenBounds.y / m, screenBounds.y, 0);
            } else
            {
                screenPos = new Vector3(-screenBounds.y / m, -screenBounds.y, 0);
            }

            if(screenPos.x > screenBounds.x)
            {
                screenPos = new Vector3(screenBounds.x, screenBounds.x * m, 0);
            } else
            {
                screenPos = new Vector3(-screenBounds.x, -screenBounds.x / m, 0);
            }

            screenPos += screenCenter;

            transform.localPosition = screenPos;
            transform.localRotation = Quaternion.Euler(0, 0, angle * Mathf.Rad2Deg + 180);



            direction = new Vector2(screenPos.x - 0.5f, screenPos.y - 0.5f) * 2; //2D version, new mapping
            max = Mathf.Max(Mathf.Abs(direction.x), Mathf.Abs(direction.y)); //get largest offset
            direction = (direction / (max * 2)) + new Vector2(0.5f, 0.5f); //undo mapping
            //Debug.Log(direction);
        }
    }
}
