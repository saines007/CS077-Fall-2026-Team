using UnityEngine;

public class SkyPan : MonoBehaviour
{
    public float degreesPerSecond = 2f;

    void Start()
    {
        // Work on a copy so the material file itself isn't changed
        RenderSettings.skybox = new Material(RenderSettings.skybox);
    }

    void Update()
    {
        float angle = (Time.time * degreesPerSecond) % 360f;
        RenderSettings.skybox.SetFloat("_Rotation", angle);
    }
}