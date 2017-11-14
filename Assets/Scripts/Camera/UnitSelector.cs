using UnityEngine;

namespace MCC
{
	public class UnitSelector : MonoBehaviour
	{
		private void Update()
		{
			if (Input.GetMouseButtonDown(0))
			{
				RaycastHit hit = new RaycastHit();
				Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
				if (Physics.Raycast(ray, out hit))
				{
					// TODO: Left off here. Look at http://www.bendangelo.me/tutorials/rts/2015/12/19/unity-rts-game-architecture.html
				}
			}
		}
	}
}