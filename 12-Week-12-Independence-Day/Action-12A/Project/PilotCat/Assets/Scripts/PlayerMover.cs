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
    public AudioClip shootSound;
    [Range(0f, 1f)] public float shootVolume = 0.3f;

    void Update()
    {
        // Game is frozen after winning
        if (Time.timeScale == 0f) return;

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
            if (pickupAudio != null && shootSound != null) pickupAudio.PlayOneShot(shootSound, shootVolume);
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
            if (ScoreKeeper.instance != null) ScoreKeeper.instance.Lose();
        }
        else if (other.CompareTag("Beacon"))
        {
            // Reached the extraction beacon: win
            if (ScoreKeeper.instance != null) ScoreKeeper.instance.Win();
        }
    }
}