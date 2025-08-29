using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Camera))]
public class RTSCamera : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float _moveSpeed = 15f;
    [SerializeField] private float _acceleration = 2f;
    [SerializeField] private Vector2 boundsMin = new Vector2(-50, -50);
    [SerializeField] private Vector2 boundsMax = new Vector2(50, 50);

    [Header("Zoom Settings")]
    [SerializeField] private float _zoomSpeed = 3f;
    [SerializeField] private float _maxCameraZoom = 25f;
    [SerializeField] private float _standartCameraZoom = 60f;
    [SerializeField] private float zoomSmoothing = 8f;

    private Camera _camera;
    private Vector2 _moveInput;
    private float _zoomInput;
    private Vector3 _currentVelocity;
    private float _targetFOV;

    private void Awake()
    {
        _camera = GetComponent<Camera>();
        _targetFOV = _camera.fieldOfView;
    }

    private void Update()
    {
        HandleMovement();
        HandleZoom();
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        _moveInput = context.ReadValue<Vector2>();
    }

    public void OnZoom(InputAction.CallbackContext context)
    {
        _zoomInput = context.ReadValue<float>();
    }

    private void HandleMovement()
    {
        Vector3 targetDirection = new Vector3(_moveInput.x, 0, _moveInput.y);
        Vector3 moveOffset = targetDirection * (_moveSpeed * (_targetFOV / _standartCameraZoom)) * Time.deltaTime;

        transform.position = Vector3.SmoothDamp(
            transform.position,
            ClampPosition(transform.position + moveOffset),
            ref _currentVelocity,
            _acceleration * Time.deltaTime
        );
    }

    private void HandleZoom()
    {
        if (_zoomInput != 0)
        {
            _targetFOV -= _zoomInput * _zoomSpeed;
            _targetFOV = Mathf.Clamp(_targetFOV, _maxCameraZoom, _standartCameraZoom);
        }

        _camera.fieldOfView = Mathf.Lerp(
            _camera.fieldOfView,
            _targetFOV,
            zoomSmoothing * Time.deltaTime
        );
    }

    private Vector3 ClampPosition(Vector3 position)
    {
        position.x = Mathf.Clamp(position.x, boundsMin.x, boundsMax.x);
        position.z = Mathf.Clamp(position.z, boundsMin.y, boundsMax.y);
        return position;
    }
}