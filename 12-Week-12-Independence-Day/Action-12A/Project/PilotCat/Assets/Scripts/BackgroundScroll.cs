using UnityEngine;

public class BackgroundScroll : MonoBehaviour
{
    public float speed = 1.5f;
    public float leftEdge = -30f;
    public float rightEdge = 30f;

    void Update()
    {
        transform.Translate(Vector3.left * speed * Time.deltaTime, Space.World);

        // Once it's off the left side, jump back to the right side
        if (transform.position.x < leftEdge)
        {
            Vector3 pos = transform.position;
            pos.x = rightEdge;
            transform.position = pos;
        }
    }
}