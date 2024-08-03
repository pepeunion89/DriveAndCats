using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{

    public static PlayerController Instance { get; private set; }
    
    [SerializeField] InputManager inputManager;


    private void Awake() {

        Instance = this;

        inputManager = new InputManager();
        inputManager.Enable();

    }
    
    public Vector2 GetMovementNormalized() {

        Vector2 direction = inputManager.Player.Move.ReadValue<Vector2>();

        direction = direction.normalized;

        return direction;


    }

}
