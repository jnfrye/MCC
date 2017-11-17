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
			EventManager.Instance.AddListener<Controller.MovementCommandIssued>(OnMovementCommand);
			controller.YawCommand += PerformRotation;
		}

		private void OnDisable()
		{
			EventManager.Instance.RemoveListener<Controller.MovementCommandIssued>(OnMovementCommand);
			controller.YawCommand -= PerformRotation;
		}

		private void OnMovementCommand(Controller.MovementCommandIssued movementCommand)
		{
			PerformMovement(movementCommand.movementDirection);
		}

		private void PerformMovement(Vector3 movementCommand)
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