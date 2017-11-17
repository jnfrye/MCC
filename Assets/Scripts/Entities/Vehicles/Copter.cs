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
			EventManager.Instance.AddListener<Controller.YawCommandIssued>(OnYawCommand);
		}

		private void OnDisable()
		{
			EventManager.Instance.RemoveListener<Controller.MovementCommandIssued>(OnMovementCommand);
			EventManager.Instance.RemoveListener<Controller.YawCommandIssued>(OnYawCommand);
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

		private void OnYawCommand(Controller.YawCommandIssued yawCommand)
		{
			PerformRotation(yawCommand.yawDirection);
		}

		private void PerformRotation(float turningMovement)
		{
			Vector3 angularVelocity = new Vector3(0, angularSpeed * turningMovement, 0);
			rb.angularVelocity = angularVelocity;
		}
	}
}