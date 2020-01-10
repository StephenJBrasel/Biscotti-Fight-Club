using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfiniteMoveTerrain : MonoBehaviour
{
	[SerializeField] private Transform[] terrains;
	[SerializeField] private Vector3 direction = Vector3.left;
	[SerializeField] private float speed = 3f;

	private List<Transform> TerrainList;
	private Transform lastOut;
	private Vector3 resetPosition;
	private float distFromStart;
	// Start is called before the first frame update
	void Start()
	{
		if (terrains != null)
		{
			lastOut = terrains[terrains.Length - 1];
			resetPosition = terrains[terrains.Length - 1].position;
			distFromStart = Vector3.Distance(terrains[0].position, terrains[1].position);
		}
		TerrainList = new List<Transform>(terrains);
	}

	// Update is called once per frame
	void Update()
	{
		if (Vector3.Distance(lastOut.position, resetPosition) >= distFromStart)
		{
			TerrainList[0].position = resetPosition + direction * Time.deltaTime * speed;
			Transform temp = TerrainList[0];
			TerrainList.RemoveAt(0);
			TerrainList.Add(temp);
			lastOut = temp;
		}
		for (int i = 0; i < TerrainList.Count; i++)
		{
			TerrainList[i].position += direction * Time.deltaTime * speed;
		}
	}

	//private void OnTriggerEnter(Collider other)
	//{
	//	for (int i = 0; i < TerrainList.Count; i++)
	//	{
	//		if (other.gameObject.transform == (TerrainList[i]))
	//		{

	//		}
	//	}
	//}
}
