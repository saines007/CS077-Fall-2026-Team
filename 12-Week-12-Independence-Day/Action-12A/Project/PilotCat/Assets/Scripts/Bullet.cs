using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 16f;
    public AudioClip explodeSound;
    [Range(0f, 1f)] public float explodeVolume = 0.4f;

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
            ScoreKeeper.kills++;

            // Play the boom at the camera so it's always full volume
            if (explodeSound != null)
            {
                AudioSource.PlayClipAtPoint(explodeSound, Camera.main.transform.position, explodeVolume);
            }

            Destroy(other.gameObject);
            Destroy(gameObject);
        }
    }
}