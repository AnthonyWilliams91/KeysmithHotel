using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(CircleCollider2D))]


public class ClickdragSpin : MonoBehaviour {
	public const float speed = 167;
	private bool dragging = false;

	void OnMouseDrag()
	{
		Vector3 point = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		point.z = gameObject.transform.position.z;
		gameObject.transform.position = point;
		Cursor.visible = false;
		dragging = true;
	}

	void Update()
	{
		if( dragging )
		{
			if (Input.GetKey(KeyCode.A))
				transform.Rotate(Vector3.back, -speed * Time.deltaTime);

			if (Input.GetKey(KeyCode.D))
				transform.Rotate(Vector3.back, speed * Time.deltaTime);

			float height = GetComponent<SpriteRenderer>().sprite.rect.size.y;
			List<Line> puzzlePlacementSolutionLines = FindObjectOfType<WirePuzzleController>().puzzlePlacementSolutionLines;
			foreach (Line currLine in puzzlePlacementSolutionLines)
			{
				Vector3 placedPointB = new Vector3(
					transform.localPosition.x,
					transform.localPosition.y + height,
					transform.localPosition.z
				);
				Vector3 placedPointBRotated = transform.localToWorldMatrix.MultiplyPoint(placedPointB);
				// VZTODO: add parent position
				if (
					(Mathf.Abs(transform.position.x - currLine.pointA.x) < .2 &&
					Mathf.Abs(transform.position.y - currLine.pointA.y) < .2 &&
					Mathf.Abs(placedPointBRotated.x - currLine.pointB.x) < .2 &&
					Mathf.Abs(placedPointBRotated.y - currLine.pointB.y) < .2) ||
					(Mathf.Abs(transform.position.x - currLine.pointB.x) < .2 &&
					Mathf.Abs(transform.position.y - currLine.pointB.y) < .2 &&
					Mathf.Abs(placedPointBRotated.x - currLine.pointA.x) < .2 &&
					Mathf.Abs(placedPointBRotated.y - currLine.pointA.y) < .2)
					)
				{
					// found match!
					Debug.Log("Found match!!!!");
				}
			}
		}
	}

	//void OnMouseDown()
	//{
	//	Debug.Log("come on");
	//}

	void OnMouseUp()
	{
		Cursor.visible = true;
		dragging = false;
	}
}
