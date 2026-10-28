using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Transform firePoint;
    public float fireCooldown = 0.25f;
    public float _nextFireTime;

    public float moveSpeed = 5f;
    public float maxLeft = -8f;
    public float maxRight = 8f;

    // Stats
    public int points;

    private void Start()
    {
        points = 0;
    }

    private void Update()
    {
        float moveX = Input.GetAxis("Horizontal");
        Vector3 movement = new Vector2(moveX, 0f) * moveSpeed * Time.deltaTime;
        transform.Translate(movement);

        if (transform.position.x < maxLeft)
        {
            transform.position = new Vector3(maxLeft, transform.position.y, transform.position.z);
        }
        else if (transform.position.x > maxRight)
        {
            transform.position = new Vector3(maxRight, transform.position.y, transform.position.z);
        }

        if(Input.GetKey(KeyCode.Space)) {
            TryFire();
        }
    }

    private void TryFire() {
        if (Time.time < _nextFireTime) return;
        _nextFireTime = Time.time + fireCooldown;

        GameObject bullet = BulletPool.SharedInstance.GetPooledObject();
        if (bullet != null)
        {
            bullet.transform.position = firePoint.position;
            bullet.transform.rotation = firePoint.rotation;
            bullet.SetActive(true);
        }
    }


}
