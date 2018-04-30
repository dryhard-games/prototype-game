namespace Decoy.Core.SoundSystem {
    using UnityEngine;
    using System.Collections.Generic;
    using System.Collections;
    using Decoy.Core.EventSystem;
    using Decoy.Core.GameDataSystem;
    /// <summary>
    /// SoundtrackController.cs
    /// <summary>
    /// Author: Thomas van Opstal
    /// </summary>
    public class SoundtrackController : MonoBehaviour {
        [Header("Pool of available Audio Sources")]
        public AudioSource audioSource;
        public SoundtrackModel soundtrackModel;

        private Dictionary<SOUNDTRACK_TYPES, AudioClip> soundtracks = new Dictionary<SOUNDTRACK_TYPES, AudioClip>();
        private float volume = 1.0f;
        private AudioSource activeSource;

        private Coroutine currentFadeIn;
        private Coroutine currentDampen;

        private void Start() {
            EventManager.StartListening(SoundEventTypes.DAMPING_PUZZLE_MUSIC, DampingMusicVolume);
            EventManager.StartListening(SoundEventTypes.PLAY_SOUNDTRACK, PlaySoundtrack);

            FillDictionary();
        }

        private void OnDestroy() {
            EventManager.StopListening(SoundEventTypes.DAMPING_PUZZLE_MUSIC, DampingMusicVolume);
            EventManager.StopListening(SoundEventTypes.PLAY_SOUNDTRACK, PlaySoundtrack);
        }

        private void FillDictionary() {
            for (int i = 0; i < soundtrackModel.soundtracks.Length; i++)
                soundtracks[soundtrackModel.soundtracks[i].key] = soundtrackModel.soundtracks[i].value;
        }

        private void DampingMusicVolume(object[] percentage) {
            float newVolume = (volume / 100.0f) * (int)percentage[0];

            if (currentDampen != null)
                StopCoroutine(currentDampen);

            if (activeSource != null)
                currentDampen = StartCoroutine(DampenMusicVolume(newVolume));
        }

        /// <summary>
        /// Lowers the music volume to the new desired volume
        /// </summary>
        /// <param name="newVolume">(float)volume</param>
        private IEnumerator DampenMusicVolume(float newVolume) {
            if (activeSource.volume > newVolume) {
                while (activeSource.volume > newVolume) {
                    activeSource.volume -= Time.deltaTime * 0.5f;
                    yield return null;
                }
            } else {
                while (activeSource.volume < newVolume) {
                    activeSource.volume += Time.deltaTime * 0.5f;
                    yield return null;
                }
            }

            activeSource.volume = newVolume;
        }

        /// <summary>
        /// Plays a single clip - if another clip is being played already it will be muted (by fade)
        /// </summary>
        public void PlaySoundtrack(object[] arg) {
            AudioClip clip = soundtracks[(SOUNDTRACK_TYPES)arg[0]];
            float fadeTime = (float)arg[1];
            bool shouldLoop = (bool)arg[2];
            bool forceRestart = (bool)arg[3];

            if (CheckIfTrackPlaying(clip) && !forceRestart)
                return;

            AudioSource source = audioSource;

            source.clip = clip;
            source.loop = shouldLoop;

            if (currentFadeIn != null)
                StopCoroutine(currentFadeIn);

            if (activeSource != null)
                StartCoroutine(FadeOut(activeSource, fadeTime));

            currentFadeIn = StartCoroutine(FadeIn(source, fadeTime));
        }

        /// <summary>
        /// Checks if the requested clip isnt already playing
        /// </summary>
        /// <param name="clip">clip that is requested</param>
        /// <returns>True if playing, false if not playing</returns>
        private bool CheckIfTrackPlaying(AudioClip clip) {
            if (activeSource == null)
                return false;

            if (activeSource.clip == clip)
                return true;

            return false;
        }

        /// <summary>
        /// Fades in the audio source
        /// </summary>
        /// <param name="source">Audio source to fade in</param>
        /// <param name="fadeTime">time it takes to fade in</param>
        private IEnumerator FadeIn(AudioSource source, float fadeTime) {
            activeSource = source;

            source.volume = 0;

            if (!source.isPlaying)
                source.Play();

            while (source.volume < volume) {
                source.volume += volume * Time.deltaTime / fadeTime;
                yield return null;
            }

            source.volume = volume;

            currentFadeIn = null;
        }

        /// <summary>
        /// Fades out the audio source
        /// </summary>
        /// <param name="source">source to fade out</param>
        /// <param name="fadeTime">time it takes to fade out</param>
        private IEnumerator FadeOut(AudioSource source, float fadeTime) {
            float startVolume = source.volume;

            while (source.volume > 0) {
                source.volume -= startVolume * Time.deltaTime / fadeTime;
                yield return null;
            }

            source.Stop();
        }
    }
}
