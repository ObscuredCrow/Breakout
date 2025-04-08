using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private float _moveSpeed = 30;

    [Header("Sounds")]
    [SerializeField] private AudioClip _hitClip;

    private Rigidbody2D _rigidbody;
    private Vector3 _startPosition;

    private void Update() {
        ResetPosition();
        Move();
    }

    private void ResetPosition() {
        if (GameController.Instance.State != GameState.Start)  return;

        transform.localPosition = _startPosition;
    }

    private void Move() {
        _rigidbody.simulated = GameController.Instance.State == GameState.Run;
        if (GameController.Instance.State != GameState.Run) return;

        _rigidbody.linearVelocity = Vector3.zero;

        if (Input.GetKey(KeyCode.LeftArrow))
			transform.position += transform.right * -_moveSpeed * Time.deltaTime;

		if (Input.GetKey(KeyCode.RightArrow))
			transform.position += transform.right * _moveSpeed * Time.deltaTime;
	}

	private void OnCollisionEnter2D(Collision2D collision) {
		if (collision.collider.tag != "Ball") return;

        GameController.Instance.PlayBump();
	}

	private void Awake() { 
        _rigidbody = GetComponent<Rigidbody2D>(); 
        _startPosition = transform.position;
    }
}