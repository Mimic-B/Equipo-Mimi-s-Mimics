using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ClaseLunes : MonoBehaviour
{
    public TMP_InputField usrInput;

    // Start is called before the first frame update
   public void SaveCurrentData()
    {
        Debug.Log(usrInput.text);
        PlayerPrefs.SetString("Testsave", usrInput.text);
    }

    // Update is called once per frame
    void Start()
    {
        string data = PlayerPrefs.GetString("TestSave", "");
        usrInput.text = data;
        Debug.Log(data);
    }
}
