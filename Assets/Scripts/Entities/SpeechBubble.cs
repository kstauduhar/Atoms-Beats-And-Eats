﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeechBubble : MonoBehaviour
{

	private int speechHideStack;
	private Animator animator;
	private SpriteRenderer recipe;

	private Sprite success;
	private Sprite failure;

	private AudioSource audioSource;

	void Awake ()
	{
		speechHideStack = 0;
		recipe = transform.Find("Image").GetComponent<SpriteRenderer> ();
		animator = GetComponent<Animator> ();
		audioSource = GetComponent<AudioSource> ();

		success = Resources.Load<Sprite> ("RecipeIcons/Success");
		failure = Resources.Load<Sprite> ("RecipeIcons/Failure");
	}

	// Show the speech bubble when an order starts
	public void orderStart()
	{
		speechHideStack++;
		gameObject.SetActive (true);
	}

	// Hide the speech bubble when an order ends
	public void orderEnd(ORDER_RESULT result)
	{
		speechHideStack--;
		speechHideStack = Mathf.Max (0, speechHideStack);
		if (speechHideStack == 0)
		{
			gameObject.SetActive (false);
		}
	}

	public void recipeStart(Sprite image)
	{
		// Show the recipe image
		recipe.sprite = image;
		// Animate the speech bubble
		animator.SetTrigger ("Pop");
	}

	public void recipeEnd(bool didSucceed)
	{
		// Show the success or failure image in the speech bubble
		recipe.sprite = didSucceed ? success : failure;
		if (didSucceed)
		{
			audioSource.Play ();
		}
		// Animate the speech bubble
		animator.SetTrigger ("Pop");
	}
}
