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
		private Queue eventQueue = new Queue();

		// TODO Definitely need better names for these
		private Dictionary<Type, Listener> listenersByType = new Dictionary<Type, Listener>();
		private Dictionary<Delegate, Listener> listenersByDelegate = new Dictionary<Delegate, Listener>();

		// TODO I don't know if the name change here was good
		public delegate void Listener<TEvent>(TEvent gameEvent) where TEvent : GameEvent;
		private delegate void Listener(GameEvent gameEvent);

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

		public void AddListener<TEvent>(Listener<TEvent> newListener) where TEvent : GameEvent
		{ // NOTE I don't really understand this code, gotta read through it
			if (HasListener<TEvent>(newListener))
			{ // TODO Throw an error here instead?
				Debug.LogWarning("Listener: " + newListener + " was already registered."); ;
			}

			// Create a new non-generic delegate which calls our generic one.
			// This is the delegate we actually invoke.
			Listener internalListener = (gameEvent) => newListener((TEvent)gameEvent);
			listenersByDelegate[newListener] = internalListener;

			Listener tempListener;
			if (listenersByType.TryGetValue(typeof(TEvent), out tempListener))
			{
				listenersByType[typeof(TEvent)] = tempListener += internalListener;
			}
			else
			{
				listenersByType[typeof(TEvent)] = internalListener;
			}
		}

		public void RemoveListener<TEvent>(Listener<TEvent> listener) where TEvent : GameEvent
		{ // TODO I don't understand this code either, read through it
			Listener internalListener;
			if (listenersByDelegate.TryGetValue(listener, out internalListener))
			{
				Listener tempListener;
				if (listenersByType.TryGetValue(typeof(TEvent), out tempListener))
				{
					tempListener -= internalListener;
					if (tempListener == null)
					{
						listenersByType.Remove(typeof(TEvent));
					}
					else
					{
						listenersByType[typeof(TEvent)] = tempListener;
					}
				}

				listenersByDelegate.Remove(listener);
			}
		}

		public bool HasListener<TEvent>(Listener<TEvent> listener) where TEvent : GameEvent
		{
			return listenersByDelegate.ContainsKey(listener);
		}
		
		public bool TryQueueEvent(GameEvent gameEvent)
		{
			if (!listenersByType.ContainsKey(gameEvent.GetType()))
			{ // TODO Make this throw an exception maybe? So we don't have to return a bool
				Debug.LogWarning("EventManager: QueueEvent failed due to no listeners for event: " + gameEvent.GetType());
				return false;
			}

			eventQueue.Enqueue(gameEvent);
			return true;
		}
		
		private void Update()
		{
			while (eventQueue.Count > 0)
			{
				GameEvent nextEvent = eventQueue.Dequeue() as GameEvent;
				TriggerEvent(nextEvent);
			}
		}

		public void TriggerEvent(GameEvent gameEvent)
		{
			Listener listener;
			if (listenersByType.TryGetValue(gameEvent.GetType(), out listener))
			{
				listener.Invoke(gameEvent);
			}
			else
			{ // TODO Maybe throw exception?
				Debug.LogWarning("Event: " + gameEvent.GetType() + " has no listeners");
			}
		}
	}
}