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

    private float lastMusicVolume = 0.3f;
    private float lastSfxVolume = 1f;

    public AudioSource musicAus;
    public AudioSource sfxAus;

    [Header("Game musics: ")]
    public AudioClip menuBackgroundMusics;
    public AudioClip gamePlayBackgroundMusics;

    [Header("Game sounds UI: ")]
    public AudioClip win;
    public AudioClip lose;
    public AudioClip waveAlertSound;
    public AudioClip toturialSound;

    [Header("Game sounds Buttons UI: ")]
    public AudioClip UI_ButtonsClick;
    public AudioClip slectionButtonsClick;
    public AudioClip upGradesButtonsClick;
    
    public AudioClip unlock;

    [Header("Game sounds Collect: ")]
    public AudioClip learnSkils;
    public AudioClip reroll;
    
    public AudioClip collectCoin;
    public AudioClip health;

    [Header("Game sounds Battle: ")]
    public AudioClip enemyMeleeAttack;
    public AudioClip enemyRangedAttack;
    public AudioClip enemyDeath;

    public AudioClip hitEnemy;
    public AudioClip hitBoss;
    public AudioClip hitPlayer;
    public AudioClip hitMissPlayer;
    public AudioClip burnPlayer;

    [Header("Game sound VFX: ")]
    public AudioClip enemySpawn;
    public AudioClip bossSpawn;

    public AudioClip playerLVLUP;

    [Header("Game sounds PlayerShot: ")]
    public AudioClip archerShot;
    public AudioClip chesterShot;

    [Header("Game sounds Unit: ")]
    public AudioClip kunaiFly;
    public AudioClip weaponsSpin;
    public AudioClip sword;
    public AudioClip droneShot;
    public AudioClip iceDragon;
    public AudioClip fireDragon;
    public AudioClip spawnMine;
    public AudioClip mineExplode;
    public AudioClip lightning;
    public AudioClip regenSkillTick;

    [Header("Game sounds BossSkills: ")]
    public AudioClip charge;
    public AudioClip multiShoot;
    public AudioClip fireBreath;
    public AudioClip lazeBreath;
    public AudioClip bossPreCharge;
    public AudioClip bossStartPhase2;
    public AudioClip bossPrePhase2;
    public AudioClip bossExplode;

    [Header("Game sounds Special Enemy Attak: ")]
    public AudioClip dash;
    public AudioClip buff;
    public AudioClip explode;


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

    public void PlaySoundMultipleTimes(AudioClip clip, int times = 3, float interval = 0.3f, AudioSource aus = null)
    {
        if (!aus) aus = sfxAus;
        if (clip == null || aus == null || times <= 0) return;

        StartCoroutine(PlaySoundCoroutine(clip, aus, times, interval));
    }

    private IEnumerator PlaySoundCoroutine(AudioClip clip, AudioSource aus, int times, float interval)
    {
        for (int i = 0; i < times; i++)
        {
            aus.PlayOneShot(clip, sfxVolume);
            yield return new WaitForSeconds(interval);
        }
    }


    // ==========================
    // 🎧 NEW: Toggle functions
    // ==========================
    public void ToggleMusic()
    {
        if (musicAus == null) return;

        if (musicVolume > 0f)
        {
            lastMusicVolume = musicVolume;
            musicVolume = 0f;
        }
        else
        {
            musicVolume = lastMusicVolume > 0 ? lastMusicVolume : 0.3f;
        }

        musicAus.volume = musicVolume;
    }

    public void ToggleSFX()
    {
        if (sfxAus == null) return;

        if (sfxVolume > 0f)
        {
            lastSfxVolume = sfxVolume;
            sfxVolume = 0f;
        }
        else
        {
            sfxVolume = lastSfxVolume > 0 ? lastSfxVolume : 1f;
        }

        sfxAus.volume = sfxVolume;
    }
    // ==========================


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