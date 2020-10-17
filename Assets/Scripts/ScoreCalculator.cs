using UnityEngine;
using TMPro;
using System;

public class ScoreCalculator : MonoBehaviour
{
    private float _currentDistanceTraveled;

    [SerializeField] private PlayerController _player;

    private bool _playerIsAlive = true;

    private void Awake()
    {
        if(!_player)
            _player = FindObjectOfType<PlayerController>();
    }

    private void OnEnable() => PlayerController.OnPlayerDeath += SaveScore;

    private void OnDisable() => PlayerController.OnPlayerDeath -= SaveScore;

    private void Update()
    {
        if(_playerIsAlive)
            _currentDistanceTraveled += _player.MoveSpeedForward * Time.deltaTime;
    }

    private void SaveScore()
    {
        _playerIsAlive = false;

        if (Math.Round(_currentDistanceTraveled / 1000f, 2) > PlayerPrefs.GetFloat("EndlessRunHighScore", 0))
        { 
            PlayerPrefs.SetFloat("EndlessRunHighScore", (float)Math.Round(_currentDistanceTraveled / 1000f, 2));
            PlayerPrefs.Save();
        }
    }

    public string CurrentScoreText() => $"You Traveled : {_currentDistanceTraveled / 1000f:0.##} AU";
}
