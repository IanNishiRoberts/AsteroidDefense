using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOverSceneController : MonoBehaviour
{
    public GameObject lastScoreText;
    public GameObject highScoreText;
    public GameObject highMultiplierText;

    // Start is called before the first frame update
    void Start()
    {
        Text t = lastScoreText.GetComponent<Text>();
        Text m = highMultiplierText.GetComponent<Text>();
        Text h = highScoreText.GetComponent<Text>();

        t.text = "Last Score : " + PlayerStatsController.LastScore.ToString();
        m.text = "Highest Multiplier : " + PlayerStatsController.HigestMultiplier.ToString();
        h.text = "High Score : " + PlayerStatsController.HighScore.ToString();
    }

    void Update()
    {
        if (Input.GetButton("Fire1")) {
            SceneManager.LoadScene("GameScene");
        }
    }
}
