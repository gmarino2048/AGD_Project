using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXController : MonoBehaviour {

    [Header("Audio Source")]
    public AudioSource SFXPlayer;
    public float SFXScale;

    [Header("Audio Clips")]
    public AudioClip KnifeCut;

    public void PlayCut () 
    {
        SFXPlayer.PlayOneShot(KnifeCut, SFXScale);
    }
}
