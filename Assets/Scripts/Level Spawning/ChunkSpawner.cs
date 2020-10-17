using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class ChunkSpawner : MonoBehaviour
{
    [SerializeField] private Chunk _chunk = null;
    [SerializeField] private BlockedHighwayChunk _blockedHighwayChunk = null;
    [SerializeField] private float _blockedChunkSpawnPercent = .5f;

    [SerializeField] private ObstaclePool _obstaclePool;
    [SerializeField] private HighwayPool _highwayPool;
    [SerializeField] private ParticlePool _particlePool;

    private List<Chunk> _chunks = new List<Chunk>();
    private int _chunkNumber = 0;
    public float _chunkSize { get; private set; } = 500f;

    private BoxCollider _chunkSpawnCheckpoint;


    private void Start()
    {      
        if (_obstaclePool == null) _obstaclePool = FindObjectOfType<ObstaclePool>();
        if (_highwayPool == null) _highwayPool = FindObjectOfType<HighwayPool>();
        if (_particlePool == null) _particlePool = FindObjectOfType<ParticlePool>();


        _chunkSize = (int)(_chunkSize / _highwayPool.HighwayPieceLength) * _highwayPool.HighwayPieceLength;


        _chunkSpawnCheckpoint = GetComponent<BoxCollider>();
        _chunkSpawnCheckpoint.isTrigger = true;
        _chunkSpawnCheckpoint.center = new Vector3(0, 0, _chunkSize);

        CreateChunk(1, true);
        CreateChunk(2, false);
    }

    public void CreateChunk(int num, bool firstChunk)
    {
        for (int i = 0; i < num; i++)
        {
            Vector3 position = new Vector3(0, 0, _chunkSize / 2 + _chunkSize * _chunkNumber);
            Chunk newChunk = Instantiate(ChunkToSpawn(firstChunk), position, Quaternion.identity);
            newChunk.SpawnChunk(_chunkSize, _obstaclePool, _highwayPool, _particlePool, firstChunk);
            _chunks.Add(newChunk);
            _chunkNumber++;
        }
    }

    private Chunk ChunkToSpawn(bool firstChunk)
    {
        Chunk chunkToSpawn;

        if (!firstChunk)
            chunkToSpawn = Random.Range(0f, 1f) >= _blockedChunkSpawnPercent ? _chunk : _blockedHighwayChunk;
        else
            chunkToSpawn = _chunk;

        return chunkToSpawn;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PlayerController>())
        {
            CheckForDespawningChunks(other);
            CreateChunk(1, false);
            _chunkSpawnCheckpoint.center = new Vector3(0, 0, _chunkSpawnCheckpoint.center.z + _chunkSize);
        }
    }

    private void CheckForDespawningChunks(Collider other)
    {
        for (int i = 0; i < _chunks.Count; i++)
        {
            float distance = Vector3.Distance(_chunks[i].transform.position, other.transform.position);
            if (distance > _chunkSize * 2)
            {
                _chunks[i].DespawnChunk();
                _chunks.Remove(_chunks[i]);
            }
        }
    }
}
