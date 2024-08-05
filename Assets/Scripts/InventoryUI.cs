using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{

    [SerializeField] public TextMeshProUGUI catAmountUI;
    private Inventory inventory;
    
    void Start()
    {
        
        

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetInventory(Inventory inventory) {

        this.inventory = inventory;

        inventory.UpdateCatAmount += InventoryUI_UpdateCatAmount;

    }

    public void InventoryUI_UpdateCatAmount(int catAmount) {
        catAmountUI.text = catAmount.ToString();
    }
}
