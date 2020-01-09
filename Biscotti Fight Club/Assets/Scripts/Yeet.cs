using UnityEngine;

public class Yeet : MonoBehaviour
{
    [SerializeField] private float force = 3f;

    private Rigidbody rb;
    private Vector3 forwardDirection;

    // Start is called before the first frame update
    void Start()
    {
        //Throw();
        rb = GetComponent<Rigidbody>();
        forwardDirection = transform.forward;
    }

    private void Update()
    {
        rb.velocity = (forwardDirection * force);
        //rb.velocity = Vector3.zero;
        //rb.AddForce(transform.forward * force);
    }

    public void Throw()
    {
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        Vector3 forward = Vector3.forward;
        float distance = float.MaxValue;
        foreach (GameObject player in players)
        {
            if (distance < Vector3.Distance(transform.position, player.transform.position))
            {
                forward = transform.worldToLocalMatrix.MultiplyVector(player.transform.forward);
            }
        }

        rb.AddForce(forward * force);
    }
}
