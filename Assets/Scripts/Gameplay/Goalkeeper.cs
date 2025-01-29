using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goalkeeper : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<Ball>())
        {
            other.gameObject.GetComponent<Ball>().GoalEffect();
        }
    }
}
