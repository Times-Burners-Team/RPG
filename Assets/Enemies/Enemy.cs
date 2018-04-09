﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour 
{

	[SerializeField] float maxHealthPoints = 100f;

	float currentHealthPoint = 100f;

	public float healthAsPercentage
	{
		get
		{
			return currentHealthPoint / (float)maxHealthPoints;
		}
	}


}
