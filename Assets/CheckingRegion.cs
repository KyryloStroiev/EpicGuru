using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class CheckingRegion : MonoBehaviour
{
    private const string apiUrl = "https://ipinfo.io/json";

    private bool isUkraine;

    private string _loadscene = "LoadScene";
	private void Start()
	{
        StartCoroutine(GetLocation());
	}
	private IEnumerator GetLocation()
    {
       using(UnityWebRequest webRequest = UnityWebRequest.Get(apiUrl))
       {
          yield return webRequest.SendWebRequest();

            if (webRequest.result == UnityWebRequest.Result.Success)
            {
                string jsonData = webRequest.downloadHandler.text;

                LocationData data = JsonUtility.FromJson<LocationData>(jsonData);

                isUkraine = IsUkraine(data.country);
            }
            else
            {
                Debug.Log(webRequest.error);
            }
       }

        yield return new WaitForSeconds(1.5f);

        CheckCountry(isUkraine);
    }

    private bool IsUkraine(string contryCode)
    {
        return contryCode.Equals("UA");
    }

    private void CheckCountry(bool isUkraine)
    {
        if(isUkraine)
        {
            SceneManager.LoadScene(_loadscene);
        }
        else
        {
            string wikipediaURL = "https://en.wikipedia.org";

            Application.OpenURL(wikipediaURL);
		}
    }
}

[System.Serializable]
public class LocationData
{
    public string country;
}
