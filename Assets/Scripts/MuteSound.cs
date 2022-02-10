using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MuteSound : MonoBehaviour {
    [SerializeField] private Sprite _activeSprite;
    [SerializeField] private Sprite _defaultSprite;
    [SerializeField] private Button _muteSoundButton;

    public bool isActive = false;

    public void MuteSoundClick() {
        if (!isActive) {
            isActive = true;
            _muteSoundButton.image.sprite = _defaultSprite;
        }
        else {
            isActive = false;
            _muteSoundButton.image.sprite = _activeSprite;
        }
    }
}
