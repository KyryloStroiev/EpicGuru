using AppsFlyerSDK;
using UnityEngine;

public class AppsFlyerSD : MonoBehaviour
{
	private static AppsFlyerSD _instance;

	private void Awake()
	{
		if(_instance == null)
		{
			_instance = this;
			DontDestroyOnLoad(gameObject);
		}
		else
		{
			Destroy(gameObject);
		}

	}

	private void Start()
	{
		AppsFlyer.initSDK("devkey", "appID");
		AppsFlyer.startSDK();
	}


}
