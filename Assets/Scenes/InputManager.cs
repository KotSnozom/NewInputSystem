using System.Collections;
using System.Collections.Generic;
using UnityEditor.iOS.Xcode;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    public static InputManager Instance;
    private InputSystem _inputSystem;

    public static UnityAction OnStart;
    public static UnityAction OnStop;
    public static UnityAction<Vector2> OnPosition;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else Destroy(gameObject);
        _inputSystem = new InputSystem();
    }
    private void OnEnable()
    {
        _inputSystem.Enable();
    }
    private void OnDisable()
    {
        _inputSystem.Disable();
    }
    private void Start()
    {
        _inputSystem.Mouse.MouseTapLeft.started += ctx => StartMove();
        _inputSystem.Mouse.MouseTapLeft.canceled += ctx => EndMove();

        _inputSystem.Mouse.MousePosition.performed += ctx => Move();
    }
    private void StartMove()
    {
        Debug.Log("Start");
        if (OnStart != null) OnStart();
    }
    private void EndMove()
    {
        Debug.Log("Stop");
        if (OnStop != null) OnStop();
    }
    private void Move()
    {
        if (OnPosition != null) OnPosition(_inputSystem.Mouse.MousePosition.ReadValue<Vector2>());
    }
}
