using UnityEngine;

namespace MCC
{
	public class UserInput : Controller
	{
		private void Update()
		{
			PublishMovementCommand();
			PublishYawCommand();
			PublishScrollCommand();
			PublishScreenBorderCommand();
		}

		// TODO Notice how similar the next several methods are... abstract this?
		private void PublishMovementCommand()
		{
			float xMovement = Input.GetAxis("Horizontal");
			float yMovement = Input.GetAxis("Vertical");
			float zMovement = Input.GetAxis("Forward");

			Vector3 movementDirection = new Vector3(xMovement, yMovement, zMovement);
			if (movementDirection != Vector3.zero)
			{
				OnMovementCommand(movementDirection);
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

		private void PublishScreenBorderCommand() // TODO get better name
		{
			int screenBorderDirection = DetermineScreenBorderDirection(Input.mousePosition.y);

			if (screenBorderDirection != 0)
			{
				OnTiltCommand(screenBorderDirection);
			}
		}

		private int DetermineScreenBorderDirection(float mouseY) // TODO get better name
		{
			float mouseYRatio = mouseY / Screen.height;
			int borderDirection = 0;
			// Camera pitch
			if (GlobalConstants.Camera.TopOfScreen.Contains(mouseYRatio))
			{
				borderDirection = 1;
			}
			else if (GlobalConstants.Camera.BottomOfScreen.Contains(mouseYRatio))
			{
				borderDirection = -1;
			}
			return borderDirection;
		}
	}
}