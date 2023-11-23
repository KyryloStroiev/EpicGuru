using UnityEngine;

public class Coins : MonoBehaviour
{
	private void OnTriggerEnter2D(Collider2D collision)
	{
		Game.Instance.Score();
		Destroy(gameObject);
	}
}

