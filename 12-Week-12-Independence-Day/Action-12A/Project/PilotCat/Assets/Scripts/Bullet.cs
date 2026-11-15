using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 16f;

    void Update()
    {
        transform.Translate(Vector3.right * speed * Time.deltaTime, Space.World);

        // Remove it once it's past the right edge of the screen
        if (transform.position.x > 11f) Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Hazard"))
        {
            Destroy(other.gameObject);
            Destroy(gameObject);
        }
    }
}