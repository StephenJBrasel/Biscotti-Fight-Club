using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityStandardAssets.CrossPlatformInput;

public class CharacterControls : MonoBehaviour
{
	public float XSensitivity = 2f;
	public float YSensitivity = 2f;
	public float smoothTime = 5f;
	public float MinimumX = -90F;
	public float MaximumX = 90F;
	public bool clampVerticalRotation = true;
	public bool smooth;

	private Camera m_Camera;
	private Quaternion m_CharacterTargetRot;
	private Quaternion m_CameraTargetRot;

	// Start is called before the first frame update
	void Start()
    {
        m_Camera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        RotateView();
    }
    private void RotateView()
    {
        LookRotation(transform, m_Camera.transform);
    }

	public void LookRotation(Transform character, Transform camera)
	{
		float yRot = Input.GetAxis("Mouse X") * XSensitivity;
		float xRot = Input.GetAxis("Mouse Y") * YSensitivity;

		m_CharacterTargetRot *= Quaternion.Euler(0f, yRot, 0f);
		m_CameraTargetRot *= Quaternion.Euler(-xRot, 0f, 0f);

		if (clampVerticalRotation)
			m_CameraTargetRot = ClampRotationAroundXAxis(m_CameraTargetRot);

		if (smooth)
		{
			character.localRotation = Quaternion.Slerp(character.localRotation, m_CharacterTargetRot,
				smoothTime * Time.deltaTime);
			camera.localRotation = Quaternion.Slerp(camera.localRotation, m_CameraTargetRot,
				smoothTime * Time.deltaTime);
		}
		else
		{
			character.localRotation = m_CharacterTargetRot;
			camera.localRotation = m_CameraTargetRot;
		}

		//UpdateCursorLock();
	}
	Quaternion ClampRotationAroundXAxis(Quaternion q)
	{
		q.x /= q.w;
		q.y /= q.w;
		q.z /= q.w;
		q.w = 1.0f;

		float angleX = 2.0f * Mathf.Rad2Deg * Mathf.Atan(q.x);

		angleX = Mathf.Clamp(angleX, MinimumX, MaximumX);

		q.x = Mathf.Tan(0.5f * Mathf.Deg2Rad * angleX);

		return q;
	}
}
