using UnityEngine;

public class Yeet : MonoBehaviour
{
    [SerializeField] private float force = 3f;
    [SerializeField] private float lifeInSeconds = 10f;

    private Rigidbody rb;
    private Vector3 forwardDirection;
    private float timePassed = 0f;

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
        if(timePassed - lifeInSeconds > 0f)
        {
            Destroy(this.gameObject);
        }
        timePassed += Time.deltaTime;
    }

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Destroy(collision.gameObject);
            Destroy(this.gameObject);
        }
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
