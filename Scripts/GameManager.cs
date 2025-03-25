using UnityEngine;
using UnityEngine.UI;
using System.Collections;

[DefaultExecutionOrder(-1)]
public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [SerializeField] private Blade blade;
    [SerializeField] private Spawner spawner;
    [SerializeField] private Text scoreText, NewscoreText, MoreGameScoreText;
    [SerializeField] private Image fadeImage;
    [SerializeField]
    private GameObject gameOverScreen, PlayGame, option, levelpage,
        Newoption, NewgameOverScreen, MoreGameoverScreen, Moreoption;
    [SerializeField] private GameTimer gameTimer;
    [SerializeField] private MoreGameTimer MoreGameTimer;
    public GameObject NewGame;
    public GameObject MoreGame;

    private int newGameScore = 0;
    private int moreGameScore = 0;
    public int score { get; private set; } = 0;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void IncreaseScore(int points)
    {
        if (NewGame.activeSelf)
        {
            newGameScore += points;
            NewscoreText.text = newGameScore.ToString();
            PlayerPrefs.SetInt("newGameHiscore", Mathf.Max(PlayerPrefs.GetInt("newGameHiscore", 0), newGameScore));
        }
        else if (MoreGame.activeSelf)
        {
            moreGameScore += points;
            MoreGameScoreText.text = moreGameScore.ToString();
            PlayerPrefs.SetInt("moreGameHiscore", Mathf.Max(PlayerPrefs.GetInt("moreGameHiscore", 0), moreGameScore));
        }
        else
        {
            score += points;
            scoreText.text = score.ToString();
            PlayerPrefs.SetFloat("hiscore", Mathf.Max(PlayerPrefs.GetFloat("hiscore", 0), score));
        }
    }

    public void Explode()
    {
        if (NewGame.activeSelf)
        {
            newGameScore = Mathf.Max(0, newGameScore - 10);
            NewscoreText.text = newGameScore.ToString();
        }
        else if (MoreGame.activeSelf)
        {
            moreGameScore = Mathf.Max(0, moreGameScore - 10);
            MoreGameScoreText.text = moreGameScore.ToString();
        }
        else
        {
            spawner.StopSpawning();
            Time.timeScale = 0;
            StartCoroutine(ShowGameOverAfterDelay(1f));
        }
    }

    private IEnumerator ShowGameOverAfterDelay(float delay)
    {
        yield return new WaitForSecondsRealtime(delay);
        fadeImage.CrossFadeAlpha(1, 1f, false);

        if (NewGame.activeSelf)
            NewgameOverScreen?.SetActive(true);
        else if (MoreGame.activeSelf)
            MoreGameoverScreen?.SetActive(true);
        else
            gameOverScreen?.SetActive(true);

        PlayGame.SetActive(false);
        NewGame.SetActive(false);
        MoreGame.SetActive(false);
    }

    public void RestartGame()
    {
        ResetGame();
        spawner.StartSpawning();
        PlayGame.SetActive(true);
        gameOverScreen.SetActive(false);
        option.SetActive(false);

    }

    public void RestartNewGame()
    {
        ResetGame();
        NewGame.SetActive(true);
        Newoption.SetActive(false);    
        gameTimer.RestartTimer();
        NewgameOverScreen.SetActive(false);
    }

    public void RestartMoreGame()
    {
        ResetGame();
        MoreGame.SetActive(true);
        Moreoption.SetActive(false);
        MoreGameTimer.RestartTimer();
        MoreGameoverScreen.SetActive(false); 
    }

    private void ResetGame()
    {
        Time.timeScale = 1;
        score = 0;
        newGameScore = 0;
        moreGameScore = 0;
        scoreText.text = "0";
        NewscoreText.text = "0";
        MoreGameScoreText.text = "0";
        DestroyAllFruitsAndBombs();
    }

    public void QuitToLevel()
    {
        ResetGame();
        levelpage.SetActive(true);
        gameOverScreen.SetActive(false);
        NewgameOverScreen.SetActive(false);
        MoreGameoverScreen.SetActive(false);
        Newoption.SetActive(false);
        Moreoption.SetActive(false);
        MoreGame.SetActive(false);
        NewGame.SetActive(false);
        Time.timeScale = 0;
        DestroyAllFruitsAndBombs();
    }

    private void DestroyAllFruitsAndBombs()
    {
        foreach (GameObject obj in GameObject.FindGameObjectsWithTag("Fruit"))
            Destroy(obj);
        foreach (GameObject obj in GameObject.FindGameObjectsWithTag("Bomb"))
            Destroy(obj);
    }
}
