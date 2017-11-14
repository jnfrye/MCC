using UnityEngine;

namespace MCC
{
	public class Copter : Entity
	{
		public float speed;
		public float angularSpeed;

		private Rigidbody rb;

		private void Start() { rb = gameObject.GetComponent<Rigidbody>(); }

		private void OnEnable()
		{
			controller.MovementCommand += PerformTranslation;
			controller.YawCommand += PerformRotation;
		}

		private void OnDisable()
		{
			controller.MovementCommand -= PerformTranslation;
			controller.YawCommand -= PerformRotation;
		}

		private void PerformTranslation(Vector3 movementCommand)
		{
			Vector3 localVelocity = speed * movementCommand;
			Vector3 globalVelocity = transform.TransformDirection(localVelocity);
			rb.velocity = globalVelocity;
		}

		private void PerformRotation(float turningMovement)
		{
			Vector3 angularVelocity = new Vector3(0, angularSpeed * turningMovement, 0);
			rb.angularVelocity = angularVelocity;
		}
	}
}