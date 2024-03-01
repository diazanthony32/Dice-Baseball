using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceDetector : MonoBehaviour
{
    private void OnTriggerStay(Collider other)
    {
        if (other.GetComponentInParent<Die>() != null)
        {
            if (other.attachedRigidbody.velocity == Vector3.zero)
            {
                other.GetComponentInParent<Die>().landedValue = int.Parse(other.name);
            }
        }
    }

}
