using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioController : MonoBehaviour
{
    public static AudioController Instance;

    [Header("Audio Sources")]
    public AudioSource musicSource;
    public AudioSource soundFxSource;

    [Header("Sounds")]
    public AudioClip menuConfirm;
    public AudioClip menuCancel;
    public AudioClip menuClick;
    public AudioClip buyItem;

    [Header("Musics")]
    public AudioClip mainMenuMusic;

    //PlayerPrefs
    private const string MUSIC_VOLUME_KEY = "music_volume";
    private const string SOUND_FX_VOLUME_KEY = "sound_fx_volume";

    [Header("Others")]
    public float musicVolume;
    public float soundFxVolume;

    [Header("Sliders")]
    public Slider musicVolumeSlider;
    public Slider soundFxVolumeSlider;

    private void Awake()
    {
        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        LoadAudioSettings();

        // Adiciona um listener ao evento OnValueChanged do slider de volume da música
        musicVolumeSlider.onValueChanged.AddListener(UpdateMusicVolume);

        // Adiciona um listener ao evento OnValueChanged do slider de volume dos efeitos sonoros
        soundFxVolumeSlider.onValueChanged.AddListener(UpdateSoundFxVolume);
    }

    // Método para atualizar o volume da música quando o slider é movido
    private void UpdateMusicVolume(float value)
    {
        musicVolume = value;
        musicSource.volume = musicVolume;

        SaveAudioSettings();
    }

    // Método para atualizar o volume dos efeitos sonoros quando o slider é movido
    private void UpdateSoundFxVolume(float value)
    {
        // Verifica se o som atual ainda está tocando
        if (soundFxSource.isPlaying)
        {
            return; // retorna sem fazer nada
        }

        soundFxSource.volume = soundFxVolumeSlider.value;
        PlaySound(menuConfirm);
    }

    public void PlaySound(AudioClip fxSound)
    {
        soundFxSource.PlayOneShot(fxSound);
    }

    public void playMusic(AudioClip FxMusic)
    {
        musicSource.clip = FxMusic;
        musicSource.Play();
    }

    private void LoadAudioSettings()
    {
        musicVolume = PlayerPrefs.GetFloat(MUSIC_VOLUME_KEY, 1f);
        soundFxVolume = PlayerPrefs.GetFloat(SOUND_FX_VOLUME_KEY, 1f);

        // Atualiza os sliders de volume da música e dos efeitos sonoros com os valores carregados
        musicVolumeSlider.value = musicVolume;
        soundFxVolumeSlider.value = soundFxVolume;

        // Aplica as configurações de áudio carregadas para o gerenciador de áudio ou outros objetos relevantes
        // Por exemplo:
        // GetComponent<AudioSource>().volume = musicVolume;

        // Atualiza também os volumes dos áudios
        musicSource.volume = musicVolume;
        soundFxSource.volume = soundFxVolume;
    }

    public void SaveAudioSettings()
    {
        PlayerPrefs.SetFloat(MUSIC_VOLUME_KEY, musicVolume);
        PlayerPrefs.SetFloat(SOUND_FX_VOLUME_KEY, soundFxVolume);
        PlayerPrefs.Save();
    }
}