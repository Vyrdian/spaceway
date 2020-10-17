using System.Collections.Generic;
using UnityEngine;

public class ParticlePool : MonoBehaviour
{
    [SerializeField] private GameObject _particleSystem = null;
    [SerializeField] private int _initialParticleSystems = 3;

    private Queue<GameObject> _particleSystems = new Queue<GameObject>();

    private void Awake()
    {
        AddParticleSystems(_initialParticleSystems);
    }

    private void AddParticleSystems(int num)
    {
        for (int i = 0; i < num; i++)
        {
            GameObject particleSystem = Instantiate(_particleSystem, transform, true);
            particleSystem.gameObject.SetActive(false);
            _particleSystems.Enqueue(particleSystem);
        }
    }

    public GameObject GetParticleSystem()
    {
        if (_particleSystems.Count == 0)
            AddParticleSystems(1);
        return _particleSystems.Dequeue();
    }

    public void ReturnToQueue(GameObject particleSystem)
    {
        _particleSystems.Enqueue(particleSystem);
        particleSystem.gameObject.SetActive(false);
    }
}