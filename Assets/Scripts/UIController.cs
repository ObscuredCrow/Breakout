using TMPro;
using UnityEngine;

public class UIController : MonoBehaviour
{
    public static UIController Instance;

    [SerializeField] private GameObject _start;
	[SerializeField] private GameObject _play;
	[SerializeField] private GameObject _gameOver;
    [SerializeField] private TMP_Text _score;
    [SerializeField] private TMP_Text _highScore;
    [SerializeField] private GameObject[] _life;

    private void SetVisibility() {
        _start.SetActive(GameController.Instance.State == GameState.Start);
		_play.gameObject.SetActive(GameController.Instance.State != GameState.Start);
        _gameOver.SetActive(GameController.Instance.State == GameState.Stop);

        for(int i = 0; i < _life.Length; i++) {
            _life[i].SetActive(GameController.Instance.Life > i);
        }
    }

    public void SetHighScore(int score) { 
        _highScore.text = $"High Score: {score}";
        PlayerPrefs.SetInt("highscore", score);
    }

    public void SetScore(int score) => _score.text = $"Score: {score}";

	private void Awake() { 
        Instance ??= this;
        SetHighScore(PlayerPrefs.GetInt("highscore", 0));
    }

    private void Update() => SetVisibility();
}