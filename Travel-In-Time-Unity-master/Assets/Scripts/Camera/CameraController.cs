using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform target;
    public Vector3 offset;
    public float pitch = 2f;
    public float zoomSpeed = 4f;
    public float minZoom = 5f;
    public float maxZoom = 15f;
    public float yawSpeed = 100f;
    //Controls max and min rotation of camera on the X axis
    public float minYyaw = -10f;
    public float maxYyaw = 60f;
    private float currentZoom = 10f;
    private float currentYawX = 0;
    private float currentYawY = 0;

    // Update is called once per frame
    void Update()
    {
        currentZoom -= Input.GetAxis("Mouse ScrollWheel") * zoomSpeed;
        currentZoom = Mathf.Clamp(currentZoom, minZoom, maxZoom);
        if (Input.GetMouseButton(1))
        {
            currentYawX += Input.GetAxis("Mouse X") * yawSpeed * Time.deltaTime;
            currentYawY -= Input.GetAxis("Mouse Y") * yawSpeed * Time.deltaTime;
            currentYawY = Mathf.Clamp(currentYawY, minYyaw, maxYyaw);
        }

    }

    private void LateUpdate()
    {
        transform.position = target.position - offset * currentZoom;
        transform.LookAt(target.position + Vector3.up * pitch);
        transform.RotateAround(target.position, Vector3.up, currentYawX);
        transform.RotateAround(target.position, transform.right, currentYawY);
    }
}