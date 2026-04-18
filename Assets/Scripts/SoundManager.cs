using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip radio;

    public void PlayRadio()
    {
        audioSource.PlayOneShot(radio);
    }
}
