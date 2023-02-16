using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lich : MonoBehaviour
{
    [SerializeField] private GameObject winText;
    [SerializeField] private GameObject lich;

    private void Start()
    {
        winText.SetActive(false);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        winText.SetActive(true);
        Destroy(lich);
    }
}
