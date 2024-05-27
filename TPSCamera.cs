using UnityEngine;

public class TPSCamera : MonoBehaviour
{
    public Transform orientation;
    public Transform player;
    public Transform cam;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        // Rotate Orientation
        Vector3 viewDirection = player.position - new Vector3(cam.transform.position.x, player.position.y, cam.transform.position.z);
        orientation.forward = viewDirection.normalized;

        // Fix orientation rotation x is always 0
        Vector3 orientationEuler = orientation.eulerAngles;
        orientationEuler.x = 0;
        orientation.eulerAngles = orientationEuler;
    }
}
