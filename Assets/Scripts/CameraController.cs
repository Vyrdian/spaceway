using UnityEngine;

public class CameraController : MonoBehaviour
{

    [SerializeField] private float _zOffset = -14f;
    [SerializeField] private float _yOffset = -.5f;
    [SerializeField] private float _xRotationOffset = -10f;

    private float _currentYPosition;
    private float _positionChangeTime;
    private float _cameraPositionChangeTime;
    private float _currentPositionChangeTime = 0f;

    private bool _changingPosition = false;

    private Quaternion _initialRotation;
    private Quaternion _endRotation;

    [SerializeField]
    private PlayerController _target;

    void Start()
    {
        if (_target == null)
            _target = FindObjectOfType<PlayerController>();

        SetInitialPosition();
    }

    private void SetInitialPosition()
    {
        _positionChangeTime = _target.PositionChangeCooldownTime;
        _currentYPosition = _target.transform.position.y + _yOffset;

        transform.position = new Vector3(_target.transform.position.x, _target.transform.position.y + _yOffset, _target.transform.position.z + _zOffset);
        transform.rotation = Quaternion.Euler(_xRotationOffset, 0, 0);
    }

    private void LateUpdate()
    {
        if (_changingPosition)
            ChangeCameraPosition();
        else
            FollowTarget();
    }

    private void FollowTarget()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y, _target.transform.position.z + _zOffset);

        if (_currentYPosition != _target.transform.position.y + _yOffset)
            StartCameraPositionChange();
    }

    private void StartCameraPositionChange()
    {
        _changingPosition = true;
        _yOffset = -_yOffset;
        _xRotationOffset = -_xRotationOffset;
        _currentPositionChangeTime = 0;
        _initialRotation = transform.rotation;
        _endRotation = Quaternion.Euler(_xRotationOffset, 0, 0);
    }

    private void ChangeCameraPosition()
    {
        LerpCameraPosition();
        LerpCameraRotation();

        if (_currentPositionChangeTime >= _positionChangeTime)
        {
            _changingPosition = false;
            ConfirmCameraTransformValues();
        }
    }

    private void ConfirmCameraTransformValues()
    {
        transform.position = new Vector3(transform.position.x, _target.transform.position.y + _yOffset, _target.transform.position.z + _zOffset);
        transform.rotation = _endRotation;
        _currentYPosition = transform.position.y;
    }

    private void LerpCameraRotation()
    {
        transform.rotation = Quaternion.Lerp(_initialRotation, _endRotation, _currentPositionChangeTime / _positionChangeTime);
        _currentPositionChangeTime += Time.deltaTime;
    }

    private void LerpCameraPosition()
    {
        transform.position = new Vector3(transform.position.x,
            Mathf.Lerp(_currentYPosition, _target.transform.position.y + _yOffset, _currentPositionChangeTime / _positionChangeTime), _target.transform.position.z + _zOffset);
    }
}
