using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class FruitFactory : MonoBehaviour {
    [SerializeField] private Fruit[] fruits;
    [SerializeField] private Camera cam;
    private void Update() {
        if (Input.GetButtonDown("Fire1")) {
            var mousePos = Input.mousePosition;
            var point = cam.ScreenToWorldPoint(mousePos);
            InstantiateFruit(GetRandomFruit(), point);
        }
    }

    private Fruit GetRandomFruit() {
        var index = Random.Range(0, fruits.Length);
        return fruits[index];
    }

    private void InstantiateFruit(Fruit fruit, Vector2 position) {
        Instantiate(fruit, position, Quaternion.identity);
    }
}