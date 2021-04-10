using UnityEngine;

public class DynamicPlaneAngleCamera : MonoBehaviour
{
    public Transform focus;
    public float mouseSensivity = 3.0f;

    public float maxThteta = 45f;
    public float minThteta = 0f;

    public float maxDist = 4f;
    public float minDist = 1f;

    private float dist = 1f;

    // Перевести в радианы потом
    private Vector2 sphericalPosition = new Vector2(180f, 50f);

    private bool freeMovement = false;

    private void Update()
    {
        freeMovement = getFreeMovement();

        if (freeMovement)
        {
            ChangeCameraSphericalCords();
        }
        else
        {
            sphericalPosition.x = focus.rotation.y * 2f * Mathf.Rad2Deg;
        }

        ZoomCamera();

        MoveCameraToSphericalCord();
        transform.LookAt(focus);
    }

    private void ChangeCameraSphericalCords ()
    {

        float dAlpha = mouseSensivity * Input.GetAxis("Mouse X");
        float dTheta = mouseSensivity * Input.GetAxis("Mouse Y");

        sphericalPosition += new Vector2(dAlpha, dTheta);
        if (Mathf.Abs(sphericalPosition.x) >= 360f)
            sphericalPosition.x += Mathf.Sign(sphericalPosition.x) * -360f;

        sphericalPosition.y = Mathf.Clamp(sphericalPosition.y, minThteta, maxThteta);
    }

    private void ZoomCamera ()
    {
        dist -= Input.GetAxis("Mouse ScrollWheel");
        dist = Mathf.Clamp(dist, minDist, maxDist);
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

    private bool getFreeMovement()
    {
        return Input.GetMouseButton(1) && Input.GetButton("Camera Movement");
    }

}
