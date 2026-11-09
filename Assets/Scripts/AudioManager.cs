using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [Header("Audio Sources")]
    [SerializeField] private AudioSource musicSource;
    [SerializeField] private AudioSource sfxSource;

    [Header("Clips")]
    [SerializeField] private AudioClip backgroundMusic;
    [SerializeField] private AudioClip flapClip;
    [SerializeField] private AudioClip hitClip;
    [SerializeField] private AudioClip pointClip;

    [Header("Volume")]
    [SerializeField, Range(0f, 1f)] private float musicVolume = 0.3f;
    [SerializeField, Range(0f, 1f)] private float sfxVolume = 1f;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        musicSource.clip = backgroundMusic;
        musicSource.loop = true;
        musicSource.volume = musicVolume;
        musicSource.Play();

        sfxSource.loop = false;
        sfxSource.volume = sfxVolume;
    }

    public void PlayFlap()  => sfxSource.PlayOneShot(flapClip);
    public void PlayHit()   => sfxSource.PlayOneShot(hitClip);
    public void PlayPoint() => sfxSource.PlayOneShot(pointClip);
}