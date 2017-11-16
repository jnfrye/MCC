﻿/*
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

		private EventDelegate AddDelegate<TEvent>(EventDelegate<TEvent> eventDelegate) where TEvent : GameEvent
		{ // NOTE I don't really understand this code, gotta read through it
			// Early-out if we've already registered this delegate
			if (HasListener<TEvent>(eventDelegate))
			{ // TODO Throw an error here instead of returning null
				return null;
			}

			// Create a new non-generic delegate which calls our generic one.
			// This is the delegate we actually invoke.
			EventDelegate internalDelegate = (e) => eventDelegate((TEvent)e);
			delegateLookup[eventDelegate] = internalDelegate;

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

		public void RemoveListener<TEvent>(EventDelegate<TEvent> eventDelegate) where TEvent : GameEvent
		{ // TODO I don't understand this code either, read through it
			EventDelegate internalDelegate;
			if (delegateLookup.TryGetValue(eventDelegate, out internalDelegate))
			{
				EventDelegate tempDelegate;
				if (delegates.TryGetValue(typeof(TEvent), out tempDelegate))
				{
					tempDelegate -= internalDelegate;
					if (tempDelegate == null)
					{
						delegates.Remove(typeof(TEvent));
					}
					else
					{
						delegates[typeof(TEvent)] = tempDelegate;
					}
				}

				delegateLookup.Remove(eventDelegate);
			}
		}

		public bool HasListener<TEvent>(EventDelegate<TEvent> eventDelegate) where TEvent : GameEvent
		{
			return delegateLookup.ContainsKey(eventDelegate);
		}

		//Inserts the event into the current queue.
		public bool TryQueueEvent(GameEvent gameEvent)
		{
			if (!delegates.ContainsKey(gameEvent.GetType()))
			{ // TODO Make this throw an exception maybe? So we don't have to return a bool
				Debug.LogWarning("EventManager: QueueEvent failed due to no listeners for event: " + gameEvent.GetType());
				return false;
			}

			eventQueue.Enqueue(gameEvent);
			return true;
		}

		public void TriggerEvent(GameEvent gameEvent)
		{
			EventDelegate eventDelegate;
			if (delegates.TryGetValue(gameEvent.GetType(), out eventDelegate))
			{
				eventDelegate.Invoke(gameEvent);
			}
			else
			{ // TODO Maybe throw exception?
				Debug.LogWarning("Event: " + gameEvent.GetType() + " has no listeners");
			}
		}
	}
}