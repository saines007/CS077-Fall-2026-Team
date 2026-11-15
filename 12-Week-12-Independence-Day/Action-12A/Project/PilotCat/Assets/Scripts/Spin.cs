using UnityEngine;

public class Spin : MonoBehaviour
{
    public Vector3 degreesPerSecond = new Vector3(0f, 0f, 1200f);

    void Update()
    {
        transform.Rotate(degreesPerSecond * Time.deltaTime, Space.Self);
    }
}