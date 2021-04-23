using System.Collections.Generic;
using UnityEngine;

public class Chunk : MonoBehaviour
{
    protected int _maxObstaclesPerSpawnLocation = 5;
    protected int _minObstaclesPerSpawnLocation = 3;
    private float _obstacleSpawnDistanceMin = 80f;
    private float _obstacleSpawnDistanceMax = 120f;

    private int[] _obstacleXPosValues = new int[] { -3, 0, 3 };
    protected int[] _obstacleYPosValues = new int[] { 0, 3 };

    private float _chunkLength;
    public Vector3 ChunkCenter { get; set; }

    private List<Obstacle> _obstacles = new List<Obstacle>();
    private ObstaclePool _obstaclePool;
    List<Vector3> obstacleSpawnPoints = new List<Vector3>();

    private List<HighwayPiece> _highwayPieces = new List<HighwayPiece>();
    private HighwayPool _highwayPool;

    protected ParticlePool _particlePool;

    public void SpawnChunk(float chunkLength, ObstaclePool obstaclePool, HighwayPool highwayPool, ParticlePool particlePool, bool firstChunk)
    {
        
        _chunkLength = chunkLength;
        ChunkCenter = transform.position;
        _obstaclePool = obstaclePool;
        _particlePool = particlePool;
        _highwayPool = highwayPool;

        SpawnHighway();
        SetObstacleSpawnPoints(firstChunk);       
    }

    private void SetObstacleSpawnPoints(bool firstChunk)
    {
        float spawnStartPoint;
        if (firstChunk) spawnStartPoint = ChunkCenter.z + _chunkLength / 4;
        else spawnStartPoint = ChunkCenter.z - _chunkLength / 2 + _obstacleSpawnDistanceMin;

        for (float f = spawnStartPoint; f < ChunkCenter.z + _chunkLength / 2;)
        {
            obstacleSpawnPoints.Clear();
            int obstaclesToSpawn = Random.Range(_minObstaclesPerSpawnLocation, _maxObstaclesPerSpawnLocation);

            for (int i = 0; i <= obstaclesToSpawn; i++)
            {
                SpawnObstacles(f);
            }
            f += Random.Range(_obstacleSpawnDistanceMin, _obstacleSpawnDistanceMax);
        }
    }

    private void SpawnObstacles(float spawnPoint)
    {
        Obstacle obstacle = _obstaclePool.GetObstacle();
        Vector3 newObstaclePosition = new Vector3(_obstacleXPosValues[Random.Range(0, _obstacleXPosValues.Length)], _obstacleYPosValues[Random.Range(0, _obstacleYPosValues.Length)], spawnPoint);
        while (obstacleSpawnPoints.Contains(newObstaclePosition))
        {
            newObstaclePosition = new Vector3(_obstacleXPosValues[Random.Range(0, _obstacleXPosValues.Length)], _obstacleYPosValues[Random.Range(0, _obstacleYPosValues.Length)], spawnPoint);
        }
        obstacleSpawnPoints.Add(newObstaclePosition);
        obstacle.transform.position = newObstaclePosition;
        obstacle.gameObject.SetActive(true);
        _obstacles.Add(obstacle);
    }

    private void SpawnHighway()
    {
        for (float f = ChunkCenter.z - _chunkLength / 2; f + Mathf.Epsilon < ChunkCenter.z + _chunkLength / 2 - _highwayPool.HighwayPieceLength / 2;)
        {
            HighwayPiece highwayPiece = _highwayPool.GetHighwayPiece();
            highwayPiece.transform.position = new Vector3(0, 0, f);
            highwayPiece.gameObject.SetActive(true);
            _highwayPieces.Add(highwayPiece);

            f += _highwayPool.HighwayPieceLength;
        }
    }

    public virtual void DespawnChunk()
    {
        foreach(HighwayPiece highwayPiece in _highwayPieces)
            _highwayPool.ReturnToQueue(highwayPiece);

        foreach(Obstacle obstacle in _obstacles)
            _obstaclePool.ReturnToQueue(obstacle);

        Destroy(this.gameObject);
    }
}
