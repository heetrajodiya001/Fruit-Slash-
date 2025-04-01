using UnityEngine;
using UnityEngine.UI;
using System.Collections;

[DefaultExecutionOrder(-1)]
public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [SerializeField] private Blade blade;
    [SerializeField] private Spawner spawner;
    [SerializeField] private Text scoreText, NewscoreText, MoreGameScoreText, classicBestscore, NewGameBestScore, MoreGameBestScore;
    [SerializeField] private Image fadeImage, NewFadeImg, MoreFadImg;
    [SerializeField] private GameObject gameOverScreen, PlayGame, option, levelpage, Newoption, NewgameOverScreen, MoreGameoverScreen, Moreoption;
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

    private void Start()
    {
        LoadBestScores();
    }

    private void LoadBestScores()
    {
        int bestScore = PlayerPrefs.GetInt("hiscore", 0);
        classicBestscore.text = bestScore.ToString();
        NewGameBestScore.text = bestScore.ToString();
        MoreGameBestScore.text = bestScore.ToString();
    }

    public void IncreaseScore(int points)
    {
        if (NewGame.activeSelf)
        {
            newGameScore += points;
            NewscoreText.text = newGameScore.ToString();
        }
        else if (MoreGame.activeSelf)
        {
            moreGameScore += points;
            MoreGameScoreText.text = moreGameScore.ToString();
        }
        else
        {
            score += points;
            scoreText.text = score.ToString();
        }
        UpdateBestScore();
    }

    private void UpdateBestScore()
    {
        int currentBest = Mathf.Max(PlayerPrefs.GetInt("hiscore", 0), score, newGameScore, moreGameScore);
        PlayerPrefs.SetInt("hiscore", currentBest);
        classicBestscore.text = currentBest.ToString();
        NewGameBestScore.text = currentBest.ToString();
        MoreGameBestScore.text = currentBest.ToString();
    }

    public void Explode()
    {
        if (NewGame.activeSelf)
        {
            newGameScore = Mathf.Max(0, newGameScore - 10);
            NewscoreText.text = newGameScore.ToString();
            DestroyAllFruitsAndBombs();
            StartCoroutine(NewExplodeSequence());
        }
        else if (MoreGame.activeSelf)
        {
            moreGameScore = Mathf.Max(0, moreGameScore - 10);
            MoreGameScoreText.text = moreGameScore.ToString();
            DestroyAllFruitsAndBombs();
            StartCoroutine(MoreExplodeSequence());
        }
        else
        {
            spawner.StopSpawning();
            Time.timeScale = 0;
            StartCoroutine(ShowGameOverAfterDelay(1f));
            DestroyAllFruitsAndBombs();
        }
    }

    private IEnumerator ShowGameOverAfterDelay(float delay)
    {
        yield return new WaitForSecondsRealtime(delay);
        fadeImage.CrossFadeAlpha(1, 1f, false);
        gameOverScreen?.SetActive(true);
        PlayGame.SetActive(false);
        NewGame.SetActive(false);
        MoreGame.SetActive(false);
    }

    private IEnumerator FadeSequence(Image fadeImage)
    {
        float elapsed = 0f, duration = 0.5f;
        while (elapsed < duration)
        {
            float t = Mathf.Clamp01(elapsed / duration);
            fadeImage.color = Color.Lerp(Color.clear, Color.white, t);
            elapsed += Time.unscaledDeltaTime;
            yield return null;
        }
        yield return new WaitForSecondsRealtime(1f);
        elapsed = 0f;
        while (elapsed < duration)
        {
            float t = Mathf.Clamp01(elapsed / duration);
            fadeImage.color = Color.Lerp(Color.white, Color.clear, t);
            elapsed += Time.unscaledDeltaTime;
            yield return null;
        }
    }

    private IEnumerator NewExplodeSequence() => FadeSequence(NewFadeImg);
    private IEnumerator MoreExplodeSequence() => FadeSequence(MoreFadImg);

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
        score = newGameScore = moreGameScore = 0;
        scoreText.text = NewscoreText.text = MoreGameScoreText.text = "0";
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