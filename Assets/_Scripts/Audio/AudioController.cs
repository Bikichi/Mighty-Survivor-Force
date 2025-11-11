using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AudioController : Singleton<AudioController>
{
    public static AudioClip audioClip;

    [Header("Main Settings:")]
    [Range(0, 1)]
    public float musicVolume = 0.3f;
    /// the sound fx volume
    [Range(0, 1)]
    public float sfxVolume = 1f;

    public AudioSource musicAus;
    public AudioSource sfxAus;

    [Header("Game sounds: ")]
    public AudioClip UI_ButtonsClick;
    public AudioClip slectionButtonsClick;
    public AudioClip upGradesButtonsClick;
    
    public AudioClip unlock;
    
    public AudioClip learnSkils;
    public AudioClip reroll;
    
    public AudioClip collectCoin;
    public AudioClip health;

    public AudioClip enemyMeleeAttack;
    public AudioClip enemyRangedAttack;


    [Header("Game musics: ")]
    public AudioClip menuBackgroundMusics;
    public AudioClip gamePlayBackgroundMusics;

    private void Start()
    {
        PlayMusic(menuBackgroundMusics, true);
    }
    /// <summary>
    /// Play Sound Effect
    /// </summary>
    /// <param name="clips">Array of sounds</param>
    /// <param name="aus">Audio Source</param>
    public void PlayRandomSound(AudioClip[] clips, AudioSource aus = null)
    {
        if (!aus)
        {
            aus = sfxAus;
        }

        if (clips != null && clips.Length > 0 && aus)
        {
            var randomIdx = Random.Range(0, clips.Length);
            aus.PlayOneShot(clips[randomIdx], sfxVolume);
        }
    }

    /// <summary>
    /// Play Sound Effect
    /// </summary>
    /// <param name="clip">Sounds</param>
    /// <param name="aus">Audio Source</param>
    public void PlaySound(AudioClip clip, AudioSource aus = null)
    {
        if (!aus)
        {
            aus = sfxAus;
        }

        if (clip != null && aus)
        {
            aus.PlayOneShot(clip, sfxVolume);
        }
    }

    /// <summary>
    /// Play Music
    /// </summary>
    /// <param name="musics">Array of musics</param>
    /// <param name="loop">Can Loop</param>
    public void PlayRandomMusic(AudioClip[] musics, bool loop = true)
    {
        if (musicAus && musics != null && musics.Length > 0)
        {
            var randomIdx = Random.Range(0, musics.Length);

            musicAus.clip = musics[randomIdx];
            musicAus.loop = loop;
            musicAus.volume = musicVolume;
            musicAus.Play();
        }
    }

    /// <summary>
    /// Play Music
    /// </summary>
    /// <param name="music">music</param>
    /// <param name="canLoop">Can Loop</param>
    public void PlayMusic(AudioClip music, bool canLoop)
    {
        if (musicAus && music != null)
        {
            musicAus.clip = music;
            musicAus.loop = canLoop;
            musicAus.volume = musicVolume;
            musicAus.Play();
        }
    }

    /// <summary>
    /// Set volume for audiosource
    /// </summary>
    /// <param name="vol">New Volume</param>
    public void SetMusicVolume(float vol)
    {
        if (musicAus) musicAus.volume = vol;
    }

    /// <summary>
    /// Stop play music or sound effect
    /// </summary>
    public void StopPlayMusic() 
    {
        if (musicAus) musicAus.Stop();
    }
}