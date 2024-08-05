using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Inventory 
{

    private int catCounter;
    public event Action<int> UpdateCatAmount;
    
    public Inventory() {

        catCounter = 0;

    }

    public void AddCat() {

        catCounter++;

        UpdateCatAmount.Invoke(catCounter);

    }

    public int GetCatAmount() {

        return catCounter;

    }

}
