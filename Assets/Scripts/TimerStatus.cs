using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerStatus : MonoBehaviour {
    [SerializeField] private Button _gamePauseButton;
    [SerializeField] private GameObject _gameStartButton;
    [SerializeField] private Button _gameIncreaseButton;
    [SerializeField] private Button _gameDecreaseButton;

    [SerializeField] private Sprite _increaseImageClick;
    [SerializeField] private Sprite _decreaseImageClick;
    [SerializeField] private Sprite _increaseImageDefault;
    [SerializeField] private Sprite _decreaseImageDefault;

    public void PasueGame() {
        Time.timeScale = 0;
        _gamePauseButton.gameObject.SetActive(false);
        _gameStartButton.gameObject.SetActive(true);

        _gameIncreaseButton.image.sprite = _increaseImageDefault;
        _gameDecreaseButton.image.sprite = _decreaseImageDefault;
    }

    public void StartGame() {
        Time.timeScale = 1;
        _gameStartButton.gameObject.SetActive(false);
        _gamePauseButton.gameObject.SetActive(true);
        _gameIncreaseButton.image.sprite = _increaseImageDefault;
        _gameDecreaseButton.image.sprite = _decreaseImageDefault;
    }

    public void IncreaseGame() {
        Time.timeScale = 2;
        _gameIncreaseButton.image.sprite = _increaseImageClick;
        _gameDecreaseButton.image.sprite = _decreaseImageDefault;
    }

    public void DecreaseGame() {
        Time.timeScale = 0.5f;
        _gameDecreaseButton.image.sprite = _decreaseImageClick;
        _gameIncreaseButton.image.sprite = _increaseImageDefault;
    }
}
