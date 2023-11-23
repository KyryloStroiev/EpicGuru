using UnityEngine;

public class ColliderChecking: MonoBehaviour
{
	[SerializeField] private float _raycastDistance = 0.01f;
	[SerializeField] private float _circleRadius;
	[SerializeField] private float _circleOffsetY;


	[SerializeField] private LayerMask _obstacleMask;
	[SerializeField] private LayerMask _finishMask;

	public bool IsGround {  get; private set; }
	public bool IsObstacle { get; private set; }
	public bool IsFinish { get; private set; }

	private Rigidbody2D _rigidbody;
	private Renderer _renderer;
	private void Awake()
	{
		_rigidbody = GetComponent<Rigidbody2D>();
		_renderer = _rigidbody.gameObject.GetComponent<Renderer>();
	}

	private void Update()
	{
		IsGround = CheckGround();
		IsObstacle = CheckCollision(_obstacleMask);
		IsFinish = CheckCollision(_finishMask);

		Debug.Log(IsFinish);
	}

	private bool CheckCollision(LayerMask collisionLayerMask)
	{
		Vector2 playerCenter = _rigidbody.position + _rigidbody.centerOfMass;
		Vector2 raycastOrigin = playerCenter + Vector2.right * (_renderer.bounds.extents.x/2f);
		RaycastHit2D hit = Physics2D.Raycast(raycastOrigin, Vector2.right, _raycastDistance, collisionLayerMask);
		Debug.DrawRay(raycastOrigin, Vector2.right * _raycastDistance, Color.red);
		return hit.collider != null;
	}


	private bool CheckGround()
	{
		Vector2 playerCenter = _rigidbody.position + new Vector2(0.0f, _circleOffsetY);
		Collider2D[] colliders = Physics2D.OverlapCircleAll(playerCenter, _circleRadius, _obstacleMask);
		foreach (Collider2D collider in colliders)
		{
			if (collider != null && collider.gameObject != gameObject)
			{
				return true;
			}
		}

		return false;
	}

	private void OnDrawGizmos()
	{
		Vector3 playerCenter = transform.position + new Vector3(0.0f, _circleOffsetY, 0.0f);
		Gizmos.color = Color.red;
		Gizmos.DrawWireSphere(playerCenter, _circleRadius);
	}

}
