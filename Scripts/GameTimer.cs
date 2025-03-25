using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameTimer : MonoBehaviour
{
    public Text timerText;
    public GameObject gameOverPage;
    private float timer = 120f;

    void OnEnable()
    {
        if (gameObject.activeInHierarchy)
        {
            StartCoroutine(StartGameTimer());
        }
    }

    IEnumerator StartGameTimer()
    {
        while (timer > 0)
        {
            timer -= Time.deltaTime;
            UpdateTimerDisplay(timer);
            yield return null;
        }

        GameOver();
    }

    public void UpdateTimerDisplay(float time)
    {
        int minutes = Mathf.FloorToInt(time / 60);
        int seconds = Mathf.FloorToInt(time % 60);
        timerText.text = minutes.ToString("00") + ":" + seconds.ToString("00");
    }

    public void RestartTimer()
    {
        StopAllCoroutines();
        timer = 120f;
        UpdateTimerDisplay(timer);

        if (gameObject.activeInHierarchy)
        {
            StartCoroutine(StartGameTimer());
        }
    }

   public void GameOver()
    {
        gameOverPage.SetActive(true);
        Time.timeScale = 0f;
    }

    public void RestartGame()
    {
        Time.timeScale = 1f; // Resume game
        gameOverPage.SetActive(false); // Hide game over page
        RestartTimer();
    }
}
