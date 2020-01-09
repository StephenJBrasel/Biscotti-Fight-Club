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

public class SpawnArea : MonoBehaviour
{
    [SerializeField] private SpawnType[] spawnTypes;
    [SerializeField] private float yPos = 0f;
    [SerializeField] private float spawnRate = 3f;

    private Rect AreaToSpawnIn;
    private float xMin;
    private float xMax;
    private float zMin;
    private float zMax;
    private float timePassed;

    // Start is called before the first frame update
    void Start()
    {
        xMin = transform.position.x - (transform.localScale.x * 0.5f);
        xMax = xMin + transform.localScale.x;

        zMin = transform.position.z - (transform.localScale.z * 0.5f);
        zMax = zMin + transform.localScale.z;
    }

    // Update is called once per frame
    void Update()
    {
        if(timePassed - spawnRate > 0f)
        {
            Spawn();
            timePassed = 0f;
        }
        timePassed += Time.unscaledDeltaTime;
    }

    public void Spawn()
    {
        foreach( SpawnType spawn in spawnTypes)
        {
            int amount = Random.Range(spawn.min, spawn.max);
            for (int i = 0; i < amount; i++)
            {
                float xPos = Random.Range(xMin, xMax);
                float zPos = Random.Range(zMin, zMax);
                Vector3 startPos = new Vector3(xPos, yPos, zPos);
                Instantiate(spawn.prefab, startPos, Quaternion.identity);
            }
        }
    }
}
