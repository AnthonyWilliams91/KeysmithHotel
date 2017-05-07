using UnityEngine;
using System.Collections;

[RequireComponent(typeof(CircleCollider2D))]

public class DragObject : MonoBehaviour
{
	void OnMouseDrag()
	{
		Debug.Log("whaa");
		Vector3 point = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		point.z = gameObject.transform.position.z;
		gameObject.transform.position = point;
		Cursor.visible = false;
	}

	void OnMouseDown()
	{
		Debug.Log("come on");
	}

	void OnMouseUp()
	{
		Cursor.visible = true;
	}
}
