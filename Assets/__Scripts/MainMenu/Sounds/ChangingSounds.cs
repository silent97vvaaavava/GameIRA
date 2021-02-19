using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangingSounds : MonoBehaviour
{
    //voice
    [SerializeField] AudioSource Main;
    [SerializeField] AudioClip localMenuSounds;
    [SerializeField] AudioClip Menu;

    //gameObjects
    [SerializeField] GameObject localMenu;


    public void ChangedSoundLocal()
    {
        Main.clip = localMenuSounds;
        Main.Play();
    }

    public void ChangedSoundMain()
    {
        Main.clip = Menu;
        Main.Play();
    }
}
