using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
	[SerializeField] private float _moveSpeed;
	[SerializeField] private float _jumpHeight;

	public Vector2 Direction => _direction;
	private Vector2 _direction;

	public bool IsJumping { get; private set; }

	private const float GRAVITY = -9.81f;

	private string _jumpMusic = "Jump";

	private Rigidbody2D _rigidbody;
	private ColliderChecking _checkingCollider;
	private void Awake()
	{
		_rigidbody = GetComponent<Rigidbody2D>();
		_checkingCollider = GetComponent<ColliderChecking>();
		_direction.x = 1f;
	}

	private void Update()
	{
		ApplyGravity();
		if (_checkingCollider.IsObstacle)
		{
			_direction.x = 0;
		}
	}

	private void FixedUpdate()
	{
		Move();
	}

	private void Move()
	{
		_direction.x = 1f;
		Vector2 movement = _direction * _moveSpeed * Time.fixedDeltaTime;
		_rigidbody.MovePosition(_rigidbody.position + movement);
	}

	public void Jump()
	{
		if(_checkingCollider.IsGround && !IsJumping)
		{
			AudioManager.instance.Play(_jumpMusic);
			_direction.y = Mathf.Sqrt(_jumpHeight  * -1f * GRAVITY/2f);
			_direction.x = 0.5f;
			IsJumping = true;
		}
	}	

	public void AddSpeed()
	{
		_moveSpeed++;
	}
	private void ApplyGravity()
	{
		_direction.y += GRAVITY * Time.deltaTime;
		_direction.y = Mathf.Max(_direction.y, GRAVITY*2f);

		if(_checkingCollider.IsGround && _direction.y < 0f) 
		{
			_direction.y = -2f;
			IsJumping = false;
		}
	}
}
