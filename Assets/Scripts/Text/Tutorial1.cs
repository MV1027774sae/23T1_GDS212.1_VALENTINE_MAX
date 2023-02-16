using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial1 : MonoBehaviour
{
    [SerializeField] private GameObject tutorialText;

    private void Start()
    {
        tutorialText.SetActive(false);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        tutorialText.SetActive(true);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        tutorialText.SetActive(false);
    }
}
