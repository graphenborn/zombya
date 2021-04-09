using UnityEngine;

public class cameraController : MonoBehaviour
{
    public Transform focus;
    public float mouseSensivity = 3.0f;

    public float maxThteta = 45f;
    public float minThteta = 0f;

    public float maxDist = 4f;
    public float minDist = 1f;

    //Залочить в приват потом
    public float dist = 2f;
    public Vector2 sphericalPosition = new Vector2(0f, 90f);

    private void Update()
    {
        if (RotateCamera())
        {
            float dAlpha = mouseSensivity * Input.GetAxis("Mouse X");
            float dTheta = mouseSensivity * Input.GetAxis("Mouse Y");

            sphericalPosition += new Vector2(dAlpha, dTheta);
            if (Mathf.Abs(sphericalPosition.x) >= 360f)
                sphericalPosition.x += Mathf.Sign(sphericalPosition.x) * -360f;

            sphericalPosition.y = Mathf.Clamp(sphericalPosition.y, minThteta, maxThteta);
        }

        dist -= Input.GetAxis("Mouse ScrollWheel");
        dist = Mathf.Clamp(dist, minDist, maxDist);

        MoveCameraToSphericalCord();
        transform.LookAt(focus);
    }

    private void MoveCameraToSphericalCord()
    {
        float alpha = sphericalPosition.x * Mathf.Deg2Rad;
        float theta = sphericalPosition.y * Mathf.Deg2Rad;

        float x = dist * Mathf.Sin(alpha) * Mathf.Sin(theta);
        float z = dist * Mathf.Cos(alpha) * Mathf.Sin(theta);
        float y = dist * Mathf.Cos(theta);

        transform.position = focus.position + new Vector3(x, y, z);
    }

    private bool RotateCamera()
    {
        if (Input.GetMouseButton(1) && Input.GetButton("Camera Movement"))
            return true;
        return false;
    }

}
