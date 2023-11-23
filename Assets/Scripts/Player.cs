using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
	private ColliderChecking _checkCollider;

	private void Awake()
	{
		_checkCollider = GetComponent<ColliderChecking>();
	}

	private void Update()
	{
		if( _checkCollider.IsObstacle)
		{
			Game.Instance.GameOver();
		}
		else if( _checkCollider.IsFinish)
		{
			Game.Instance.FinishGame();
		}
	}
}
