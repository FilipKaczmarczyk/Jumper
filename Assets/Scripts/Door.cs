using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public Animator Animator;
    public SoundManager SoundManager;

    private static int CLOSE_ANIM_HASH = Animator.StringToHash("Close");

    public bool IsOpen;

    public void Open()
    {
        Animator.SetBool(CLOSE_ANIM_HASH, false);
        SoundManager.PlaySFX(1);
        IsOpen = true;
    }
    public void Close()
    {
        Animator.SetBool(CLOSE_ANIM_HASH, true);
        SoundManager.PlaySFX(1);
        IsOpen = false;
    }
}
