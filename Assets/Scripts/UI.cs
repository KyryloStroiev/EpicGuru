using UnityEngine;
using UnityEngine.SceneManagement;
public class UI: MonoBehaviour 
{
	public void LoadScene(string sceneName)
	{
		SceneManager.LoadScene(sceneName);
	}
}

