using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

	public GameObject player;

	private Vector3 offset;

	// Use this for initialization
	void Start () {
		offset = transform.position - player.transform.position;
		player = GameObject.FindGameObjectWithTag("Player");
	} 
	
	// Update is called once per frame
	void LateUpdate () {
		if(player.transform.position.x >= 0 && player.transform.position.x <= 191) {
			transform.position = player.transform.position + offset;	
		}
	}
}
