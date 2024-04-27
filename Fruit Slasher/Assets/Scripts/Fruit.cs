using UnityEngine;

public class Fruit : MonoBehaviour
{
    [Header("References"), Space]
    [SerializeField] private GameObject whole;
    [SerializeField] private GameObject sliced;
    [SerializeField] protected Rigidbody rb;
    [SerializeField] private ParticleSystem juiceSplatter;
    [SerializeField] private GameObject damageTextPrefab;
    
    [Header("Scoring"), Space]
    [SerializeField] private int points;

    // Protected fields.
    protected bool _alreadySliced;
    
    protected virtual void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !_alreadySliced)
        {
            PlayerBlade blade = other.GetComponentInParent<PlayerBlade>();
            Slice(blade.Direction, rb.position, blade.sliceForce);

            GameManager.Instance.UpdateScore(points);

            Vector3 damageTextPos = transform.position + (Vector3.up * (transform.localScale.x + 1f));
            DamageText.Generate(damageTextPrefab, damageTextPos, DamageText.HealingColor, DamageTextStyle.Normal, points.ToString());
        }
    }

    private void Slice(Vector2 direction, Vector2 position, float force)
    {
        whole.SetActive(false);
        sliced.SetActive(true);

        juiceSplatter.Play();
        AudioManager.Instance.PlayWithRandomPitch("Slash", .7f, 1.2f);
        AudioManager.Instance.PlayWithRandomPitch("Splatter", .7f, 1.2f);

        transform.right = direction;
        Rigidbody[] slicedPieces = sliced.GetComponentsInChildren<Rigidbody>();

        foreach (Rigidbody piece in slicedPieces)
        {
            piece.velocity = rb.velocity;
            piece.AddExplosionForce(force, position, transform.localScale.x);
        }

        _alreadySliced = true;
    }
}