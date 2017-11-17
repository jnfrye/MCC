using UnityEngine;

namespace MCC
{
	public class UserInput : Controller
	{
		private void Update()
		{
			IssueMovementCommands();
			PublishYawCommand();
			PublishScrollCommand();
			IssueTiltCommand();
		}

		// TODO Notice how similar the next several methods are... abstract this?
		private void IssueMovementCommands()
		{
			float xMovement = Input.GetAxis("Horizontal");
			float yMovement = Input.GetAxis("Vertical");
			float zMovement = Input.GetAxis("Forward");

			Vector3 movementDirection = new Vector3(xMovement, yMovement, zMovement);
			if (movementDirection != Vector3.zero)
			{
				EventManager.Instance.TriggerEvent(new MovementCommandIssued(movementDirection));
			}
		}

		private void PublishYawCommand()
		{
			float yawDirection = Input.GetAxis("Turn");

			if (yawDirection != 0f)
			{
				OnYawCommand(yawDirection);
			}
		}

		private void PublishScrollCommand()
		{
			float scrollDirection = Input.GetAxis("Mouse ScrollWheel");

			if (scrollDirection != 0f)
			{
				OnZoomCommand(scrollDirection);
			}
		}

		private void IssueTiltCommand() // TODO get better name
		{
			int screenBorderDirection = DetermineScreenBorderDirection(Input.mousePosition.y);

			if (screenBorderDirection != 0)
			{
				EventManager.Instance.TriggerEvent(new TiltCommandIssued(screenBorderDirection));
			}
		}

		private int DetermineScreenBorderDirection(float mouseY) // TODO get better name
		{
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
}