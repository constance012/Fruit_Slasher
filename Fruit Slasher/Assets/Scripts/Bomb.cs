using UnityEngine;

public class Bomb : MonoBehaviour
{
    [Header("Explosion Settings"), Space]
    [SerializeField] private float explodeDuration;

    // Private fields.
    private bool _alreadyExploded;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !_alreadyExploded)
        {
            Explode();
        }
    }

    private void Explode()
    {
        CameraShaker.Instance.ShakeCamera();
        AudioManager.Instance.Play("Explode");
        GameManager.Instance.GameOver(explodeDuration);

        _alreadyExploded = true;
    }
}