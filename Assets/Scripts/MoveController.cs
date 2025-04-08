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
        _rigidbody.linearVelocity = Vector3.zero;
        _isMoving = false;
	}

    private void Move() {
        if (GameController.Instance.State != GameState.Run) return;

		if (_rigidbody.linearVelocity.magnitude > _maxVelocity)
			_rigidbody.linearVelocity = _rigidbody.linearVelocity.normalized * _maxVelocity;

		if (_rigidbody.linearVelocity.magnitude < CurrentSpeed)
            _rigidbody.linearVelocity = _rigidbody.linearVelocity.normalized * CurrentSpeed;

        if (_rigidbody.linearVelocity.x == 0 || _rigidbody.linearVelocity.y == 0)
            _rigidbody.linearVelocity = new Vector2(Random.Range(-1, 1), Random.Range(0.5f, 1)) * _rigidbody.linearVelocity.magnitude;

        if(_rigidbody.linearVelocity == Vector2.zero)
			_rigidbody.linearVelocity = ((transform.up + transform.right) * 0.5f) * CurrentSpeed;

		if (!_isMoving) {
			_rigidbody.linearVelocity = transform.up * CurrentSpeed;
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
