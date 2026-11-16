using UnityEngine;

public class PlaneTilt : MonoBehaviour
{
    public float maxPitch = 15f;     // degrees of nose up/down
    public float smoothing = 6f;     // higher = snappier

    Quaternion baseLocalRotation;
    float currentPitch;

    void Start()
    {
        baseLocalRotation = transform.localRotation;
    }

    void LateUpdate()
    {
        if (Time.timeScale == 0f) return;

        // W/S decide the target tilt, then ease toward it
        float target = Input.GetAxis("Vertical") * maxPitch;
        currentPitch = Mathf.Lerp(currentPitch, target, smoothing * Time.deltaTime);

        // Tilt around the screen's depth axis so the nose points up or down on screen
        transform.rotation = Quaternion.AngleAxis(currentPitch, Vector3.forward)
                             * transform.parent.rotation * baseLocalRotation;
    }
}