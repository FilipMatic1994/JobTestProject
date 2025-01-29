using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Rigidbody rb;
    [SerializeField] private float bulletSpeed = 20f;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.velocity = transform.forward * bulletSpeed;

        Invoke(nameof(EndBulletLife), 5f);
    }

    private void EndBulletLife()
    {
        Destroy(this.gameObject);
    }
}
