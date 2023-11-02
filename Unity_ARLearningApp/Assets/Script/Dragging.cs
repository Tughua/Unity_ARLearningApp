using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ARObjectController : MonoBehaviour
{
    private Vector2 touchStartPos;
    private Vector2 touchCurrentPos;
    private bool isDragging = false;

    void Update()
    {
        if (Input.touchCount == 1)
        {
            Touch touch = Input.GetTouch(0);

            if (!isDragging)
            {
                if (touch.phase == TouchPhase.Began)
                {
                    Ray ray = Camera.main.ScreenPointToRay(touch.position);
                    RaycastHit hit;

                    // Cast a ray from the touch position to detect the object
                    if (Physics.Raycast(ray, out hit) && hit.transform == transform)
                    {
                        touchStartPos = touch.position;
                        isDragging = true;
                    }
                }
            }
            else
            {
                if (touch.phase == TouchPhase.Moved)
                {
                    touchCurrentPos = touch.position;

                    // Calculate the difference in touch position
                    Vector2 delta = touchCurrentPos - touchStartPos;

                    // Adjust the object's position based on the touch input
                    Vector3 moveDirection = new Vector3(delta.x, delta.y, 0);
                    transform.position += Camera.main.transform.TransformDirection(moveDirection) * 0.001f; // Adjust sensitivity as needed

                    touchStartPos = touchCurrentPos;
                }
                else if (touch.phase == TouchPhase.Ended)
                {
                    isDragging = false;
                }
            }
        }
    }
}