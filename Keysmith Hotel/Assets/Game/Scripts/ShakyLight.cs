using UnityEngine;
using System.Collections;

namespace AssemblyCSharp
{
	public class ShakyLight : MonoBehaviour
	{
		public Light light;


		// Use this for initialization
		void Start () {
			light = this.GetComponent<Light> ();
		}

		// Update is called once per frame
		void Update () {
			light.intensity = (flickerVal()/100);
		}

		// Returns Light intensity value
		float flickerVal() {
			return Random.Range(700, 800);
		}
	}
}

