using UnityEngine;

public class ScoreTrigger : MonoBehaviour
{
    [SerializeField] private int _breakHits = 1;

	private SpriteRenderer _renderer;
	private int hits = 0;

	private void Update() {
		_renderer.sprite = GameController.Instance.BlockImages[(_breakHits - 1) - hits];
	}

	private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.collider.tag != "Ball") return;

        hits++;
		if (_breakHits <= hits) {
			GameController.Instance.PlayBreak();
			GameController.Instance.AddToScore(_breakHits);
			MoveController.Instance.CurrentSpeed *= 1.005f;
			gameObject.SetActive(false);
			GameController.Instance.WhatBlocksLeft();
		}
		else GameController.Instance.PlayHit();
	}

	public void ResetHits() => hits = 0;

	private void Awake() => _renderer = GetComponent<SpriteRenderer>(); 
}
