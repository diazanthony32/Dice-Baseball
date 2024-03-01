using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Die : MonoBehaviour
{
    [HideInInspector] public int landedValue;

    [HideInInspector] public bool isMoving;

    [HideInInspector] public Rigidbody rBody;

    private void Awake()
    {
        rBody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        isMoving = !(rBody.velocity == Vector3.zero);
    }
}
