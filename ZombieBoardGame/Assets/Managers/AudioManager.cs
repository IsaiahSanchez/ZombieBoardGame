using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    [SerializeField] private AudioObject[] audioclips;

    private void Awake()
    {
        instance = this;
    }


    public void playSound(string soundName, Vector2 locationToPlay)
    {
        foreach (AudioObject audio in audioclips)
        {
            if (audio.name == soundName)
            {
                Instantiate(audio, locationToPlay, Quaternion.identity);
            }
        }
    }
}
