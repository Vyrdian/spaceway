using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public PositionYAxis _yPosition { get; private set; }
    public float MoveSpeedForward { get; private set; } = 50f;

    private float _yPosChange = 3f;
    private float _maxXPositionMovement = 3f;


    public float PositionChangeCooldownTime { get; private set; } = .15f;
    private float _currentPositionChangeCooldown;


    private float _speedGainPerSecond = .3f;

    private float _moveSpeedX = 12f;

    [SerializeField] private GameObject _deathParticles = null;
    [SerializeField] private GameObject _shipGFX = null;
    private bool _isAlive = true;

    public delegate void PlayerDeath();
    public static event PlayerDeath OnPlayerDeath;


    void Start() => _yPosition = PositionYAxis.Down;

    void Update()
    {
        if (!_isAlive) return;

        MoveForward();

        if (_currentPositionChangeCooldown > 0)
            _currentPositionChangeCooldown -= Time.deltaTime;
        else
            ProcessInput();
    }

    private void MoveForward()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + MoveSpeedForward * Time.deltaTime);
        MoveSpeedForward = Mathf.Clamp(MoveSpeedForward + _speedGainPerSecond * Time.deltaTime, MoveSpeedForward, MoveSpeedForward * 3);
    }

    private void ProcessInput()
    {
        if (Input.GetKey(KeyCode.A) && transform.position.x > -_maxXPositionMovement)
            transform.position = new Vector3(transform.position.x - _moveSpeedX * Time.deltaTime, transform.position.y, transform.position.z);

        if (Input.GetKey(KeyCode.D) && transform.position.x < _maxXPositionMovement)
            transform.position = new Vector3(transform.position.x + _moveSpeedX * Time.deltaTime, transform.position.y, transform.position.z);

        if (Input.GetKeyDown(KeyCode.W) && _yPosition != PositionYAxis.Up)
        {
            _yPosition = PositionYAxis.Up;
            transform.position = new Vector3(transform.position.x, transform.position.y + _yPosChange, transform.position.z);
            _currentPositionChangeCooldown = PositionChangeCooldownTime;
        }
        if (Input.GetKeyDown(KeyCode.S) && _yPosition != PositionYAxis.Down)
        {
            _yPosition = PositionYAxis.Down;
            transform.position = new Vector3(transform.position.x, transform.position.y - _yPosChange, transform.position.z);
            _currentPositionChangeCooldown = PositionChangeCooldownTime;
        }
    }

    public void Collided()
    {
        OnPlayerDeath?.Invoke();

        _isAlive = false;
        _shipGFX.SetActive(false);

        if (_deathParticles)
            Instantiate(_deathParticles, transform.position, Quaternion.identity);
    }
}

public enum PositionYAxis { Down, Up}
