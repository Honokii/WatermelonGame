using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fruit : MonoBehaviour {
    [SerializeField] private int fruitId;

    private bool _isMerging = false;

    private void OnCollisionEnter2D(Collision2D col) {
        var collidedFruit = col.gameObject.GetComponent<Fruit>();
        if (collidedFruit == null) return; //collided with a non-fruit object
        
        HandleFruitCollision(collidedFruit);
    }

    private void HandleFruitCollision(Fruit collidedFruit) {
        if (_isMerging) return; //current fruit is merging with another, 
        if (collidedFruit._isMerging) return; //collided fruit is merging with other fruit
        
        Merge(this, collidedFruit);
    }

    private static void Merge(Fruit a, Fruit b) {
        a._isMerging = true;
        b._isMerging = true;
        
        
    }
}
