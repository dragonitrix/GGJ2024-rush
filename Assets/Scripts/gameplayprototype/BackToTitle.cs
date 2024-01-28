using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BackToTitle : MonoBehaviour
{
    private void OnEnable()
    {
        SceneManager.LoadScene("sc_gametitle");
    }
}
