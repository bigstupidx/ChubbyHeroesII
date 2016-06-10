using UnityEngine;
using System.Collections;
using System ;
public class SoundController : MonoBehaviour {
	

	public static SoundController Static ;
	public AudioSource[]  audioSources;
	public AudioClip[] Clips;
	public string[] ClipsName;
	public AudioSource bgSound,coinsCount,jetPackSound;
	public AudioSource coinSoundSource;
	public AudioClip coinSound;
	void Start () {
		
		Static = this;
	}

	public void playSoundFromName(string clipName)
	{
		swithAudioSources ( Clips[  Array.IndexOf(ClipsName, clipName)  ]  );
	}
	public void StopSounds ()
	{
		GetComponent<AudioSource>().Stop ();
	}
	
	void swithAudioSources( AudioClip clip)
	{
		if(audioSources[0].isPlaying) 
		{
			audioSources[1].PlayOneShot(clip);
		}
		else audioSources[0].PlayOneShot(clip);
		
	}

	public void playCoinSound()
	{

		if(!coinSoundSource.isPlaying ) coinSoundSource.PlayOneShot(coinSound);
	}
	void StopBG()
	{
		bgSound.volume=0;
	}

	 
}


