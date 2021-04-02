using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public GameObject snakeHead;

    public bool FPCamera;
    public bool newEvento;

    private void Start()
    {
        newEvento = true;
        FPCamera = false;
    }

    void Update()
    {
        if (newEvento)
        {
            FPCamera = !FPCamera;
            if (FPCamera)
            {
                transform.SetParent(snakeHead.transform, false);
                transform.position = snakeHead.transform.position + new Vector3(0, -0.5f, 0);
                transform.rotation = snakeHead.transform.rotation;
                newEvento = false;
            }
            else
            {
                transform.SetParent(null);
                transform.position = new Vector3(0, 16, 0);
                Quaternion target = Quaternion.Euler(90, -90, 0);
                transform.rotation = Quaternion.Slerp(transform.rotation, target, 1);
                newEvento = false;
            }
        }
    }
}
