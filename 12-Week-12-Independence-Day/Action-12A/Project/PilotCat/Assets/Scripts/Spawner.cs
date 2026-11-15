using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject prefab;
    public float interval = 1.5f;
    public float spawnX = 12f;
    public float minY = -4f;
    public float maxY = 4f;

    float timer;

    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= interval)
        {
            timer = 0f;
            Vector3 pos = new Vector3(spawnX, Random.Range(minY, maxY), 0f);
            Instantiate(prefab, pos, Quaternion.identity);
        }
    }
}