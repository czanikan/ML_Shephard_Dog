using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManagement : MonoBehaviour
{
    public TextMeshProUGUI timeLeftText;
    public TextMeshProUGUI pointsText;

    public GameObject gameOverGUI;
    public GameObject winnerGUI;
    public GameObject pausedGUI;
    public GameObject nextLevelGUI;
    public GameObject dog;

    public int nextLvlIndex;

    int timeLeft = 240;
    int maxPoints = 0; //ez most itt ilyen ideiglenes valtozo
    GameObject[] sheepCount;
    private SheepGettingIn sheepGetInScript;

    public bool lvlWon = false;
    public bool gameOver = false;
    // Start is called before the first frame update
    void Start()
    {
        lvlWon = false;
        gameOver = false;
        sheepGetInScript = GameObject.FindGameObjectWithTag("fence").GetComponent<SheepGettingIn>();
        //timeLeftText.SetText("Time: " + timeLeft);
        //pointsText.SetText("Points: " + sheepGetInScript.points + "/" + maxPoints);

        if (nextLvlIndex == 1)
        {
            timeLeft = 120;
        }
        else if (nextLvlIndex == 2)
        {
            timeLeft = 180;
        }
        else if (nextLvlIndex == 3)
        {
            timeLeft = 240;
        }

        sheepCount = GameObject.FindGameObjectsWithTag("Sheep");
        maxPoints = sheepCount.Length;

        StartCoroutine(Countdown());
    }
    IEnumerator Countdown()
    {
        while (timeLeft > 0 && !lvlWon)
        {
            yield return new WaitForSeconds(1);
            timeLeft--;
            timeLeftText.SetText("Time: " + timeLeft);
            Debug.Log(timeLeft + "seconds left!");
        }
    }
    // Update is called once per frame
    void Update()
    {
        pointsText.SetText("Points: " + sheepGetInScript.points + "/" + maxPoints);
        if (timeLeft <= 0 && sheepGetInScript.points < maxPoints)
        {
            GameOver();
            Debug.Log("Game Over!");
        }
        else if (sheepGetInScript.points == maxPoints && nextLvlIndex < 3)
        {
            dog.GetComponent<PlayerController>().enabled = false;
            lvlWon = true;
            nextLevelGUI.SetActive(true);
            Debug.Log("Next level");
        }
        else if(sheepGetInScript.points == maxPoints && nextLvlIndex == 3)
        {
            dog.GetComponent<PlayerController>().enabled = false;
            lvlWon = true;
            Winner();
            Debug.Log("Winner");
        }
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            GamePaused();
        }
    }

    public void GameOver()
    {
        gameOver = true;
        dog.GetComponent<PlayerController>().enabled = false;
        gameOverGUI.SetActive(true);
    }

    public void Winner()
    {
        Time.timeScale = 0;
        gameOver = true;
        dog.GetComponent<PlayerController>().enabled = false;
        winnerGUI.SetActive(true);
        
    }

    public void GamePaused()
    {
        Time.timeScale = 0;
        gameOver = true;
        dog.GetComponent<PlayerController>().enabled = false;
        pausedGUI.SetActive(true);
    }

    public void GameContinue()
    {
        Time.timeScale = 1;
        gameOver = false;
        dog.GetComponent<PlayerController>().enabled = true;
        pausedGUI.SetActive(false);
    }

    public void Quit()
    {
        Application.Quit();
    }
    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void NextLevel()
    {
        StartCoroutine(StartNewLevel());
    }

    IEnumerator StartNewLevel()
    {
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene(nextLvlIndex);
    }

}
