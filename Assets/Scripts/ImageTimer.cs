using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImageTimer : MonoBehaviour {

    public bool tick;
    [SerializeField] private MuteSound _muteSound;
    [SerializeField] private float _maxTime;
    [SerializeField] private AudioClip _audioFx;
    [SerializeField] private AudioSource _audio;

    private Image _image;
    private float _currentTime;

    void Start() {
        _image = GetComponent<Image>();
        _currentTime = 0;
    }

    void Update() {
        tick = false;
        _currentTime += Time.deltaTime;
        
        if (_currentTime >= _maxTime) {
            tick = true;
            _currentTime = 0;
            if (!_muteSound.isActive)
                _audio.PlayOneShot(_audioFx);
        }

        _image.fillAmount = _currentTime / _maxTime;
    }
}
