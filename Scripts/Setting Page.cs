using UnityEngine;

public class SettingPage : MonoBehaviour
{
    public GameObject Level, setting;
   
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
}
