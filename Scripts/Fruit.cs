using UnityEngine;

public class Fruit : MonoBehaviour
{
    public GameObject whole, sliced;
    public AudioClip sliceSound; // Slice sound when fruit is cut
    private Rigidbody fruitRb;
    private Collider fruitCol;
    private ParticleSystem juiceEffect;
    private AudioSource audioSource;
    public int points = 1;
    private bool isCut = false;

    private void Awake()
    {
        fruitRb = GetComponent<Rigidbody>();
        fruitCol = GetComponent<Collider>();
        juiceEffect = GetComponentInChildren<ParticleSystem>();

        audioSource = gameObject.AddComponent<AudioSource>(); // Add AudioSource dynamically
        audioSource.playOnAwake = false;
    }

    private void Slice(Vector3 direction, Vector3 position, float force)
    {
        if (isCut) return;

        isCut = true;
        GameManager.Instance.IncreaseScore(points);

        whole.SetActive(false);
        sliced.SetActive(true);
        juiceEffect.Play();

        // Play slice sound
        if (sliceSound != null)
        {
            audioSource.PlayOneShot(sliceSound);
        }

        Rigidbody[] slices = sliced.GetComponentsInChildren<Rigidbody>();
        foreach (Rigidbody slice in slices)
        {
            slice.linearVelocity = fruitRb.linearVelocity;
            slice.AddForceAtPosition(direction * force, position, ForceMode.Impulse);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!isCut && other.CompareTag("Player"))
        {
            Blade blade = other.GetComponent<Blade>();
            Slice(blade.direction, blade.transform.position, blade.sliceForce);
        }
    }
}
