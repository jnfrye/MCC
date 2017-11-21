using UnityEngine;

namespace MCC
{
	public class UserInput : Controller
	{
		private void Update()
		{
			IssueMovementCommands();
			IssueYawCommands();
			IssueZoomCommands();
			IssueTiltCommands();
		}

		// TODO Notice how similar the next several methods are... abstract this?
		private void IssueMovementCommands() // TODO maybe these should be moved to the controller class?
		{
			Vector3 movementDirection = GetMovementInput();
			if (movementDirection != Vector3.zero)
			{
				EventManager.Instance.TriggerEvent(new MovementCommandIssued(movementDirection));
			}
		}

		private Vector3 GetMovementInput()
		{
			float xMovement = Input.GetAxis("Horizontal");
			float yMovement = Input.GetAxis("Vertical");
			float zMovement = Input.GetAxis("Forward");

			return new Vector3(xMovement, yMovement, zMovement);
		}

		private void IssueYawCommands()
		{
			float yawDirection = GetYawInput();

			if (yawDirection != 0f)
			{
				EventManager.Instance.TriggerEvent(new YawCommandIssued(yawDirection));
			}
		}

		private float GetYawInput()
		{
			return Input.GetAxis("Turn");
		}

		private void IssueZoomCommands()
		{
			float zoomDirection = GetZoomInput();
			
			if (zoomDirection != 0f)
			{
				EventManager.Instance.TriggerEvent(new ZoomCommandIssued(zoomDirection));
			}
		}

		private float GetZoomInput()
		{
			return Input.GetAxis("Mouse ScrollWheel");
		}

		private void IssueTiltCommands() // TODO get better name
		{
			int tiltDirection = GetTiltInput();

			if (tiltDirection != 0)
			{
				EventManager.Instance.TriggerEvent(new TiltCommandIssued(tiltDirection));
			}
		}

		private int GetTiltInput() // TODO get better name
		{
			float mouseY = Input.mousePosition.y;
			float mouseYRatio = mouseY / Screen.height;
			int borderDirection = 0;
			// Camera pitch
			if (GlobalConstants.Screen.TopBorder.Contains(mouseYRatio))
			{
				borderDirection = 1;
			}
			else if (GlobalConstants.Screen.BottomBorder.Contains(mouseYRatio))
			{
				borderDirection = -1;
			}
			return borderDirection;
		}
	}

	public class ZoomCommandIssued : GameEvent
	{
		public float zoomDirection;

		public ZoomCommandIssued(float zoomDirection)
		{
			this.zoomDirection = zoomDirection;
		}
	}
}