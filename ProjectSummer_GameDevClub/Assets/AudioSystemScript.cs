using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioSystemScript : MonoBehaviour
{
    AudioSource sounds;
    public AudioSource music { get; private set; }
    [SerializeField] AudioClip themeMusic;
    public static AudioSystemScript instance { get; private set; }
    private void Awake()
    {
        if (instance != null && instance != this) Destroy(this);
        else instance = this;

        sounds = transform.Find("Sounds").gameObject.GetComponent<AudioSource>();
        music = transform.Find("Music").gameObject.GetComponent<AudioSource>();
    }
    private void Start()
    {
        music.clip = themeMusic;
        music.Play();
    }
    public void PlayMusic(AudioClip clip)
    {
        music.clip = clip;
        music.Play();
    }

    public void PlaySound(AudioClip clip, Vector3 pos, float vol = 1)
    {
        sounds.transform.position = pos;
        PlaySound(clip, vol);
    }

    public void PlaySound(AudioClip clip, float vol = 1)
    {
        sounds.PlayOneShot(clip, vol);
    }
    public void TogglePlayOffThemeMusic()
    {
        if (music.isPlaying) music.Pause();
        else
        {
            music.UnPause();
        }
    }
    public void PlaySound()
    {
        sounds.UnPause();
    }
    public void Pause()
    {
        sounds.Pause();
    }
    public bool IsSoundPlaying()
    {
        return sounds.isPlaying;
    }
}
