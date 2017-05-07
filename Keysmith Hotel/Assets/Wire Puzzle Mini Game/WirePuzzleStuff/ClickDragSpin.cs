using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(CircleCollider2D))]

public class ClickDragSpin : MonoBehaviour
{
	public const float speed = 120;
	private bool dragging = false;
	private bool fused = false;

	void OnMouseDrag()
	{
		if (fused)
			return;
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

			//Debug.Log("pos:" + transform.position.x + "," + transform.position.y);
			//Debug.Log("transformPoint:" + transform.TransformPoint(Vector3.zero));
			//Debug.Log("transformPoint:" + transform.TransformPoint(new Vector3(0,.6f)));
			//Debug.Log("localPos:" + transform.localPosition);
			//Debug.Log("localToWorldMatrix:" + transform.localToWorldMatrix);
			//Debug.Log("localToWorldMatrix applied to pos:" + transform.localToWorldMatrix.MultiplyPoint(transform.position));
			//Debug.Log("localToWorldMatrix applied to zero:" + transform.localToWorldMatrix.MultiplyPoint(Vector3.zero));
			//Debug.Log("localToWorldMatrix applied to localPos:" + transform.localToWorldMatrix.MultiplyPoint(transform.localPosition));
			//Debug.Log("WorldToScreenPoint pos:" + Camera.main.WorldToScreenPoint(transform.position));
			//Debug.Log("WorldToScreenPoint localPos:" + Camera.main.WorldToScreenPoint(transform.localPosition));
			//Debug.Log("WorldToScreenPoint localToWorldMatrix applied to zero:" + Camera.main.WorldToScreenPoint(transform.localToWorldMatrix.MultiplyPoint(Vector3.zero)));
			
			List<Line> puzzlePlacementSolutionLines = FindObjectOfType<WirePuzzleController>().puzzlePlacementSolutionLines;
			foreach (Line currLine in puzzlePlacementSolutionLines)
			{
				if( currLine.fused )
					continue;
				Vector3 placedPointARotated = transform.TransformPoint(new Vector3(0, -.6f));
				Vector3 placedPointBRotated = transform.TransformPoint(new Vector3(0, .6f));
				Vector3 movedPosA = Camera.main.WorldToScreenPoint(placedPointARotated);
				Vector3 movedPosB = Camera.main.WorldToScreenPoint(placedPointBRotated);
				//Debug.Log("comparing movedA:" + movedPosA + " and movedB:" + movedPosB + " - to - pointA:" + currLine.pointA + " and pointB:" + currLine.pointB);
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
					currLine.fused = true;
					checkFinish(puzzlePlacementSolutionLines);
					foundMatch();
				}
			}
		}
	}

	private void checkFinish(List<Line> puzzlePlacementSolutionLines)
	{
		int fusedLines = 0;
		foreach( Line checkLine in puzzlePlacementSolutionLines )
		{
			if (checkLine.fused)
				fusedLines++;
		}
		if(fusedLines >= puzzlePlacementSolutionLines.Count)
		{
			Debug.Log("You Did It!!!!");
			// TODO show congrats, wait for click, then close down
		}
	}

	private void foundMatch()
	{
		transform.localScale *= 2;
		OnMouseUp();
		fused = true;
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
