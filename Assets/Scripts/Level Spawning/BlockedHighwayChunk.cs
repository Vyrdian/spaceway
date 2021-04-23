using UnityEngine;

public class BlockedHighwayChunk : Chunk
{
    private int _nonBlockedYPosition;

    private GameObject _particleField;

    private void Awake()
    {
        SetNewObstacleSpawnValues();
    }

    private void Start()
    {
        SpawnParticleField();
    }

    private void SetNewObstacleSpawnValues()
    {
        _nonBlockedYPosition = Random.Range(0f, 1f) > .5 ? 0 : 3;
        _maxObstaclesPerSpawnLocation = 2;
        _minObstaclesPerSpawnLocation = 1;
        _obstacleYPosValues = new int[] { _nonBlockedYPosition };
    }

    private void SpawnParticleField()
    {
        _particleField = _particlePool.GetParticleSystem();
        _particleField.transform.position = new Vector3(transform.position.x, _nonBlockedYPosition == 0 ? 3 : 0, transform.position.z);
        _particleField.gameObject.SetActive(true);
    }

    public override void DespawnChunk()
    {
        _particlePool.ReturnToQueue(_particleField);
        base.DespawnChunk();
    }

}
