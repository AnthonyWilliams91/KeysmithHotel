using UnityEngine;
using System.Collections;
//using System;

public class FlickeringLight : MonoBehaviour {
	public Light light;
	static float timeLim;
	public float remove;
	public bool lightIsOn;
	static float steadyRange;
	static float flickeringRange;
	 

	// Use this for initialization
	void Start () {
		light = this.GetComponent<Light> ();
		lightIsOn = true;
		timeLim = Random.Range (40, 60);
	}

	// Update is called once per frame
	void Update () {

		remove = Time.deltaTime;
		timeLim -= remove;

		steadyRange = Random.Range(2, 8);
		flickeringRange = Random.Range(5, 10);

		// Start flickering
		if (timeLim < (timeLim / 2) + steadyRange + flickeringRange && timeLim > (timeLim / 2) + steadyRange) {
			light.intensity = flickerVal ();
			if (light.intensity < 1.5) {
				lightIsOn = false;
			} else {
				lightIsOn = true;
			}
		} else if (timeLim < (timeLim / 2) + steadyRange && timeLim > timeLim / 2) {
			lightIsOn = false;
			light.intensity = 0;
		} else if (timeLim < (timeLim / 2) && timeLim > (timeLim / 2) - flickeringRange) {
			light.intensity = flickerVal ();
			if (light.intensity < 1.5) {
				lightIsOn = false;
			} else {
				lightIsOn = true;
			}
		} else {
			lightIsOn = true;
			light.intensity = Random.Range(6, 8);
		}

		if (timeLim <= 0) {
			timeLim = Random.Range (14, 30);
		}
	}

	// Returns Light intensity value
	float flickerVal() {
		return Random.Range(0, 8);
	}
}
