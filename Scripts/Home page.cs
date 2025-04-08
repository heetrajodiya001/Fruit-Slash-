using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Audio;
public class HomePage : MonoBehaviour
{
    [SerializeField] private Spawner spawner;
    public GameObject Home, Level, Play, Gameover;
    public GameObject NewGame;
    public GameObject Spowen;
    public GameObject MoreGame;
    public GameObject Shop;

    public void homePage()
    {      
        Level.SetActive(true);
        Home.SetActive(false);
        Spowen.SetActive(false);
    }
    public void LevelPagetoHome()
    {
        Level.SetActive(false);
        Home.SetActive(true);
        Spowen.SetActive(false);
    }
    public void LeveltoPlaye()
    {
        Level.SetActive(false);
        Play.SetActive(true);
        // Spowen.SetActive(true);
        spawner.StartSpawning();       
        Time.timeScale = 1.0f;  
    }
    public void GameovertoLevel()
    {
        DestroyAllFruits();
        Level.SetActive(true);
        Gameover.SetActive(false);
        Spowen.SetActive(false);
       // gameTimer.UpdateTimerDisplay(0);
    }
    public void LeveltoNewGame()
    {
        Level.SetActive(false);
        NewGame.SetActive(true);
        spawner.StartSpawning();
        Time.timeScale = 1.0f;
    }
    // More Game
    public void LeveltoMoreGame()
    {
        Level.SetActive(false);
        MoreGame.SetActive(true);
        spawner.StartSpawning(); 
        Time.timeScale = 1.0f;
    }
    public void Shoptoopenshop()
    {
        Level.SetActive(false);
        Shop.SetActive(true);
    }
    public void Shoptolevel()
    {
        Level.SetActive(true);
        Shop.SetActive(false);  
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