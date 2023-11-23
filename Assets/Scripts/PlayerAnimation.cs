using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    private PlayerMovement _movement;
    private Animator _animator;

	private void Start()
	{
		_movement = GetComponent<PlayerMovement>();
		_animator = GetComponent<Animator>();

		Game.Instance.GameOverEvent += GameOver;
		Game.Instance.FinishEvent += EndGame;
	}

	void Update()
    {
		_animator.SetFloat("Run", _movement.Direction.x);
		_animator.SetBool("Jump", _movement.IsJumping);
    }

	private void GameOver()
	{
		_animator.SetTrigger("Death");
	}

	private void EndGame()
	{
		_animator.SetTrigger("Finish");
	}

	private void OnDisable()
	{

		Game.Instance.GameOverEvent -= GameOver;
		Game.Instance.FinishEvent -= EndGame;
	}
}
