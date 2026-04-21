using UnityEngine;
using Random = System.Random;

public class SoundManager : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip radio;
    [SerializeField] private AudioClip whistle;
    [SerializeField] private AudioClip[] pageTurns;
    
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

    public void PlayPageTurn()
    {
        var rand = new Random();
        var pos = rand.Next(0, pageTurns.Length);
        var pageTurn = pageTurns[pos];
        audioSource.PlayOneShot(pageTurn);
    }
}
