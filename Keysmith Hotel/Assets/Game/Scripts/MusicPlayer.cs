using UnityEngine;
using System.Collections;


[RequireComponent(typeof(AudioSource))]
public class MusicPlayer : MonoBehaviour {


		void Start()
		{
			AudioSource audio = GetComponent<AudioSource>();
			audio.Play();
			audio.Play(44100);
		}

}
