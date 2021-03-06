﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeImage : MonoBehaviour
{
	[Range(0, 1)] [SerializeField] private float startAlpha = 0.673f;
	[SerializeField] private Image image;
	[SerializeField] private float fadeSpeed = 0.9f;

	private bool FADE_IMAGE = true;

	void Awake()
	{
		if (image == null)
		{
			image = GetComponent<Image>();
		}

		image.gameObject.SetActive(true);
		Color tempColor = image.color;
		tempColor.a = 0f;
		image.color = tempColor;
	}

	// Update is called once per frame
	void Update()
	{
		Color tempColor = image.color;
		if (tempColor.a > 0f && FADE_IMAGE)
		{
			float tempA = tempColor.a - (fadeSpeed * Time.unscaledDeltaTime);
			tempColor.a = Mathf.Clamp(tempA, 0f, 1f);
			image.color = tempColor;
		}
	}

	/// <summary>
	/// Pauses the fading of the fadeImage for X specified seconds.
	/// </summary>
	/// <param name="seconds">Defaults to -1f in order to check how long the playercontrollerCC slowTime is.</param>
	public void Pause(float seconds = 3.0f)
	{
		StartCoroutine(DontFadeForSeconds(seconds));
	}

	private IEnumerator DontFadeForSeconds(float seconds)
	{
		FADE_IMAGE = false;
		yield return new WaitForSecondsRealtime(seconds);
		FADE_IMAGE = true;
	}

	public void ResetAlpha()
	{
		Color tempColor = image.color;
		tempColor.a = startAlpha;
		image.color = tempColor;
	}
}
