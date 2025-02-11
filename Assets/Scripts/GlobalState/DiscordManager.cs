using System;
using System.Collections;
using Discord;
using FSM;
using Player;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace GlobalState
{
    public class DiscordManager : MonoBehaviour
    {
        private static DiscordManager _instance;
        
        private Discord.Discord _discord;
        private StateMachine _gameStateMachine;
        
        private ActivityManager ActivityManager => _discord.GetActivityManager();

        private string _state;
        private int _score;

        private void Awake()
        {
            if (_instance != null)
            {
                Destroy(gameObject);
                return;
            }
            
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }

        private IEnumerator Start()
        {
            _state = "In Game";
            _score = 0;
            UpdateRichPresence();
            
            yield return new WaitUntil( () => GameManager.Instance != null );
            _gameStateMachine = GameManager.Instance.GetComponent<StateMachine>();
            
            _gameStateMachine.onStateEnter.AddListener(OnGameStateChanged);
            GameManager.Instance.player.GetComponent<ScoreCounter>().onScoreChanged.AddListener(OnScoreChanged);
            
            SceneManager.sceneLoaded += OnSceneLoaded;
        }

        private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            if (_state != null && scene.buildIndex == 1)
            {
                SceneManager.sceneLoaded -= OnSceneLoaded;
                StartCoroutine(Start());
            }
        }

        private void OnEnable()
        {
            try
            {
                _discord = new Discord.Discord(1338929088655786014, (UInt64)CreateFlags.NoRequireDiscord);
            }
            catch (ResultException)
            {
                enabled = false;
                return;
            }
            
            UpdateRichPresence();
        }

        private void OnDisable()
        {
            _discord?.Dispose();
        }

        private void Update()
        {
            _discord?.RunCallbacks();
        }

        private void OnGameStateChanged(string state)
        {
            _state = state;
            UpdateRichPresence();
        }

        private void OnScoreChanged(int score)
        {
            _score = score;
            UpdateRichPresence();
        }

        private void UpdateRichPresence()
        {
            
            var activity = new Activity
            {
                Details = _state == null ? "In Menu" : $"{_state} | {_score} points",
                Instance = false,
            };
            
            ActivityManager.UpdateActivity(activity, (result) =>
            {
                if (result != Result.Ok)
                {
                    Debug.LogWarning("Discord Rich Presence update failed!");
                }
            });
        }
    }
}