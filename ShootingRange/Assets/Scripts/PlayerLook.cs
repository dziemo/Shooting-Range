using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerLook : MonoBehaviour
{
    public float sensitivity = 2f;
    public Vector2 maxAngle;
    float xRot = 0;

    Camera cam;
    Vector2 rotVal;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        cam = Camera.main;
    }

    void OnLook (InputValue value)
    {
        rotVal = value.Get<Vector2>();
    }

    private void Update()
    {
        transform.Rotate(Vector3.up, rotVal.x * sensitivity * Time.deltaTime);

        xRot -= rotVal.y * sensitivity * Time.deltaTime;
        xRot = Mathf.Clamp(xRot, maxAngle.x, maxAngle.y);

        cam.transform.localRotation = Quaternion.Euler(xRot, 0, 0);
    }
}
