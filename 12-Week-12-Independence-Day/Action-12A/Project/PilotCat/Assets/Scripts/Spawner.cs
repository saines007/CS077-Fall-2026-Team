using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject prefab;
    public float interval = 1.5f;
    public float spawnX = 12f;
    public float minY = -4f;
    public float maxY = 4f;

    [Header("Aim at the player (optional)")]
    public Transform target;
    public float spread = 0.5f;

    [Header("Difficulty (hazards only)")]
    public bool scaleWithScore = false;
    public float hazardStartInterval = 1f;
    public float hazardFastestInterval = 0.4f;
    public float hazardIntervalDrop = 0.15f;
    public float speedBoostPerLevel = 1.5f;

    float timer;

    void Update()
    {
        float currentInterval = interval;
        int level = 0;

        if (scaleWithScore)
        {
            // Level goes up by 1 every 10 Cat Nip
            level = ScoreKeeper.catNip / 10;
            currentInterval = Mathf.Max(hazardFastestInterval,
                                        hazardStartInterval - level * hazardIntervalDrop);
        }

        timer += Time.deltaTime;
        if (timer >= currentInterval)
        {
            timer = 0f;

            float y;
            if (target != null)
            {
                y = target.position.y + Random.Range(-spread, spread);
                y = Mathf.Clamp(y, minY, maxY);
            }
            else
            {
                y = Random.Range(minY, maxY);
            }

            GameObject spawned = Instantiate(prefab, new Vector3(spawnX, y, 0f), Quaternion.identity);

            // Faster hazards at higher levels
            ScrollLeft mover = spawned.GetComponent<ScrollLeft>();
            if (mover != null)
            {
                mover.speed += level * speedBoostPerLevel;
            }
        }
    }
}