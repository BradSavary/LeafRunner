using UnityEngine;

/// <summary>
/// Gère tous les sons du jeu (musique et effets sonores)
/// Pattern Singleton : une seule instance dans toute la scène
/// </summary>
public class AudioManager : MonoBehaviour
{
    // ===== SINGLETON PATTERN =====
    public static AudioManager Instance { get; private set; }

    [Header("Musique de Fond")]
    [Tooltip("Clip audio pour la musique de fond")]
    public AudioClip backgroundMusic;
    
    [Tooltip("Volume de la musique (0 à 1)")]
    [Range(0f, 1f)]
    public float musicVolume = 0.5f;
    
    private AudioSource musicSource;

    [Header("Effets Sonores")]
    [Tooltip("Son du saut")]
    public AudioClip jumpSound;
    
    [Tooltip("Son de la course")]
    public AudioClip runningSound;
    
    [Tooltip("Volume des effets sonores (0 à 1)")]
    [Range(0f, 1f)]
    public float sfxVolume = 0.7f;
    
    private AudioSource sfxSource;
    private AudioSource runningSource;

    void Awake()
    {
        // Singleton : vérifier s'il existe déjà une instance
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        
        Instance = this;
        DontDestroyOnLoad(gameObject); // Garde l'AudioManager entre les scènes
        
        // Créer les AudioSources
        SetupAudioSources();
    }

    /// <summary>
    /// Configure les AudioSources nécessaires
    /// </summary>
    void SetupAudioSources()
    {
        // AudioSource pour la musique de fond (en boucle)
        musicSource = gameObject.AddComponent<AudioSource>();
        musicSource.loop = true;
        musicSource.playOnAwake = false;
        musicSource.volume = musicVolume;
        
        // AudioSource pour les effets sonores ponctuels (saut, collectibles, etc.)
        sfxSource = gameObject.AddComponent<AudioSource>();
        sfxSource.loop = false;
        sfxSource.playOnAwake = false;
        sfxSource.volume = sfxVolume;
        
        // AudioSource pour le son de course (en boucle)
        runningSource = gameObject.AddComponent<AudioSource>();
        runningSource.loop = true;
        runningSource.playOnAwake = false;
        runningSource.volume = sfxVolume;
    }

    /// <summary>
    /// Joue la musique de fond
    /// </summary>
    public void PlayBackgroundMusic()
    {
        if (backgroundMusic != null && musicSource != null)
        {
            musicSource.clip = backgroundMusic;
            musicSource.Play();
            Debug.Log("AudioManager: Musique de fond démarrée");
        }
        else
        {
            Debug.LogWarning("AudioManager: Musique de fond non assignée !");
        }
    }

    /// <summary>
    /// Arrête la musique de fond
    /// </summary>
    public void StopBackgroundMusic()
    {
        if (musicSource != null)
        {
            musicSource.Stop();
        }
    }

    /// <summary>
    /// Joue le son du saut
    /// </summary>
    public void PlayJumpSound()
    {
        if (jumpSound != null && sfxSource != null)
        {
            sfxSource.PlayOneShot(jumpSound);
        }
        else
        {
            Debug.LogWarning("AudioManager: Son de saut non assigné !");
        }
    }

    /// <summary>
    /// Démarre le son de course (en boucle)
    /// </summary>
    public void PlayRunningSound()
    {
        if (runningSound != null && runningSource != null && !runningSource.isPlaying)
        {
            runningSource.clip = runningSound;
            runningSource.Play();
        }
    }

    /// <summary>
    /// Arrête le son de course
    /// </summary>
    public void StopRunningSound()
    {
        if (runningSource != null)
        {
            runningSource.Stop();
        }
    }

    /// <summary>
    /// Change le volume de la musique
    /// </summary>
    public void SetMusicVolume(float volume)
    {
        musicVolume = Mathf.Clamp01(volume);
        if (musicSource != null)
        {
            musicSource.volume = musicVolume;
        }
    }

    /// <summary>
    /// Change le volume des effets sonores
    /// </summary>
    public void SetSFXVolume(float volume)
    {
        sfxVolume = Mathf.Clamp01(volume);
        if (sfxSource != null)
        {
            sfxSource.volume = sfxVolume;
        }
        if (runningSource != null)
        {
            runningSource.volume = sfxVolume;
        }
    }

    /// <summary>
    /// Active/Désactive le son complètement
    /// </summary>
    public void ToggleMute(bool mute)
    {
        AudioListener.volume = mute ? 0 : 1;
    }
}
