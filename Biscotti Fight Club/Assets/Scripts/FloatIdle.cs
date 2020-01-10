using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatIdle : MonoBehaviour
{
    public float oscilateSpeed = 5f;

    private Vector3 startPos;

    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = startPos + Vector3.up * Mathf.Sin(Time.time * oscilateSpeed);
    }
}
