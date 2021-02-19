using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonSounds : MonoBehaviour
{
    [SerializeField] AudioClip button;

    public void PlaySound()
    {
        gameObject.AddComponent<AudioSource>().clip = button;

    }
}
