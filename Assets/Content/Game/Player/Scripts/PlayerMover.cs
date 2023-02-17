using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class PlayerMover : MonoBehaviour
{
    [SerializeField] private float _speed = 150f;
    [SerializeField] private float _rotationSpeed = 2f;
    
    private NavMeshAgent _agent;
    private Animator _animator;
    private string _animatorMoveParam = "AnimationPar";
    private DeviceType _playerDevice;

    void Start()
    {
        _agent = gameObject.GetComponentInChildren<NavMeshAgent>();
        _animator = GetComponentInChildren<Animator>();
        _playerDevice = SystemInfo.deviceType;
    }

    void Update()
    {
        var input = GetInput();
        Move(input);
        Rotate(input);
    }
    
    private Vector3 GetInput()
    {
        var horizontal = 0.0f;
        var vertical = 0.0f;
        
        switch (_playerDevice)
        {
            case DeviceType.Desktop:
                horizontal = Input.GetAxis("Horizontal");
                vertical = Input.GetAxis("Vertical");
                break;
            case DeviceType.Handheld:
                horizontal = UltimateJoystick.GetHorizontalAxis("Movement");
                vertical = UltimateJoystick.GetVerticalAxis("Movement");
                break;
        }

        var input = new Vector3(horizontal, 0, vertical);
        return input;
    }
    
    private void Move(Vector3 input)
    {
        var moveDirection = new Vector3(input.x, 0, input.z);
        _animator.SetInteger(_animatorMoveParam, input.magnitude > 0 ? 1 : 0);

        MoveTowardTarget(moveDirection);
    }

    private void MoveTowardTarget(Vector3 targetVector)
    {
        var speed = _speed * Time.deltaTime;
        _agent.Move(targetVector.normalized * (speed * Time.deltaTime));
    }

    private void Rotate(Vector3 input)
    {
        var targetVector = new Vector3(input.x, 0, input.z);
        RotateTowardMovementVector(targetVector);
    }

    private void RotateTowardMovementVector(Vector3 movementDirection)
    {
        if (movementDirection.magnitude == 0)
        {
            return;
        }
        
        var rotation = Quaternion.LookRotation(movementDirection);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, _rotationSpeed);
    }
}
