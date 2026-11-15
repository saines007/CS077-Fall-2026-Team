using UnityEngine;

public class ScrollLeft : MonoBehaviour
{
    public float speed = 6f;

    void Update()
    {
        transform.Translate(Vector3.left * speed * Time.deltaTime, Space.World);

        // Remove it once it's past the left edge of the screen
        if (transform.position.x < -14f) Destroy(gameObject);
    }
}