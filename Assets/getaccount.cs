using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class getaccount : MonoBehaviour
{

    public Text myaccount;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    public void getaddress()
    {
        string account = PlayerPrefs.GetString("Account");

        myaccount.text = account;
    }
}
