using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public AudioClip clickSound; // Assign this in the Inspector
    private AudioSource audioSource;

    private static SoundManager instance; // Static instance to ensure only one SoundManager exists

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
            Destroy(gameObject); // Destroy this object if another SoundManager exists
        }
    }

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // Left mouse button
        {
            PlayClickSound();
        }
    }

    void PlayClickSound()
    {
        if (clickSound != null)
        {
            audioSource.PlayOneShot(clickSound); // Play the sound on button click
        }
        else
        {
            Debug.LogError("Click sound clip not assigned!");
        }
    }
}
