using System;
using UnityEngine;
using OneSignalSDK;
using AppsFlyerSDK;

public class Game : MonoBehaviour
{
	public float Points { get; set; }

	public event Action GameOverEvent;

	public event Action AddPoints;

	public event Action FinishEvent;

	private static Game _instance;

	public static Game Instance
	{
		get
		{
			if(_instance == null)
			{
				_instance = Instantiate(new GameObject("Game")).AddComponent<Game>();
			}
			
			return _instance;
		}
	}

	private string _backgroundMusic = "Background";
	private string _takeMusic = "Take";

	private void Awake()
	{
		string customId = "12345678";
		AppsFlyer.setCustomerUserId(customId);

		AppsFlyer.startSDK();

		OneSignal.Initialize("635c5f12-bb1c-4e8a-92a5-65636c604328");

		if (_instance != null && _instance != this) 
		{
			Destroy(this.gameObject);
		}
		else
		{
			_instance = this;
			DontDestroyOnLoad(this.gameObject);
		}
		Points = 0;

		
	}

	private void Start()
	{
		AudioManager.instance.Play(_backgroundMusic);
	}

	public void GameOver()
	{
		GameOverEvent?.Invoke();
	}

	public void FinishGame()
	{
		FinishEvent?.Invoke();
	}

	public void Score()
	{
		AudioManager.instance.Play(_takeMusic);
		Points++;
		AddPoints?.Invoke();
	}


}
