using UnityEngine;

[RequireComponent(typeof(Collider))] // new
public class ObjectSpawner : MonoBehaviour
{
    [SerializeField] private GameObject _objectPrefab;
    [SerializeField] private LayerMask _spawnSurfaceMask;// new

    private Vector3 _spawnCenter;// new
    private Collider _spawnHelper;// new
    private RaycastHit _spawnHit;// new

    private void Awake()
    {
        _spawnHelper = GetComponent<Collider>();// new
    }

    private void Start()
    {
        _spawnCenter = transform.position;// new

        for (int i = 0; i < 10; i++)
        {
            SpawnObject();
        }
    }

    private void SpawnObject() // new
    {
        float xVolume = _spawnHelper.bounds.size.x / 2;// new
        float zVolume = _spawnHelper.bounds.size.z / 2;// new


        Vector3 spawnPosition = new Vector3(Random.Range(_spawnCenter.x - xVolume, _spawnCenter.x + xVolume), _spawnCenter.y
            , Random.Range(_spawnCenter.z - zVolume, _spawnCenter.z + zVolume));// new

        if (Physics.Raycast(spawnPosition, Vector3.down, out _spawnHit, 100, _spawnSurfaceMask))
        {
            Instantiate(_objectPrefab, _spawnHit.point + Vector3.up * .5f, Quaternion.identity);
        }
    }

    //void Start()
    //{
    //    Vector3 spawnPosition = new Vector3();


    //    for (int i = 0; i < 10; i++)
    //    {
    //        Instantiate(_objectPrefab, spawnPosition, Quaternion.identity);

    //        spawnPosition += new Vector3(1.5f, 0, 0);

    //    }
    //}
}
