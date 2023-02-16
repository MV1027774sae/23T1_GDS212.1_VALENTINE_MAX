using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UserInterface : MonoBehaviour
{
    [SerializeField] private GameObject retryButton;

    public void RestartGame()
    {
        SceneManager.LoadScene(0);
    }

}
