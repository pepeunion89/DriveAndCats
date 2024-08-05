using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour {

    public static Player Instance {  get; private set; }

    [SerializeField] private PlayerController playerController;
    [SerializeField] private PlayerMovement playerMovement;
    [SerializeField] private TransmissionScript transmissionScript;
    [SerializeField] private InventoryUI inventoryUI;
    private Inventory inventory;

    private void Awake() {

        if (Instance == null) {
            Instance = this;
        }

        inventory = new Inventory();
        inventoryUI.SetInventory(inventory);

    }
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

    public Inventory GetInventory() {
    
        return inventory;
    
    }
}