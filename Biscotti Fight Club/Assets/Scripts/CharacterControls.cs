using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterControls : MonoBehaviour
{
	[HideInInspector]
	public bool hasItem = false;
	[SerializeField] private Transform itemLocation;
	[SerializeField] private GameObject spawn;
	[SerializeField] private LayerMask layerMask;
	[SerializeField] private float throwForce;
	[SerializeField] private float fireInterval;

	private Yeet itemToBeYeeted;
	private Camera camera;
	private float timepassed;

	private void Start()
	{
		camera = GetComponentInChildren<Camera>();
		SpawnItem();
	}

	private void Update()
	{
		Yeet();
	}

	private void Yeet()
	{
		if (Input.GetAxis("Fire1") > 0f)
		{
			if (timepassed - fireInterval > 0f)
			{
				SpawnItem();
				//Throw();
				timepassed = 0f;
			}
		}
		else if (Input.GetAxis("Interact") > 0f)
		{
			RaycastHit hit;
			Physics.Raycast(camera.transform.position, camera.transform.forward, out hit, 100f, layerMask);
			GameObject item = null;
			if (hit.collider) item = hit.collider.gameObject;
			if (item)
			{
				if (item.GetComponent<Yeet>())
					PickUp(item);
			}

		}
		timepassed += Time.deltaTime;
	}

	private void PickUp(GameObject item)
	{
		item.transform.parent = itemLocation;
		hasItem = true;
	}

	private void Throw()
	{
		Rigidbody rb = itemLocation.GetChild(0).GetComponent<Rigidbody>();
		rb.AddForce(itemLocation.parent.forward * throwForce);
		itemToBeYeeted.gameObject.transform.parent = null;
		hasItem = false;
	}

	private void SpawnItem()
	{
		itemToBeYeeted = (Instantiate(spawn, itemLocation.position, itemLocation.parent.transform.localRotation)).GetComponent<Yeet>();
		hasItem = true;
	}
}
