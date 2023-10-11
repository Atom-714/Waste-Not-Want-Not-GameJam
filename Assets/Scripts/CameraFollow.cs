using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraFollow : MonoBehaviour
{
    public Rect screenRect;
    public Transform playerTransform;

    private void Update()
    {
        screenRect = new Rect(0f, 0f, Screen.width, Screen.height);
        if (screenRect.Contains(Input.mousePosition))
        {
            Vector3 relativeMousePosition = playerTransform.position + (Input.mousePosition - new Vector3(Screen.width * 0.5f, Screen.height * 0.5f));
            transform.position = Vector2.Lerp(playerTransform.position, relativeMousePosition, 0.002f);
        }
    }
}
