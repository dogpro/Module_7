using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {
    [SerializeField] private MuteSound _muteSound;
    [SerializeField] private ImageTimer _resourcesTimer;
    [SerializeField] private ImageTimer _useResources;
    [SerializeField] private GameObject _gameOverScreen;
    [SerializeField] private Text _gameOverStatusText;
    [SerializeField] private Text _gameOverTitleText;

    [SerializeField] private List<Text> _statusTextUnitList;

    [SerializeField] private Image _raidTimeImage;

    [SerializeField] private Button _technicianButton;
    [SerializeField] private Button _starshipButton;

    [SerializeField] private int _resourcesCount;

    [SerializeField] private int _technicianCount;
    [SerializeField] private int _resourcePerTechnician;
    [SerializeField] private int _technicianCost;
    [SerializeField] private float _technicianCreateTime;

    [SerializeField] private int _starshipCount;
    [SerializeField] private int _resourceToStarship;
    [SerializeField] private int _starshipCost;
    [SerializeField] private float _starshipCreateTime;

    [SerializeField] private int _enemyStarshipCount;

    [SerializeField] private Text _raidCountText;
    [SerializeField] private float _raidMaxTime;
    [SerializeField] private int _nextRaid;
    [SerializeField] private int _raidIncrease;
    [SerializeField] private int _movesToAttack;
    [SerializeField] private int _movesToWin;
    [SerializeField] private int _maxResourceCreit;
    [SerializeField] private Text _storyText;

    [SerializeField] private AudioSource _audio;
    [SerializeField] private AudioClip _createStarshipClip;
    [SerializeField] private AudioClip _createTechnicianClip;
    [SerializeField] private AudioClip _raidStartClip;
    [SerializeField] private AudioClip _gameOverClip;
    [SerializeField] private AudioClip _victoryClip;

    private float _technicianTimer;
    private float _starshipTimer;
    private float _raidTimer;
    private int _raidCount;

    private int _statusStarshipCount;
    private int _statusEnemyCount;
    private int _statusTechnicianCount;
    private int _statusResCount;
    private float _statusTime;
    private bool _isGameOver;

    public void CreateTechnician() {
        _resourcesCount -= _technicianCost;
        _technicianTimer = 0;
        _technicianButton.interactable = false;
    }

    public void CreateStarship() {
        _resourcesCount -= _starshipCost;
        _starshipTimer = 0;
        _starshipButton.interactable = false;
    }

    private void UpdateStatusBarText() {
        _statusTextUnitList[0].text = _technicianCount.ToString();
        _statusTextUnitList[1].text = _starshipCount.ToString();
        _statusTextUnitList[2].text = _resourcesCount.ToString();
        _statusTextUnitList[3].text = _enemyStarshipCount.ToString();
    }

    private void UpdateStoryText() {
        if (_resourcesCount >= 0) {
            _storyText.text = $"Командир! Разведка доложила,\nчто враги на подходе!\nПо данным аналитиков, мы превосходим их " +
                              $"числом и сможем отбить атаку за {_movesToWin} волн!\nВсе в ваших руках, Командир!";
        }
        else {
            _storyText.text = $"Ресурсы на исходе?\n У нас есть еще\n немного запасов ({_maxResourceCreit * -1})\n с соседнего корабля." +
                              $"\nМожем нанять еще техников\nдля погашения долга.";
        }
    }

    private void Awake() {
        _technicianTimer = _technicianCreateTime + 2;
        _starshipTimer = _starshipCreateTime + 2;
        _raidCount = 0;
        _isGameOver = false;
    }

    private void Start() {
        Time.timeScale = 1;

        UpdateStatusBarText();

        _statusTime = 0;
        _raidTimer = 0;
        _raidCountText.text = $"До атаки {_movesToAttack}\nволны";
    }

    private void Update() {
        _statusTime += Time.deltaTime;

        _raidTimer += Time.deltaTime;
        _raidTimeImage.fillAmount = _raidTimer / _raidMaxTime;

        if (_raidTimer > _raidMaxTime) {
            _raidTimer = 0;
            _raidMaxTime += 5;
            _raidCount++;

            if (_raidCount >= _movesToAttack) {
                if (!_muteSound.isActive)
                    _audio.PlayOneShot(_raidStartClip);

                _starshipCount -= _nextRaid;

                _statusEnemyCount += _nextRaid;

                _nextRaid += _raidIncrease;
                _enemyStarshipCount = _nextRaid;

                _raidCountText.text = $"{_movesToWin - _raidCount} волн\nдо победы";
            }
            else {
                _raidCountText.text = $"До атаки {_movesToAttack - _raidCount}\nволны";
            }
        }

        if (_resourcesTimer.tick) {
            _resourcesCount += _technicianCount * _resourcePerTechnician;
            _statusResCount += _resourcesCount;
        }

        if (_useResources.tick) {
            _resourcesCount -= _starshipCount * _resourceToStarship;
        }

        if (_technicianTimer < _technicianCreateTime) {
            _technicianTimer += Time.deltaTime;
            _technicianButton.image.fillAmount = _technicianTimer / _technicianCreateTime;
        }
        else if (_technicianTimer < _technicianCreateTime + 1) {
            if (!_muteSound.isActive)
                _audio.PlayOneShot(_createTechnicianClip);

            _technicianButton.interactable = true;
            _technicianCount++;
            _statusTechnicianCount++;
            _technicianTimer = _technicianCreateTime + 2;
        }

        if (_starshipTimer < _starshipCreateTime) {
            _starshipTimer += Time.deltaTime;
            _starshipButton.image.fillAmount = _starshipTimer / _starshipCreateTime;
        }
        else if (_starshipTimer < _starshipCreateTime + 1) {
            if (!_muteSound.isActive)
                _audio.PlayOneShot(_createStarshipClip);

            _starshipButton.interactable = true;
            _starshipCount++;
            _statusStarshipCount++;
            _starshipTimer = _starshipCreateTime + 2;
        }

        if (((_maxResourceCreit * -1) >= _technicianCost) && (_technicianTimer == _technicianCreateTime + 2)) {
            _technicianButton.interactable = true;
        }
        else {
            _technicianButton.interactable = false;
        }

        if ((_resourcesCount >= _starshipCost) && (_starshipTimer == _starshipCreateTime + 2)) {
            _starshipButton.interactable = true;
        }
        else {
            _starshipButton.interactable = false;
        }

        UpdateStatusBarText();
        UpdateStoryText();
        
        if ((_starshipCount < 0 || _maxResourceCreit >= _resourcesCount || _raidCount >= _movesToWin) && !_isGameOver) {
            Time.timeScale = 0;
            _isGameOver = true;

            if (_raidCount >= _movesToWin) {
                _gameOverTitleText.text = "YOU WIN";
                if (!_muteSound.isActive)
                    _audio.PlayOneShot(_victoryClip);
            }
            else {
                _gameOverTitleText.text = "YOU LOSE";
                if (!_muteSound.isActive)
                    _audio.PlayOneShot(_gameOverClip);
            }

            var time = TimeSpan.FromSeconds(_statusTime);
            string resultTime = $"{time.Minutes}m {time.Seconds}s";

            _gameOverStatusText.text = $"Время игры:{resultTime}\nУбито врагов:{_statusEnemyCount}\nРесурсов собрано:{_statusResCount}\n" +
                                       $"Техников создано:{_statusTechnicianCount}\nКораблей создано:{_statusStarshipCount}\n";

            _gameOverScreen.SetActive(true);
        }

    }
}
