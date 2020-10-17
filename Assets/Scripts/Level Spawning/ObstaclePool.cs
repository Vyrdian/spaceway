using System.Collections.Generic;
using UnityEngine;

public class ObstaclePool : MonoBehaviour
{
    [SerializeField] private Obstacle[] _obstaclePrefabs = null;
    [SerializeField] private int _initialObstacles = 100;

    private Queue<Obstacle> _obstacles = new Queue<Obstacle>();

    void Awake() => AddObstacle(_initialObstacles);

    private void AddObstacle(int num)
    {
        for(int i = 0; i < num; i++)
        {
            Obstacle obstacle = Instantiate(_obstaclePrefabs[Random.Range(0, _obstaclePrefabs.Length)], transform, true);
            obstacle.transform.SetParent(transform);
            obstacle.gameObject.SetActive(false);
            _obstacles.Enqueue(obstacle);
        }
    }

    public Obstacle GetObstacle()
    {
        if (_obstacles.Count == 0)
                AddObstacle(1);
        return _obstacles.Dequeue();
    }

    public void ReturnToQueue(Obstacle obstacle)
    {
        _obstacles.Enqueue(obstacle);
        obstacle.gameObject.SetActive(false);       
    }
}
