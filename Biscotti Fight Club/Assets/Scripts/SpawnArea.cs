using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

[System.Serializable]
public class SpawnType
{
    [Tooltip("The prefab that will be instantiated.")]
    public GameObject prefab;

    [Tooltip("The maximum amount to spawn at one time.")] 
    public int max = 1;

    [Tooltip("The minimum amount to spawn at one time.")]
    public int min = 1;
}

public enum SpawnMethod
{
    OnceAtStart,
    Interval,
    MouseClick
}

public class SpawnArea : MonoBehaviour
{
    [SerializeField]
    private SpawnType[] spawnTypes;
    [SerializeField]
    private float MaxInstancesAllowed = float.MaxValue;
    [SerializeField]
    private bool SetSpawnHeight = false;
    [SerializeField] [DrawIf("SetSpawnHeight", true)]
    private float yPos = 0f;
    [SerializeField] 
    public SpawnMethod spawnMethod = SpawnMethod.Interval;
    [SerializeField]
    [DrawIf("spawnMethod", SpawnMethod.Interval)]
    public float spawnRate = 3f;
    [SerializeField]
    [DrawIf("spawnMethod", SpawnMethod.MouseClick)]
    public float fireRate = 2f;
    [SerializeField]
    [DrawIf("spawnMethod", SpawnMethod.MouseClick)]
    public float fireThreshold = 0.12f;

    private float xMin;
    private float xMax;
    private float zMin;
    private float zMax;
    private float timePassed;
    private bool fired = false;
    private List<Transform> instances;

    // Start is called before the first frame update
    void Start()
    {
        //xMin = transform.position.x - (transform.localScale.x * 0.5f);
        //xMax = xMin + transform.localScale.x;

        //zMin = transform.position.z - (transform.localScale.z * 0.5f);
        //zMax = zMin + transform.localScale.z;

        if (spawnMethod == SpawnMethod.OnceAtStart) 
            Spawn();
        if(MaxInstancesAllowed != float.MaxValue)
            instances = new List<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        switch (spawnMethod)
        {
            case SpawnMethod.Interval:
                IntervalSpawn();
                break;
            case SpawnMethod.MouseClick:
                if (Input.GetAxis("Fire1") > 0f && !fired)
                {
                    Spawn();
                    fired = true;
                }
                ResetFire();
                break;
            case SpawnMethod.OnceAtStart:
            default:
                break;
        }
    }

    private void ResetFire()
    {
        if (timePassed - fireRate > 0f && Input.GetAxis("Fire1") < fireThreshold)
        {
            fired = false;
            timePassed = 0f;
        }
        timePassed += Time.unscaledDeltaTime;
    }

    private void IntervalSpawn()
    {
        if (timePassed - spawnRate > 0f)
        {
            Spawn();
            timePassed = 0f;
        }
        timePassed += Time.unscaledDeltaTime;
    }

    public void Spawn()
    {
        xMin = transform.position.x - (transform.localScale.x * 0.5f);
        xMax = xMin + transform.localScale.x;

        zMin = transform.position.z - (transform.localScale.z * 0.5f);
        zMax = zMin + transform.localScale.z;

        foreach ( SpawnType spawn in spawnTypes)
        {
            int amount = Random.Range(spawn.min, spawn.max);

            if (!SetSpawnHeight)
            {
                float prefabHeight = spawn.prefab.GetComponent<Collider>().bounds.extents.y;
                yPos = transform.position.y + prefabHeight;
            }
            for (int i = 0; i < amount; i++)
            {
                if(instances.Count > MaxInstancesAllowed)
                {
                    Transform killMe = instances[0];
                    instances.RemoveAt(0);
                    Destroy(killMe.gameObject);
                }
                float xPos = Random.Range(xMin, xMax);
                float zPos = Random.Range(zMin, zMax);
                Vector3 startPos = new Vector3(xPos, yPos, zPos);
                instances.Add(Instantiate(spawn.prefab, startPos, Quaternion.identity).transform);
            }
        }
    }
}
