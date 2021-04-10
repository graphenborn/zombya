using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticAngleFollowingCamera : MonoBehaviour
{
    public Transform focus;

    public float azimuthAngle = 20f;
    public float planeAngle = 180f;

    public float maxDist = 4f;
    public float minDist = 1f;

    private float dist = 1f;

    private void Update()
    {
        ZoomCamera();

        MoveCameraToSphericalCord();
        transform.LookAt(focus);
    }

    private void ZoomCamera()
    {
        dist -= Input.GetAxis("Mouse ScrollWheel");
        dist = Mathf.Clamp(dist, minDist, maxDist);
    }

    private void MoveCameraToSphericalCord()
    {
        float alpha = planeAngle * Mathf.Deg2Rad;
        float theta = azimuthAngle * Mathf.Deg2Rad;

        float x = dist * Mathf.Sin(alpha) * Mathf.Sin(theta);
        float z = dist * Mathf.Cos(alpha) * Mathf.Sin(theta);
        float y = dist * Mathf.Cos(theta);

        transform.position = focus.position + new Vector3(x, y, z);
    }

}
