using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Magicolo;

public class AudioManager : MonoBehaviourExtended {

	public enum Songs {
		Ash,
		Brody
	}
	
	public enum Ambiances {
		Hospital,
		Forest,
		Wind,
		WindDanger
	}
	
	static AudioManager instance;
	static AudioManager Instance {
		get {
			if (instance == null) {
				instance = FindObjectOfType<AudioManager>();
			}
			return instance;
		}
	}
   
	public AudioSource currentMusic;
	public AudioSource idleMusic;
	public AudioClip ashsSong;
	public AudioClip brodysSong;
		
	[Button("SwitchToAsh", "SwitchToAsh", NoPrefixLabel = true)] public bool switchToAsh;
	void SwitchToAsh() {
		switchMusic(Songs.Ash, 0.75F, 10);
	}
	
	[Button("SwitchToBrody", "SwitchToBrody", NoPrefixLabel = true)] public bool switchToBrody;
	void SwitchToBrody() {
		switchMusic(Songs.Brody, 0.5F, 10);
	}

	[Separator]
	 
	public AudioSource currentAmbiance;
	public AudioSource idleAmbiance;
	public AudioClip hospitalAmbiance;
	public AudioClip forestAmbiance;
	public AudioClip windAmbiance;
	public AudioClip windDangerAmbiance;
		
	[Button("SwitchToForest", "SwitchToForest", NoPrefixLabel = true)] public bool switchToForest;
	void SwitchToForest() {
		switchAmbiance(Ambiances.Forest, 0.5F, 10);
	}
	
	[Button("SwitchToWind", "SwitchToWind", NoPrefixLabel = true)] public bool switchToWind;
	void SwitchToWind() {
		switchAmbiance(Ambiances.Wind, 0.75F, 10);
	}
	
	[Button("SwitchToWindDanger", "SwitchToWindDanger", NoPrefixLabel = true)] public bool switchToWindDanger;
	void SwitchToWindDanger() {
		switchAmbiance(Ambiances.WindDanger, 0.75F, 10);
	}
	
	[Button("SwitchToHospital", "SwitchToHospital", NoPrefixLabel = true)] public bool switchToHospital;
	void SwitchToHospital() {
		switchAmbiance(Ambiances.Hospital, 0.5F, 10);
	}

	[Separator]
	
	public AudioSource currentHallucination;
	public AudioSource idleHallucination;
	public AudioClip ambulanceHallucination;
	public AudioClip policeHallucination;
	public AudioClip heartBeepHallucination;
	public AudioClip traficHallucination;
	
	[Button("PlayRandom", "PlayRandom", NoPrefixLabel = true)] public bool playRandom;
	void PlayRandom() {
		playRandomHallucinations(10, 30, 0.5F, 2);
	}
	
	[Button("StopRandom", "StopRandom", NoPrefixLabel = true)] public bool stopRandom;
	void StopRandom() {
		stopRandomHallucinations(2);
	}
	
	
	public static void SwitchMusic(Songs song, float volume, float fade) {
		Instance.switchMusic(song, volume, fade);
	}
	
	public static void SwitchAmbiance(Ambiances ambiance, float volume, float fade) {
		Instance.switchAmbiance(ambiance, volume, fade);
	}
	
	public static void PlayRandomHallucinations(float minFrequency, float maxFrequency, float volume, float fade) {
		Instance.playRandomHallucinations(minFrequency, maxFrequency, volume, fade);
	}
		
	public static void StopRandomHallucinations(float fade) {
		Instance.stopRandomHallucinations(fade);
	}
	
	void switchMusic(Songs song, float volume, float fade) {
		switch (song) {
			case Songs.Ash:
				idleMusic.clip = ashsSong;
				break;
			case Songs.Brody:
				idleMusic.clip = brodysSong;
				break;
		}
		
		idleMusic.volume = 0;
		idleMusic.Play();
		
		if (CoroutinesExist("FadeMusic")) {
			StopCoroutines("FadeMusic");
		}
		StartCoroutine("FadeMusic", FadeOut(currentMusic, fade));
		StartCoroutine("FadeMusic", FadeIn(idleMusic, volume, fade));
		
		AudioSource musicTemp = currentMusic;
		currentMusic = idleMusic;
		idleMusic = musicTemp;
	}
	
	void switchAmbiance(Ambiances ambiance, float volume, float fade) {
		switch (ambiance) {
			case Ambiances.Hospital:
				idleAmbiance.clip = hospitalAmbiance;
				break;
			case Ambiances.Forest:
				idleAmbiance.clip = forestAmbiance;
				break;
			case Ambiances.Wind:
				idleAmbiance.clip = windAmbiance;
				break;
			case Ambiances.WindDanger:
				idleAmbiance.clip = windDangerAmbiance;
				break;
		}
		
		idleAmbiance.volume = 0;
		idleAmbiance.Play();
		
		if (CoroutinesExist("FadeAmbiances")) {
			StopCoroutines("FadeAmbiances");
		}
		StartCoroutine("FadeAmbiance", FadeOut(currentAmbiance, fade));
		StartCoroutine("FadeAmbiance", FadeIn(idleAmbiance, volume, fade));
		
		AudioSource ambianceTemp = currentAmbiance;
		currentAmbiance = idleAmbiance;
		idleAmbiance = ambianceTemp;
	}
	
	void playRandomHallucinations(float minFrequency, float maxFrequency, float volume, float fade) {
		if (CoroutinesExist("PlayRandomHallucinations")) {
			StopCoroutines("PlayRandomHallucinations");
		}
		
		if (CoroutinesExist("FadeHallucinations")) {
			StopCoroutines("FadeHallucinations");
		}
		
		StartCoroutine("PlayRandomHallucinations", PlayRandom(currentHallucination, idleHallucination, minFrequency, maxFrequency, volume, fade));
	}
		
	void stopRandomHallucinations(float fade) {
		if (CoroutinesExist("PlayRandomHallucinations")) {
			StopCoroutines("PlayRandomHallucinations");
		}
		
		if (CoroutinesExist("FadeHallucinations")) {
			StopCoroutines("FadeHallucinations");
		}
		
		StartCoroutine("FadeHallucinations", FadeOut(currentHallucination, fade));
		StartCoroutine("FadeHallucinations", FadeOut(idleHallucination, fade));
	}
	
	IEnumerator FadeIn(AudioSource source, float targetVolume, float fade) {
		float counter = 0;
		
		while (source.volume < targetVolume) {
			counter += Time.deltaTime;
			source.volume = (counter / fade) * targetVolume;
			yield return new WaitForSeconds(0);
		}
		
		source.volume = targetVolume;
	}
	
	IEnumerator FadeOut(AudioSource source, float fade) {
		float startVolume = source.volume;
		float counter = 0;
		
		while (source.volume > 0) {
			counter += Time.deltaTime;
			source.volume = (1 - counter / fade) * startVolume;
			yield return new WaitForSeconds(0);
		}
		
		source.volume = 0;
		source.Stop();
	}

	IEnumerator PlayRandom(AudioSource source1, AudioSource source2, float minFrequency, float maxFrequency, float volume, float fade) {
		while (true) {
			float random = Random.Range(minFrequency, maxFrequency);
			float counter = 0;
			
			while (counter < random) {
				counter += Time.deltaTime;
				yield return new WaitForSeconds(0);
			}
			
			source2.clip = new []{ ambulanceHallucination, policeHallucination, heartBeepHallucination, traficHallucination }.GetRandom();
			source2.volume = 0;
			source2.Play();
		
			if (CoroutinesExist("FadeHallucinations")) {
				StopCoroutines("FadeHallucinations");
			}
			StartCoroutine("FadeHallucinations", FadeOut(source1, fade));
			StartCoroutine("FadeHallucinations", FadeIn(source2, volume, fade));
		
			AudioSource sourceTemp = source1;
			source1 = source2;
			source2 = sourceTemp;
		}
	}
}