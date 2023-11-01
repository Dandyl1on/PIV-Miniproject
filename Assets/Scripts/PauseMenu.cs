using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public GameObject text1;

    public GameObject Player;
    // Start is called before the first frame update
    void Start()
    {
        text1.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Cancel"))
        {
            print("P");
            Time.timeScale = 0;

        }
        
    }
}
