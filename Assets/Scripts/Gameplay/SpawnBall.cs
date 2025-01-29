using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnBall : MonoBehaviour
{
    [SerializeField] private GameObject ball;
    [SerializeField] private GameObject spawnPoint;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<PlayerController>())
        {
            Instantiate(ball, spawnPoint.transform.position, Quaternion.identity);
        }
    }
}
