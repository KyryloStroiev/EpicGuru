using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class UIGame: UI
{
	[SerializeField] private TextMeshProUGUI _pointsText;

	[SerializeField] private TextMeshProUGUI _pointsFinishText;
	[SerializeField] private TextMeshProUGUI _pauseText;

	[SerializeField] private GameObject _panelGameOver;
	[SerializeField] private GameObject _panelFinish;

	[SerializeField] private Sprite _pauseActive;
	[SerializeField] private Sprite _pauseInactive;
	[SerializeField] private Button _pauseButton;

	private float _points;

	private bool _isPaused = false;
	private void Awake()
	{
		_pointsText.enabled = true;

		TogglePause();

		PointsUpdate();

		Game.Instance.AddPoints += PointsUpdate;
		Game.Instance.GameOverEvent += GameOver;
		Game.Instance.FinishEvent += EndGame;

		_panelFinish.SetActive(false);
		_panelGameOver.SetActive(false);
	}

	public void TogglePause()
	{
		_isPaused = !_isPaused;

		if(_isPaused)
		{
			_pauseButton.image.sprite = _pauseActive;
			_pauseText.enabled = true;
			Time.timeScale = 0f;
		}
		else
		{
			_pauseButton.image.sprite = _pauseInactive;
			_pauseText.enabled = false;
			Time.timeScale = 1f;
		}
	}

	private void PointsUpdate()
	{
		_points = Game.Instance.Points;
		_pointsText.text = _points.ToString();
	}

	private void GameOver()
	{
		_pointsText.enabled = false;
		_panelGameOver.SetActive(true);
	}
	private void EndGame()
	{
		StartCoroutine(EndGameCoroutine());
	}

	private IEnumerator EndGameCoroutine()
	{
		yield return new WaitForSeconds(2f);
		_pointsText.enabled = false;
		_pointsFinishText.text = $"You Score: {_points}";
		_panelFinish.SetActive(true);
	}

	private void OnDisable()
	{
		Game.Instance.AddPoints -= PointsUpdate;
		Game.Instance.GameOverEvent -= GameOver;
		Game.Instance.FinishEvent -= EndGame;
	}
}

