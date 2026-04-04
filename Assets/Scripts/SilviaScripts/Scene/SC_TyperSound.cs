using System;
using UnityEngine;

public class SC_TyperSound : MonoBehaviour
{
    [SerializeField] private AudioSource audio;
    [SerializeField] private AudioClip clip;

    private void OnEnable()
    {
        SC_HandWrittingManager.CharacterRevealer += MakeSound;
    }

    private void OnDisable()
    {
        SC_HandWrittingManager.CharacterRevealer -= MakeSound;
    }

    private void MakeSound()
    {
        if (!audio.isPlaying) audio.PlayOneShot(clip);
    }
}
