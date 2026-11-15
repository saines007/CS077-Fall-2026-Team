using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMover : MonoBehaviour
{
    [Header("Flight Parameters")]
    public float verticalSpeed = 8.0f;
    public float minY = -4f;
    public float maxY = 4f;

    [Header("Shooting")]
    public GameObject bulletPrefab;

    [Header("Audio Settings")]
    public AudioSource pickupAudio;

    void Update()
    {
        // W/S or Up/Down arrows
        float vInput = Input.GetAxis("Vertical");
        transform.Translate(Vector3.up * vInput * verticalSpeed * Time.deltaTime, Space.World);

        // Keep the plane on screen
        Vector3 pos = transform.position;
        pos.y = Mathf.Clamp(pos.y, minY, maxY);
        transform.position = pos;

        // Spacebar shoots
        if (Input.GetKeyDown(KeyCode.Space) && bulletPrefab != null)
        {
            Vector3 spawnPos = transform.position + Vector3.right * 1.5f;
            Instantiate(bulletPrefab, spawnPos, Quaternion.identity);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("CatNip"))
        {
            if (pickupAudio != null) pickupAudio.Play();
            ScoreKeeper.catNip++;
            Destroy(other.gameObject);
        }
        else if (other.CompareTag("Hazard"))
        {
            // Crash: restart the level
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}