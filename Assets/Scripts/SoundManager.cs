using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public AudioSource[] SFX;
    public AudioSource LevelMusic;

    void Start()
    {
        LevelMusic.Play();
    }

    public void PlaySFX(int sfxNumber)
    {
        for(int i = 0; i < SFX.Length; i++)
        {
            SFX[i].Stop();
        }

        if (SFX.Length <= sfxNumber)
            return;

        SFX[sfxNumber].Play();
    }
}
