using UnityEngine;

public class PlayerBlade : MonoBehaviour
{
    [Header("References"), Space]
    [SerializeField] private Collider bladeTrigger;
    [SerializeField] private TrailRenderer trail;

    [Header("Settings"), Space]
    public float sliceForce = 5f;
    [SerializeField] private Vector2 movementDeltaRange;

    // Properties.
    public Vector2 Direction { get; private set; }

    // Private fields.
    private Camera _mainCamera;

    private void Awake()
    {
        _mainCamera = Camera.main;
    }

    private void Update()
    {
        Vector2 mousePos = _mainCamera.ScreenToWorldPoint(Input.mousePosition);

        SlicingFruit(Input.GetMouseButton(0), mousePos);

        transform.position = Vector2.MoveTowards(transform.position, mousePos, movementDeltaRange.y);
    }

    private void SlicingFruit(bool continuous, Vector2 mousePos)
    {   
        if (continuous)
        {
            Direction = (Vector2)transform.position - mousePos;

            float speed = Direction.magnitude / Time.deltaTime;

            bladeTrigger.enabled = speed > movementDeltaRange.x;
            trail.enabled = continuous;
        }
        else
        {
            bladeTrigger.enabled = false;
            trail.enabled = false;
            trail.Clear();
        }
    }
}