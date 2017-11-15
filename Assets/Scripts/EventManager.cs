/*
* Original file: Copyright 2017 Ben D'Angelo. See license file for copyright information.
*/
using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

namespace MCC
{
	public abstract class GameEvent { }

	public sealed class EventManager : MonoBehaviour
	{
		private static EventManager instance;
		private static readonly object instantiationLock = new object();
		private Queue eventQueue = new Queue();

		private Dictionary<Type, EventDelegate> delegates = new Dictionary<Type, EventDelegate>();
		private Dictionary<Delegate, EventDelegate> delegateLookup = new Dictionary<Delegate, EventDelegate>();

		public delegate void EventDelegate<T>(T e) where T : GameEvent;
		private delegate void EventDelegate(GameEvent e);

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

		public void AddListener<TEvent>(EventDelegate<TEvent> newListener) where TEvent : GameEvent
		{
			AddDelegate<TEvent>(newListener);
		}

		private EventDelegate AddDelegate<TEvent>(EventDelegate<TEvent> newDelegate) where TEvent : GameEvent
		{ // NOTE I don't really understand this code, gotta read through it
			// Early-out if we've already registered this delegate
			if (delegateLookup.ContainsKey(newDelegate))
			{ // TODO Throw an error here instead of returning null
				return null;
			}

			// Create a new non-generic delegate which calls our generic one.
			// This is the delegate we actually invoke.
			EventDelegate internalDelegate = (e) => newDelegate((TEvent)e);
			delegateLookup[newDelegate] = internalDelegate;

			EventDelegate tempDelegate;
			if (delegates.TryGetValue(typeof(TEvent), out tempDelegate))
			{
				delegates[typeof(TEvent)] = tempDelegate += internalDelegate;
			}
			else
			{
				delegates[typeof(TEvent)] = internalDelegate;
			}

			return internalDelegate;
		}
	}
}