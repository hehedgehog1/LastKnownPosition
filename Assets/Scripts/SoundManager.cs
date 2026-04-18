using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip radio;
    [SerializeField] private AudioClip whistle;
    
    public static SoundManager Instance;

    private void Awake()
    {
        if (Instance is null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
        
        DontDestroyOnLoad(gameObject);
    }
    
    public void PlayRadio()
    {
        audioSource.PlayOneShot(radio);
    }

    public void PlayWhistle()
    {
        audioSource.PlayOneShot(whistle);
    }
}
