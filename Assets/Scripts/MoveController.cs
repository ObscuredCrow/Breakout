using UnityEngine;

public class MoveController : MonoBehaviour
{
	public static MoveController Instance;

	[SerializeField] private float _startSpeed = 2f;
    [SerializeField] private float _maxVelocity = 20f;

    [HideInInspector] public float CurrentSpeed;

    private Vector3 _startPosition;
    private Rigidbody2D _rigidbody;
    private bool _isMoving = false;

    public void FixedUpdate() {
        ResetPosition();
        Move();
    }

    private void ResetPosition() {
        if (GameController.Instance.State == GameState.Run) return;

        transform.localPosition = _startPosition;
        CurrentSpeed = _startSpeed;
        _rigidbody.velocity = Vector3.zero;
        _isMoving = false;
	}

    private void Move() {
        if (GameController.Instance.State != GameState.Run) return;

		if (_rigidbody.velocity.magnitude > _maxVelocity)
			_rigidbody.velocity = _rigidbody.velocity.normalized * _maxVelocity;

		if (_rigidbody.velocity.magnitude < CurrentSpeed)
            _rigidbody.velocity = _rigidbody.velocity.normalized * CurrentSpeed;

        if (_rigidbody.velocity.x == 0 || _rigidbody.velocity.y == 0)
            _rigidbody.velocity = new Vector2(Random.Range(-1, 1), Random.Range(0.5f, 1)) * _rigidbody.velocity.magnitude;

        if(_rigidbody.velocity == Vector2.zero)
			_rigidbody.velocity = ((transform.up + transform.right) * 0.5f) * CurrentSpeed;

		if (!_isMoving) {
			_rigidbody.velocity = transform.up * CurrentSpeed;
			_isMoving = true;
		}
	}

    private void Awake() {
		Instance ??= this;
		_rigidbody = GetComponent<Rigidbody2D>();
        _startPosition = transform.localPosition;
        CurrentSpeed = _startSpeed;
    }
}
