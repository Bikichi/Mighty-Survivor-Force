using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SawBladeSound : MonoBehaviour
{

    void Start()
    {
        InvokeRepeating("PlaySoundSawBlade", 0f, 2f);
    }

    public void PlaySoundSawBlade()
    {
        AudioController.Instance.PlaySound(AudioController.Instance.weaponsSpin);
    }

}
