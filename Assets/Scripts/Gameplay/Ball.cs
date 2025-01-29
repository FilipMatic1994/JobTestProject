using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    [SerializeField] private Rigidbody rb;
    [SerializeField] private GameObject obj;
    [SerializeField] private GameObject Effect;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<PlayerController>())
        {
            if(other.gameObject.GetComponent<PlayerController>().GetMovementMagnitude() > 0)
            {
                Vector3 Dir = transform.position - other.gameObject.transform.position;
                Vector3 DirVel = Dir.normalized * 50f;
                rb.velocity = DirVel;
            }
        }
    }

    public void GoalEffect()
    {
        Effect.SetActive(true);
        Destroy(obj, 3f);
    }
}
