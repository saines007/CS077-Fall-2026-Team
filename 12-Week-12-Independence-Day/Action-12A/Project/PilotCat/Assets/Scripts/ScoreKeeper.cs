using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class ScoreKeeper : MonoBehaviour
{
    public static ScoreKeeper instance;
    public static int catNip;
    public static int kills;
    public TMP_Text scoreText;

    [Header("Winning")]
    public int goal = 50;
    public GameObject beaconPrefab;
    public GameObject winMessage;

    [Header("End Sounds")]
    public AudioClip loseSound;
    [Range(0f, 1f)] public float loseVolume = 0.8f;
    public AudioSource music;

    bool beaconSent;

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        catNip = 0;
        kills = 0;
        Time.timeScale = 1f;
        if (winMessage != null) winMessage.SetActive(false);
    }

    void Update()
    {
        if (scoreText != null)
        {
            scoreText.text = "Cat Nip: " + catNip + " / " + goal + "   Kills: " + kills;
        }

        // Enough Cat Nip collected: send in the extraction beacon
        if (!beaconSent && catNip >= goal && beaconPrefab != null)
        {
            beaconSent = true;
            Instantiate(beaconPrefab, new Vector3(12f, 0f, 0f), Quaternion.identity);
        }

        // After the game ends, R restarts
        if (Time.timeScale == 0f && Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

    public void Win()
    {
        ShowEnd("Delivery Complete!");
    }

    public void Lose()
    {
        // Only once, even if two hazards hit at the same moment
        if (Time.timeScale == 0f) return;

        if (music != null) music.Stop();

        if (loseSound != null)
        {
            AudioSource.PlayClipAtPoint(loseSound, Camera.main.transform.position, loseVolume);
        }

        ShowEnd("Shot Down!");
    }

    void ShowEnd(string title)
    {
        int finalScore = catNip * kills;

        if (winMessage != null)
        {
            TMP_Text endText = winMessage.GetComponent<TMP_Text>();
            if (endText != null)
            {
                endText.text = title + "\n"
                    + "Cat Nip " + catNip + " x Kills " + kills + "\n"
                    + "Score: " + finalScore + "\n"
                    + "Press R to play again";
            }
            winMessage.SetActive(true);
        }

        Time.timeScale = 0f;
    }
}