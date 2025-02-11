using System;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

namespace Player
{
    public class ScoreCounter : MonoBehaviour
    {
        [SerializeField] private float unitsPerPoint;
        [SerializeField] private TMP_Text scoreText;
        [SerializeField] private TMP_Text deathScoreText;

        public UnityEvent<int> onScoreChanged;
        
        private int _score;
        private float _spawnHeight;

        private void Start()
        {
            _score = 0;
            _spawnHeight = transform.position.y;
        }

        private void FixedUpdate()
        {
            int oldScore = _score;
            
            int heightScore = Mathf.FloorToInt((transform.position.y - _spawnHeight) / unitsPerPoint);
            _score = Math.Max(heightScore, _score);
            
            if (_score != oldScore) onScoreChanged.Invoke(_score);
        }

        private void Update()
        {
            scoreText.text = (_score * 10).ToString();
            deathScoreText.text = "Score - " + scoreText.text;
        }
    }
}
