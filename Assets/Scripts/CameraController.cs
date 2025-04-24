using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] Transform followTarget;
    [SerializeField] private float rotationSpeed = 2f; 
    [SerializeField] float distance = 7;
    [SerializeField] float minVerticalAngle = 5;
    [SerializeField] float maxVerticalAngle = 60;
    [SerializeField] Vector2 framingOffset;
    [SerializeField] bool invertX;
    [SerializeField] bool invertY;

    float rotationX;
    float rotationY;

    float invertXVal;
    float invertYVal;


    private void Start()
    {

        // to hide the mouse
        Cursor.visible = false;

        // lock the cursor
        Cursor.lockState = CursorLockMode.Locked;

    }

    private void Update()
    {
        invertXVal = (invertX) ? -1 : 1;
        invertYVal = (invertY) ? -1 : 1;

        rotationX += Input.GetAxis("Mouse Y") * invertYVal * rotationSpeed;
        rotationX = Mathf.Clamp(rotationX, minVerticalAngle, maxVerticalAngle);
        rotationY += Input.GetAxis("Mouse X") * invertXVal * rotationSpeed;

        var targetRotation = Quaternion.Euler(rotationX, rotationY, 0);

        // Corrected camera position calculation
        Vector3 targetPosition = followTarget.position + targetRotation * new Vector3(0, 0, -distance);

        // Apply framing offset
        targetPosition += new Vector3(framingOffset.x, framingOffset.y, 0);

        // Set camera position and rotation
        transform.position = targetPosition;
        transform.rotation = targetRotation;
    }

    public void set_rotationSpeed(float value)
    {
        rotationSpeed = value;
    }

    public Quaternion PlanerRotation => Quaternion.Euler(0, rotationY, 0);

}
