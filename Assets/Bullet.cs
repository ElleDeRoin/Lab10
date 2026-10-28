using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 12f;
    public float maxLifeTime = 3f;
    public string targetTag = "Target";

    public static event Action<GameObject> OnHitTarget;

    private float _timer;

    void OnEnable() {
        _timer = 0f;
    }

    void Update()
    {
        transform.Translate(Vector3.up * speed * Time.deltaTime);

        _timer += Time.deltaTime;
        if (_timer >= maxLifeTime) {
            ReturnToPool();
        }
    }

    void OnTriggerEnter2D(Collider2D other) {
        Debug.Log("Bullet hit: " + other.gameObject.name + " with tag: " + other.tag);
        if (other.CompareTag(targetTag)) {
            OnHitTarget?.Invoke(other.gameObject);
            ReturnToPool();
        }
    }

    private void ReturnToPool() {
        gameObject.SetActive(false);
    }
}
