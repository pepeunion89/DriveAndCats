using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
    [SerializeField] private PlayerController playerController;
    [SerializeField] private PlayerMovement playerMovement;
    [SerializeField] private TransmissionScript transmissionScript;
    void Start() {
        
    }

    void Update() {
        Move();
    }

    void Move() {
        Vector2 direction = playerController.GetMovementNormalized();
        playerMovement.HandleMovement(direction);

        transmissionScript.TransmissionChanged(direction);        
    }

    
}