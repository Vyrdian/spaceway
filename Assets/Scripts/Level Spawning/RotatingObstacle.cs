using UnityEngine;

public class RotatingObstacle : Obstacle
{
    private float _rotationSpeedX;
    private float _rotationSpeedY;
    private float _rotationSpeedZ;

    protected override void Start()
    {
        base.Start();
        SetRandomRotation();
    }

    private void SetRandomRotation()
    {
        _rotationSpeedX = Random.Range(-40f, 40f);
        _rotationSpeedY = Random.Range(-40f, 40f);
        _rotationSpeedZ = Random.Range(-40f, 40f);
    }

    private void Update()
    {
        transform.rotation = Quaternion.Euler(_rotationSpeedX * Time.time + transform.rotation.x, _rotationSpeedY * Time.time + transform.rotation.y, _rotationSpeedZ * Time.time + transform.rotation.z);
    }
}
