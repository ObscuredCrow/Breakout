using UnityEngine;

public class LoseCollision : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.collider.tag != "Ball") return;

        GameController.Instance.Life--;
        if(GameController.Instance.Life <= 0)
            GameController.Instance.StopGame();
    }
}
