using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundControl : MonoBehaviour
{
    [SerializeField] private AudioSource _audio;
    [SerializeField] private AudioClip _clickFx;

    public void FXSoundClick() {
        _audio.PlayOneShot(_clickFx);
    }
}
