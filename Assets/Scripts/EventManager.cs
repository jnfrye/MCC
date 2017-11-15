﻿/*
* Original file: Copyright 2017 Ben D'Angelo. See license file for copyright information.
*/
using UnityEngine;

namespace MCC
{ 
	public sealed class EventManager : MonoBehaviour
	{
		private static EventManager instance;
		private static readonly object instantiationLock = new object();

		private EventManager() { }

		public static EventManager Instance
		{
			get
			{
				lock (instantiationLock)
				{
					if (instance == null)
					{
						instance = new EventManager();
					}
					return instance;
				}
			}
		}
	}
}