using UnityEngine;

public class FloatIdle : MonoBehaviour
{
    public float oscilateSpeed = 5f;
    public float oscilateMagnitude = 1f;

    private Vector3 startPos;

    void Start()
    {
        startPos = transform.position;
    }

    void Update()
    {
        transform.position = startPos + Vector3.up * Mathf.Sin(Time.time * oscilateSpeed) * oscilateMagnitude;
    }
}
