using System.Collections;
using UnityEngine;

public class LogingScript : MonoBehaviour
{
    [SerializeField] GameObject HomeLoding, Homepage;
    void Start()
    {
        HomeLoding.SetActive(true);
        Homepage.SetActive(false);
        StartCoroutine(ShowLodingScreen());
    }
    private IEnumerator ShowLodingScreen()
    {
        yield return new WaitForSeconds(4);
        HomeLoding.SetActive(false);
        Homepage.SetActive(true);  
    }
   
}
