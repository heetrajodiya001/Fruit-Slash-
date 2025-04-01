using UnityEngine;

public class Bomb : MonoBehaviour
{
    public AudioClip explosionSound;
    public AudioClip sliceSound;
    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.playOnAwake = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GetComponent<Collider>().enabled = false;

            if (GameManager.Instance.NewGame.activeSelf || GameManager.Instance.MoreGame.activeSelf)
            {
                if (sliceSound != null)
                {
                    PlaySound(sliceSound);
                    Debug.Log("Playing slice sound in NewGame/MoreGame mode.");
                }
            }
            else
            {
                if (explosionSound != null)
                {
                    PlaySound(explosionSound);
                    Debug.Log("Playing explosion sound in Classic mode.");
                }
                else
                {
                    Debug.LogError("Explosion sound is missing!");
                }
            }

            GameManager.Instance.Explode();
        }
    }

    private void PlaySound(AudioClip clip)
    {
        if (clip != null && audioSource != null)
        {
            audioSource.PlayOneShot(clip);
        }
        else
        {
            Debug.LogError("AudioSource or Clip missing in Bomb script!");
        }
    }
}
