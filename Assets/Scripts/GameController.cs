using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public static GameController instance;
    public int currentScore = 0;
    public int currentMultiplier = 0;
    public float remainingTime = 120.0f;

    public GameObject playerFactory;
    public GameObject asteroidFactory;
    public GameObject enemyFactory;
    public GameObject blackHoleFactory;
    public GameObject scoreText;
    public GameObject multiplierText;
    public GameObject timeText;
    public GameObject spawnText;
    public GameObject uiCanvas;

    private int count = 4;
    private GameObject spawnCountdown;

    void Awake() {
        if (instance == null) {
            instance = this;
        } else if (instance != null) {
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        spawnCountdown = Instantiate(spawnText, spawnText.transform.position, Quaternion.identity);
        spawnCountdown.transform.SetParent(uiCanvas.transform, false);

        Invoke("CountDown", 0.0f);
    }

    void Update() {

        if (count <= 0)  {
            remainingTime -= Time.deltaTime;
            Text t = timeText.GetComponent<Text>();

            if (remainingTime <=0) {
                // Gameover
                //Time.timeScale = 0.0f;
                SceneManager.LoadScene("GameOverScene");
            } else {
                t.text = "Time : " + System.Math.Round(remainingTime, 0).ToString() + " seconds";

                if (playerFactory.GetComponent<PlayerFactoryController>().player == null) {
                    Invoke("SpawnNewPlayerShip", 1.5f);
                }
            }
        }
    }

    void SpawnNewPlayerShip() {
        playerFactory.GetComponent<PlayerFactoryController>().SpawnPlayerShip(true);
    }

    public void AnnounceRespawn() {
        GameObject g = Instantiate(spawnText, spawnText.transform.position, Quaternion.identity);
        g.transform.SetParent(uiCanvas.transform, false);
        g.GetComponent<Text>().text = "respawn";
    }

    void CountDown() {
        count--;
        if (count == 0) {
            // Turn off the "Begin" text
            Destroy(spawnCountdown);
            
            // Activate all our objects
            playerFactory.GetComponent<PlayerFactoryController>().SpawnPlayerShip(false);
            asteroidFactory.GetComponent<AsteroidFactoryController>().Activate();
            enemyFactory.GetComponent<EnemyFactoryController>().Activate();
            blackHoleFactory.GetComponent<BlackHoleFactoryController>().Activate();
        } else {
            // Show the next frame
            Text t = spawnCountdown.GetComponent<Text>();
            t.text = "BEGIN IN " + count.ToString();
            Invoke("CountDown", 1.0f);
        }
    }

    public void AddToScore(string objTag) {

        int thisScore = 0;
        string postFix = "points";

        switch (objTag) {
            case "Player":
                currentMultiplier = 0;
                thisScore = 0;
                break;
            case "Satellite":
                currentMultiplier = 0;
                thisScore = 0;
                break;
            case "Enemy":
                currentMultiplier++;
                thisScore = 20;
                break;
            case "Asteroid":
                currentMultiplier++;
                thisScore = 10;
                break;
            case "Star":
                currentMultiplier++;
                thisScore = 50;
                postFix = "bonus points";
                break;
            default:
                break;
        }

        if (currentMultiplier < 0)
            currentMultiplier = 0;

        thisScore = thisScore * currentMultiplier;
        currentScore += thisScore;

        if (currentScore < 0)
            currentScore = 0;

        Text t = scoreText.GetComponent<Text>();
        Text m = multiplierText.GetComponent<Text>();

        t.text = "Score : " + currentScore.ToString();
        m.text = "Multiplier : " + currentMultiplier.ToString();

        GameObject g = Instantiate(spawnText, spawnText.transform.position, Quaternion.identity);
        g.transform.SetParent(uiCanvas.transform, false);
        if (thisScore > 0) {
            g.GetComponent<Text>().text = "+" + thisScore.ToString() + " " + postFix;
        } else {
            g.GetComponent<Text>().text = "multiplier down";
        }

        PlayerStatsController.LastScore = currentScore;
        PlayerStatsController.HighScore = currentScore;
        PlayerStatsController.HigestMultiplier = currentMultiplier;
    }
}
