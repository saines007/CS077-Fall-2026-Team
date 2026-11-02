using UnityEngine;

public class RotateObject : MonoBehaviour
{
    [SerializeField] private float degreesPersecond = 90f;

    public float DegreesPerSecond
    {
        get { return degreesPersecond; }
        set { degreesPersecond = value; }
    }

    private void Update()
    {
        transform.Rotate(Vector3.up * DegreesPerSecond * Time.deltaTime, Space.Self);
    }
}