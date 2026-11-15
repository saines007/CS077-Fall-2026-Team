using UnityEngine;
using TMPro;

public class ScoreKeeper : MonoBehaviour
{
    public static int catNip;
    public TMP_Text scoreText;

    void Start()
    {
        catNip = 0;
    }

    void Update()
    {
        if (scoreText != null)
        {
            scoreText.text = "Cat Nip: " + catNip;
        }
    }
}