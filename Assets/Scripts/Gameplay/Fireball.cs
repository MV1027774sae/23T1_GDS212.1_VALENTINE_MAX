using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : MonoBehaviour
{
    [SerializeField] AudioSource soundManager;
    [SerializeField] AudioClip sizzle;
    void Start()
    {
        Destroy(gameObject, 5f);
        soundManager.PlayOneShot(sizzle);
    }
}
