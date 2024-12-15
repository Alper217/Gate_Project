using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXCanvas : MonoBehaviour
{
    public AudioSource src;
    [SerializeField] private AudioClip sfx1, sfx2;

    public void button1()
    {
        src.clip = sfx1;
        src.Play();
    }

    public void button2()
    {    
        src.clip = sfx2;
        src.Play();
    }
}
