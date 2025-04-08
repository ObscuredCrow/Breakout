using UnityEngine;

public class GameController : MonoBehaviour
{
    public static GameController Instance;

    public GameObject[] Blocks; 
    public Sprite[] BlockImages;

    [Header("Settings")]
    [HideInInspector] public GameState State = GameState.Start;
    [HideInInspector] public int Life = 3;

    [Header("Sounds")]
	[SerializeField] private AudioClip _breakClip;
	[SerializeField] private AudioClip _hitClip;
	[SerializeField] private AudioClip _bumpClip;

	private int _score = 0;
    private int _highScore = 0;
    private AudioSource _audio;

    private void Update() {
        StartGame();
        ResetGame();
    }

    public void StartGame() {
        if ((Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space)) && State == GameState.Start) {
            State = GameState.Run;
            _score = 0;
            AddToScore(0);
        }
    }

    public void AddToScore(int amount) {
        if (State != GameState.Run) return;

        _score += amount;
        UIController.Instance.SetScore(_score);
    }

    public void SetHighScore() {
        if (State != GameState.Stop) return;

        _highScore = _score > _highScore ? _score : _highScore;
        UIController.Instance.SetHighScore(_highScore);
    }

    public void StopGame() { 
        State = GameState.Stop;
        SetHighScore();
    }

    public void ResetGame() {
        if (Input.GetKeyDown(KeyCode.R) && State == GameState.Stop) {
			State = GameState.Start;
			Life = 3;
			ResetBlocks();
		} 
    }

    public void WhatBlocksLeft() {
        bool anyLeft = false;
        foreach (var block in Blocks) {
            anyLeft = block.activeInHierarchy;
            if (anyLeft)
                break;
        }

        if (anyLeft) return;

        ResetBlocks();
	}

    public void PlayHit() => _audio.PlayOneShot(_hitClip);

	public void PlayBreak() => _audio.PlayOneShot(_breakClip);

	public void PlayBump() => _audio.PlayOneShot(_bumpClip);

	private void ResetBlocks() {
		foreach (var block in Blocks) {
			block.GetComponent<ScoreTrigger>().ResetHits();
			block.SetActive(true);
		}
	}

    private void Awake() { 
        Instance ??= this;
        _audio = GetComponent<AudioSource>();
        _highScore = PlayerPrefs.GetInt("highscore", 0);
    }
}