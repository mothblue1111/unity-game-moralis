using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class logincontroller : MonoBehaviour
{
    public void OnSigninButtonClicked()
    {
        SceneManager.LoadScene(1);
    }
}
