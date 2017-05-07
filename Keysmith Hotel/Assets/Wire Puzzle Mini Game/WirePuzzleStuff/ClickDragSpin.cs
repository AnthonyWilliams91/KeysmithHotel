using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(CircleCollider2D))]

public class ClickDragSpin : MonoBehaviour
{
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

			Debug.Log("pos:" + transform.position.x + "," + transform.position.y);
			Debug.Log("transformPoint:" + transform.TransformPoint(Vector3.zero));
			Debug.Log("transformPoint:" + transform.TransformPoint(new Vector3(0,.6f)));
			Debug.Log("localPos:" + transform.localPosition);
			Debug.Log("localToWorldMatrix:" + transform.localToWorldMatrix);
			Debug.Log("localToWorldMatrix applied to pos:" + transform.localToWorldMatrix.MultiplyPoint(transform.position));
			Debug.Log("localToWorldMatrix applied to zero:" + transform.localToWorldMatrix.MultiplyPoint(Vector3.zero));
			Debug.Log("localToWorldMatrix applied to localPos:" + transform.localToWorldMatrix.MultiplyPoint(transform.localPosition));
			Debug.Log("WorldToScreenPoint pos:" + Camera.main.WorldToScreenPoint(transform.position));
			Debug.Log("WorldToScreenPoint localPos:" + Camera.main.WorldToScreenPoint(transform.localPosition));
			Debug.Log("WorldToScreenPoint localToWorldMatrix applied to zero:" + Camera.main.WorldToScreenPoint(transform.localToWorldMatrix.MultiplyPoint(Vector3.zero)));

			float height = GetComponent<SpriteRenderer>().sprite.rect.size.y;
			List<Line> puzzlePlacementSolutionLines = FindObjectOfType<WirePuzzleController>().puzzlePlacementSolutionLines;
			foreach (Line currLine in puzzlePlacementSolutionLines)
			{
				Vector3 placedPointBRotated = transform.TransformPoint(new Vector3(0, .6f));
				Vector3 movedPosA = Camera.main.WorldToScreenPoint(transform.position);
				Vector3 movedPosB = Camera.main.WorldToScreenPoint(placedPointBRotated);
				Debug.Log("comparing movedPosA:" + movedPosA + " and movedPosB:" + movedPosB + " - to - pointA:" + currLine.pointA + " and pointB:" + currLine.pointB);
				const float closeEnough = 30;
				if (
					(Mathf.Abs(movedPosA.x - currLine.pointA.x) < closeEnough &&
					Mathf.Abs(movedPosA.y - currLine.pointA.y) < closeEnough &&
					Mathf.Abs(movedPosB.x - currLine.pointB.x) < closeEnough &&
					Mathf.Abs(movedPosB.y - currLine.pointB.y) < closeEnough) ||
					(Mathf.Abs(movedPosA.x - currLine.pointB.x) < closeEnough &&
					Mathf.Abs(movedPosA.y - currLine.pointB.y) < closeEnough &&
					Mathf.Abs(movedPosB.x - currLine.pointA.x) < closeEnough &&
					Mathf.Abs(movedPosB.y - currLine.pointA.y) < closeEnough)
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
