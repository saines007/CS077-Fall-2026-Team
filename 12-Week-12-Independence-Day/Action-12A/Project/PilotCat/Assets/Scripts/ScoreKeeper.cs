using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class ScoreKeeper : MonoBehaviour
{
    public static ScoreKeeper instance;
    public static int catNip;
    public TMP_Text scoreText;

    [Header("Winning")]
    public int goal = 10;
    public GameObject beaconPrefab;
    public GameObject winMessage;

    bool beaconSent;

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        catNip = 0;
        Time.timeScale = 1f;
        if (winMessage != null) winMessage.SetActive(false);
    }

    void Update()
    {
        if (scoreText != null)
        {
            scoreText.text = "Cat Nip: " + catNip + " / " + goal;
        }

        // Enough Cat Nip collected: send in the extraction beacon
        if (!beaconSent && catNip >= goal && beaconPrefab != null)
        {
            beaconSent = true;
            Instantiate(beaconPrefab, new Vector3(12f, 0f, 0f), Quaternion.identity);
        }

        // After winning, R restarts
        if (Time.timeScale == 0f && Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

    public void Win()
    {
        if (winMessage != null) winMessage.SetActive(true);
        Time.timeScale = 0f;
    }
}