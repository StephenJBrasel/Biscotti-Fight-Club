using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameCanvasManagement : MonoBehaviour
{
	public bool DisplayCanvas = true;
	[SerializeField] private Canvas OverlayCanvas;
	[SerializeField] private Text ItemInteract;
	[SerializeField] private LayerMask layerMask;
	[SerializeField] private CharacterControls charControls;
	[SerializeField] private Camera camera;

	[SerializeField] private float RayLength = 5f;


	// Start is called before the first frame update
	void Start()
	{
		//camera = GetComponent<Camera>();
		UpdateItemInteractText();
	}

	// Update is called once per frame
	void Update()
	{
		//if(InputChange) UpdateItemInteractionText();

		if (Physics.Raycast(camera.transform.position, 
				camera.transform.forward, 
				layerMask) &&
			!charControls.hasItem)
		{
			OverlayCanvas.enabled = true;
		}
		else
		{
			OverlayCanvas.enabled = false;
		}
	}

	private void OnDrawGizmos()
	{
		//Gizmos.DrawRay(camera.transform.position, camera.transform.forward * RayLength);
	}

	private void UpdateItemInteractText()
	{
		string InteractButton = "E";
		ItemInteract.text = $"Press\n{InteractButton}";
	}
}
