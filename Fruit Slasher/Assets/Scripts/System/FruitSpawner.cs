using UnityEngine;

public class FruitSpawner : MonoBehaviour
{
	[Header("References"), Space]
	[SerializeField] private GameObject[] fruitPrefabs;
	[SerializeField] private Collider spawnArea;

	[Header("Fruit Settings"), Space]
	[SerializeField] private Vector2 spawnIntervalRange;
	[SerializeField] private Vector2 startAngleRange;
	[SerializeField] private Vector2 startForceRange;
	[SerializeField] private float maxLifeTime;

	[Header("Bomb Settings"), Space]
	[SerializeField] private GameObject bombPrefab;
	[SerializeField, Range(0f, 1f)] private float bombChance;

	// Private fields.
	private float _interval;
	private Bounds _bounds;

	private void Start()
	{
		_interval = 2f;
		_bounds = spawnArea.bounds;
	}

	private void Update()
	{
		SpawnFruit();
	}

	private void SpawnFruit()
	{
		_interval -= Time.deltaTime;

		if (_interval <= 0f)
		{
			GameObject prefab = fruitPrefabs[Random.Range(0, fruitPrefabs.Length)];

			if (Random.value < bombChance)
				prefab = bombPrefab;
			
			Vector3 position = new Vector3
			(
				Random.Range(_bounds.min.x, _bounds.max.x),
				Random.Range(_bounds.min.y, _bounds.max.y),
				Random.Range(_bounds.min.z, _bounds.max.z)
			);
			Quaternion rotation = Random.rotation;

			Vector3 forceDirection = Quaternion.Euler(0f, 0f, Random.Range(startAngleRange.x, startAngleRange.y)) * Vector3.up;
			Vector3 force = Random.Range(startForceRange.x, startForceRange.y) * forceDirection;

			GameObject fruit = Instantiate(prefab, position, rotation);
			fruit.GetComponent<Rigidbody>().AddForce(force, ForceMode.Impulse);
			Destroy(fruit, maxLifeTime);

			_interval = Random.Range(spawnIntervalRange.x, spawnIntervalRange.y);
		}
	}
}