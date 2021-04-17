using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public Animator Animator;
    public SoundManager SoundManager;
    public void Open()
    {
        Animator.SetBool("Close", false);
        SoundManager.PlaySFX(1);
    }
    public void Close()
    {
        Animator.SetBool("Close", true);
        SoundManager.PlaySFX(1);
    }
}
