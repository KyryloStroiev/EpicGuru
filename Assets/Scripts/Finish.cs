using UnityEngine;
public class Finish: MonoBehaviour 
{
	private Animator _animator;

	private void Start()
	{
		_animator = GetComponent<Animator>();
		Game.Instance.FinishEvent += EndGame;
	}

	private void EndGame()
	{
		_animator.SetTrigger("Finish");
	}

}

