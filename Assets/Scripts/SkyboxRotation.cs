using UnityEngine;

public class SkyboxRotation : MonoBehaviour
{
    private Vector3 _rotationSpeed;
    private float _rotationSpeedMax = 2f;

    void Start()
    {
        _rotationSpeed = new Vector3(RandomFloatBetweenPositiveAndNegative(_rotationSpeedMax), RandomFloatBetweenPositiveAndNegative(_rotationSpeedMax), RandomFloatBetweenPositiveAndNegative(_rotationSpeedMax));
    }

    void LateUpdate()
    {
        transform.rotation = Quaternion.Euler(transform.rotation.x + _rotationSpeed.x * Time.time, transform.rotation.y + _rotationSpeed.y * Time.time, transform.rotation.z + _rotationSpeed.z * Time.time);
    }

    private float RandomFloatBetweenPositiveAndNegative(float value) => Random.Range(-value, value);
}
