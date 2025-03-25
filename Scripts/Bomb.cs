using UnityEngine;

public class Bomb : MonoBehaviour
{
    public AudioClip explosionSound; // Classic Mode માટે
    public AudioClip sliceSound; // New Game Mode માટે
    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GetComponent<Collider>().enabled = false;

            if (GameManager.Instance.NewGame.activeSelf)
            {
                // New Game Mode માં Slice Sound વાગશે
                if (sliceSound != null)
                {
                    audioSource.PlayOneShot(sliceSound);
                }
            }
            else if (GameManager.Instance.MoreGame.activeSelf)
            {
                // More Game Mode માં Slice Sound વાગશે
                if (sliceSound != null)
                {
                    audioSource.PlayOneShot(sliceSound);
                }
            }
            else
            {
                // Classic Mode માં Explosion Sound વાગશે
                if (explosionSound != null)
                {
                    audioSource.PlayOneShot(explosionSound);
                }
            }

            GameManager.Instance.Explode();
        }
    }

}

