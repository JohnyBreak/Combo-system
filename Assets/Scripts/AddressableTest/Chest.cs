using System.Collections;
using System.Threading.Tasks;
using UnityEngine;

public class Chest : MonoBehaviour, IInteractable
{
    [SerializeField] private Transform _spawnT;

    private Animator _animator;
    private ItemLoader _assetLoader;
    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _assetLoader = new();
    }

    public async void Interact()
    {
        _animator.Play("ChestTilt", 0);
        await Task.Delay(1000);

        var item = await _assetLoader.Load();
        _animator.Play("ChestOpen", 0);

        await Task.Delay(1000);
        StartCoroutine(LerpPosition(item.transform));
    }

    IEnumerator LerpPosition(Transform target, float duration = .3f)
    {
        float time = 0;
        Vector3 startPosition = _spawnT.position;
        while (time < duration)
        {
            target.position = Vector3.Lerp(startPosition, _spawnT.position + _spawnT.up, time / duration);
            time += Time.deltaTime;
            yield return null;
        }
        target.position = _spawnT.position + _spawnT.up;
    }
}
