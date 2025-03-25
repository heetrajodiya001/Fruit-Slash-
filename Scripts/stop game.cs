using UnityEngine;

public class PauseGame : MonoBehaviour
{
    public GameObject optionMenu; // Drag & drop the Option UI panel in Inspector
    public GameObject Levelpage , Play;
    public GameObject NewPlayGame;
    public GameObject MoreGame ;  
    public GameTimer GameTimer;
    public MoreGameTimer MoreGameTimer;
    public void OpenSettings()
    {
        if (optionMenu != null)
        {
            optionMenu.SetActive(true); // Enable the settings menu
            Time.timeScale = 0; // Pause the game
            print("hii");
        }
    }

    public void CloseSettings()
    {
        if (optionMenu != null)
        {
            optionMenu.SetActive(false); // Disable the settings menu
            Time.timeScale = 1; // Resume the game
        }
    }
    
    public void Resume()
    {
        optionMenu.SetActive(false);
        Time.timeScale = 1;// Resume the game
    }
    public void LevelPage()
    {
        DestroyAllFruits();
        Levelpage.SetActive(true);
        Play.SetActive(false);
        optionMenu.SetActive(false);    
        Time.timeScale = 0;
    }
    // New Game of Option
    public void NewGameofResume()
    {
        optionMenu.SetActive(false);
        Time.timeScale = 1;// Resume the game
    }
    public void NewGameofCloseSettings()
    {
        if (optionMenu != null)
        {
            optionMenu.SetActive(false); // Disable the settings menu
            Time.timeScale = 1; // Resume the game
        }
    }
    public void NewGametoLevel()
    {
        DestroyAllFruits();
        Levelpage.SetActive(true);
        NewPlayGame.SetActive(false);
        optionMenu.SetActive(false);
        GameTimer.GameOver();
        Time.timeScale = 0;

    }
    // More Game of Option
    public void MoreGameofResume()
    {
        optionMenu.SetActive(false);
        Time.timeScale = 1;// Resume the game
    }
    public void MoreGameofCloseSettings()
    {
        if (optionMenu != null)
        {
            optionMenu.SetActive(false); // Disable the settings menu
            Time.timeScale = 1; // Resume the game
        }
    }
    public void MoreGametoLevel()
    {
        DestroyAllFruits();
        Levelpage.SetActive(true);
        MoreGame.SetActive(false);
        optionMenu.SetActive(false);       
        MoreGameTimer.GameOver();
        Time.timeScale = 0;

    }
    private void DestroyAllFruits()
    {
        GameObject[] fruits = GameObject.FindGameObjectsWithTag("Fruit");
        foreach (GameObject fruit in fruits)
        {
            Destroy(fruit);
        }

        GameObject[] bombs = GameObject.FindGameObjectsWithTag("Bomb");
        foreach (GameObject bomb in bombs)
        {
            Destroy(bomb);
        }
    }
}
