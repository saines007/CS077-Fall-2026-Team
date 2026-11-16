using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMover : MonoBehaviour
{
    [Header("Flight Parameters")]
    public float verticalSpeed = 8.0f;
    public float minY = -4f;
    public float maxY = 4f;
    public float horizontalSpeed = 8.0f;
    public float minX = -9f;
    public float maxX = 6f;

    [Header("Shooting")]
    public GameObject bulletPrefab;

    [Header("Audio Settings")]
    public AudioSource pickupAudio;
    public AudioClip shootSound;
    [Range(0f, 1f)] public float shootVolume = 0.3f;
    public AudioClip pickupSound;
    [Range(0f, 3f)] public float pickupVolume = 2f;

    void Update()
    {
        // Game is frozen after winning
        if (Time.timeScale == 0f) return;

        // W/S or Up/Down = vertical, A/D or Left/Right = horizontal
        float vInput = Input.GetAxis("Vertical");
        float hInput = Input.GetAxis("Horizontal");
        Vector3 move = new Vector3(hInput * horizontalSpeed, vInput * verticalSpeed, 0f);
        transform.Translate(move * Time.deltaTime, Space.World);

        // Keep the plane on screen
        Vector3 pos = transform.position;
        pos.x = Mathf.Clamp(pos.x, minX, maxX);
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
            if (pickupAudio != null && pickupSound != null)
            {
                // Stack copies to go louder than 1
                float v = pickupVolume;
                while (v > 0f)
                {
                    pickupAudio.PlayOneShot(pickupSound, Mathf.Min(v, 1f));
                    v -= 1f;
                }
            }
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