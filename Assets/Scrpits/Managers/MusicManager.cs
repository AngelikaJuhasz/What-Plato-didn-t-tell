using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public AudioClip backgroundMusic; // Assign this in the Inspector
    private AudioSource audioSource;

    private static MusicManager instance; // Static instance to ensure only one MusicManager exists

    void Awake()
    {
        // If instance already exists, destroy this object
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // Prevent destruction on scene load
        }
        else
        {
            Destroy(gameObject); // Destroy this object if another MusicManager exists
        }
    }

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if (backgroundMusic != null)
        {
            audioSource.clip = backgroundMusic;
            audioSource.loop = true;
            audioSource.Play();
        }
        else
        {
            Debug.LogError("Background music clip not assigned!");
        }
    }
}
