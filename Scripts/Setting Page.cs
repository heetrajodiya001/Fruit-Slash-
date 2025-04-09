using UnityEngine;

public class SettingPage : MonoBehaviour
{
    public GameObject Level, setting;
    public GameObject shop;
    public GameObject Blade,wall;
    public void SettingtoLevel()
    {
        setting.SetActive(false);
        Level.SetActive(true);
    }
    public void Setting()
    {
        setting.SetActive(true);
        Level.SetActive(false);
    }
    public void leveltoshop()
    {
        shop.SetActive(true);
        Blade.SetActive(true);
        Level.SetActive(false);
       
    }
    public void bladtowall()
    {
        Blade.SetActive(false);
        wall.SetActive(true);   
    }
    public void walltoblad()
    {
        Blade.SetActive(true);
        wall.SetActive(false);
    }
}
