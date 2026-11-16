using UnityEngine;

public class FaceCamera : MonoBehaviour
{
    Transform cam;
    Quaternion baseRotation;

    void Start()
    {
        cam = Camera.main.transform;

        // Remember how the plane looks right now, relative to the camera's line of sight
        Vector3 ray = transform.position - cam.position;
        baseRotation = Quaternion.Inverse(Quaternion.LookRotation(ray)) * transform.rotation;
    }

    void LateUpdate()
    {
        // Turn with the line of sight so the plane looks the same anywhere on screen
        Vector3 ray = transform.position - cam.position;
        transform.rotation = Quaternion.LookRotation(ray) * baseRotation;
    }
}