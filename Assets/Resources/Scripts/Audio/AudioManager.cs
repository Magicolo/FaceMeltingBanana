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

	[Button("PlayMusic", "PlayMusic", NoPrefixLabel = true)] public bool playMusic;
	void PlayMusic() {
		
	}
	
	[Separator]
	 
	public AudioSource currentAmbiance;
	public AudioSource idleAmbiance;
	public AudioClip hospitalAmbiance;
	public AudioClip forestAmbiance;
	public AudioClip windAmbiance;
	public AudioClip windDangerAmbiance;

	[Button("PlayAmbiance", "PlayAmbiance", NoPrefixLabel = true)] public bool playAmbiance;
	void PlayAmbiance() {
		
	}
	
	[Separator]
	
	public AudioSource currentHallucination;
	public AudioSource idleHallucination;
	public AudioClip ambulanceHallucination;
	public AudioClip policeHallucination;
	public AudioClip heartBeepHallucination;
	public AudioClip traficHallucination;
	
	[Button("PlayHallucinations", "PlayHallucinations", NoPrefixLabel = true)] public bool playHallucinations;
	void PlayHallucinations() {
		playRandomHallucination(10, 30, 0.5F, 2);
	}
	
	[Button("StopHallucinations", "StopHallucinations", NoPrefixLabel = true)] public bool stopHallucinations;
	void StopHallucinations() {
		stopRandomHallucination(2);
	}

	public static void PlayAll() {
		PlayRandomMusic(60, 150, 0.5F, 10);
		PlayRandomAmbiance(45, 120, 0.5F, 5);
		PlayRandomHallucination(15, 60, 0.5F, 2);
	}
	
	public static void StopAll() {
		StopRandomMusic(2);
		StopRandomAmbiance(2);
		StopRandomHallucination(2);
	}
	
	public static void PlayRandomMusic(float minFrequency, float maxFrequency, float volume, float fade) {
		Instance.playRandomMusic(minFrequency, maxFrequency, volume, fade);
	}
		
	public static void StopRandomMusic(float fade) {
		Instance.stopRandomMusic(fade);
	}
	
	public static void PlayRandomAmbiance(float minFrequency, float maxFrequency, float volume, float fade) {
		Instance.playRandomAmbiance(minFrequency, maxFrequency, volume, fade);
	}
		
	public static void StopRandomAmbiance(float fade) {
		Instance.stopRandomAmbiance(fade);
	}
	
	public static void PlayRandomHallucination(float minFrequency, float maxFrequency, float volume, float fade) {
		Instance.playRandomHallucination(minFrequency, maxFrequency, volume, fade);
	}
		
	public static void StopRandomHallucination(float fade) {
		Instance.stopRandomHallucination(fade);
	}
	
	void playRandomMusic(float minFrequency, float maxFrequency, float volume, float fade) {
		if (CoroutinesExist("PlayRandomMusic")) {
			StopCoroutines("PlayRandomMusic");
		}
		
		if (CoroutinesExist("FadeMusic")) {
			StopCoroutines("FadeMusic");
		}
		
		AudioClip[] songs = { ashsSong, brodysSong };
		StartCoroutine("PlayRandomMusic", PlayRandom(currentMusic, idleMusic, songs, minFrequency, maxFrequency, volume, fade, "Music"));
	}
	
	void stopRandomMusic(float fade) {
		if (CoroutinesExist("PlayRandomMusic")) {
			StopCoroutines("PlayRandomMusic");
		}
		
		if (CoroutinesExist("FadeMusic")) {
			StopCoroutines("FadeMusic");
		}
		
		StartCoroutine("FadeMusic", FadeOut(currentMusic, fade));
		StartCoroutine("FadeMusic", FadeOut(idleMusic, fade));
	}
	
	void playRandomAmbiance(float minFrequency, float maxFrequency, float volume, float fade) {
		if (CoroutinesExist("PlayRandomAmbiance")) {
			StopCoroutines("PlayRandomAmbiance");
		}
		
		if (CoroutinesExist("FadeAmbiance")) {
			StopCoroutines("FadeAmbiance");
		}
		
		AudioClip[] ambiances = { forestAmbiance, hospitalAmbiance, windAmbiance, windDangerAmbiance };
		StartCoroutine("PlayRandomAmbiance", PlayRandom(currentAmbiance, idleAmbiance, ambiances, minFrequency, maxFrequency, volume, fade, "Ambiance"));
	}
	
	void stopRandomAmbiance(float fade) {
		if (CoroutinesExist("PlayRandomAmbiance")) {
			StopCoroutines("PlayRandomAmbiance");
		}
		
		if (CoroutinesExist("FadeAmbiance")) {
			StopCoroutines("FadeAmbiance");
		}
		
		StartCoroutine("FadeAmbiance", FadeOut(currentAmbiance, fade));
		StartCoroutine("FadeAmbiance", FadeOut(idleAmbiance, fade));
	}
	
	void playRandomHallucination(float minFrequency, float maxFrequency, float volume, float fade) {
		if (CoroutinesExist("PlayRandomHallucination")) {
			StopCoroutines("PlayRandomHallucination");
		}
		
		if (CoroutinesExist("FadeHallucination")) {
			StopCoroutines("FadeHallucination");
		}
		
		AudioClip[] hallucinations = { ambulanceHallucination, policeHallucination, heartBeepHallucination, traficHallucination };
		StartCoroutine("PlayRandomHallucination", PlayRandom(currentHallucination, idleHallucination, hallucinations, minFrequency, maxFrequency, volume, fade, "Hallucination"));
	}
		
	void stopRandomHallucination(float fade) {
		if (CoroutinesExist("PlayRandomHallucination")) {
			StopCoroutines("PlayRandomHallucination");
		}
		
		if (CoroutinesExist("FadeHallucination")) {
			StopCoroutines("FadeHallucination");
		}
		
		StartCoroutine("FadeHallucination", FadeOut(currentHallucination, fade));
		StartCoroutine("FadeHallucination", FadeOut(idleHallucination, fade));
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

	IEnumerator PlayRandom(AudioSource source1, AudioSource source2, AudioClip[] audioClips, float minFrequency, float maxFrequency, float volume, float fade, string suffix) {
		while (true) {
			float random = Random.Range(minFrequency, maxFrequency);
			float counter = 0;
			
			while (counter < random) {
				counter += Time.deltaTime;
				yield return new WaitForSeconds(0);
			}
			
			source2.clip = audioClips.GetRandom();
			source2.volume = 0;
			source2.Play();
		
			if (CoroutinesExist("Fade" + suffix)) {
				StopCoroutines("Fade" + suffix);
			}
			StartCoroutine("Fade" + suffix, FadeOut(source1, fade));
			StartCoroutine("Fade" + suffix, FadeIn(source2, volume, fade));
		
			AudioSource sourceTemp = source1;
			source1 = source2;
			source2 = sourceTemp;
		}
	}
}