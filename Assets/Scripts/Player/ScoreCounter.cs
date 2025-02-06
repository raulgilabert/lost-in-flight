using System;
using TMPro;
using UnityEngine;

namespace Player
{
    public class ScoreCounter : MonoBehaviour
    {
        [SerializeField] private float unitsPerPoint;
        [SerializeField] private TMP_Text scoreText;
        [SerializeField] private TMP_Text deathScoreText;

        private int _score;
        private float _spawnHeight;

        private void Start()
        {
            _score = 0;
            _spawnHeight = transform.position.y;
        }

        private void FixedUpdate()
        {
            int heightScore = Mathf.FloorToInt((transform.position.y - _spawnHeight) / unitsPerPoint);
            _score = Math.Max(heightScore, _score);
        }

        private void Update()
        {
            scoreText.text = (_score * 10).ToString();
            deathScoreText.text = "Score - " + scoreText.text;
        }
    }
}
