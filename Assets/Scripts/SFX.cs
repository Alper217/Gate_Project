using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFX : MonoBehaviour
{
    public AudioSource src;
    [SerializeField] private AudioClip sfx1;

       void OnMouseDown()
{
        src.clip = sfx1;
        src.Play();
}
}
