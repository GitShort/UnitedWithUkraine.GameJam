using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SoundManager : MonoBehaviour
{
	// Audio players components.
	public AudioSource MovementSounds;
	public AudioSource MusicSource;
	public AudioSource CatSounds;
	// Random pitch adjustment range.
	public float LowPitchRange = .95f;
	public float HighPitchRange = 1.05f;
	// Singleton instance.
	public static SoundManager Instance = null;

	public AudioClip bgMusic;
	public AudioClip[] meowingSounds;

	public float startingTime = 5f;
	public float randomTimer = 0f;
	public float modifier = 0f;

	// Initialize the singleton instance.
	private void Awake()
	{
		// If there is an instance, and it's not me, delete myself.

		if (Instance != null && Instance != this)
		{
			Destroy(this);
		}
		else
		{
			Instance = this;
		}
		this.gameObject.transform.parent = null;
		DontDestroyOnLoad(this);
	}

	private void Start()
	{
		PlayMusic(bgMusic);
		randomTimer = startingTime;
	}

	private void Update()
	{
		if(randomTimer != 0)
		{
			randomTimer -= Time.deltaTime;
		}
		if(randomTimer < 0)
		{
			RandomCatSound();
		}
	}
	// Play a single clip through the sound effects source.
	public void Play(AudioClip clip)
	{
		MovementSounds.clip = clip;
		MovementSounds.Play();
	}
	// Play a single clip through the music source.
	public void PlayMusic(AudioClip clip)
	{
		MusicSource.clip = clip;
		MusicSource.Play();
	}
	// Play a random clip from an array, and randomize the pitch slightly.
	public void RandomSoundEffect(params AudioClip[] clips)
	{
		int randomIndex = Random.Range(0, clips.Length);
		float randomPitch = Random.Range(LowPitchRange, HighPitchRange);
		MovementSounds.pitch = randomPitch;
		MovementSounds.clip = clips[randomIndex];
		MovementSounds.Play();
	}

	public void RandomCatSound()
	{
		if (meowingSounds.Length != 0)
		{
			int randomIndex = Random.Range(0, meowingSounds.Length);
			float randomPitch = Random.Range(LowPitchRange, HighPitchRange);
			CatSounds.pitch = randomPitch;
			CatSounds.clip = meowingSounds[randomIndex];
			CatSounds.Play();
		}
		modifier = Random.Range(5.0f, 10.0f);
		randomTimer = startingTime + modifier;
	}

}