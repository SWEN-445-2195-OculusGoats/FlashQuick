using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class QuizSelectScript : MonoBehaviour
{
    void Update(){
        if(Input.GetMouseButtonDown(0)){
            RaycastHit hit = new RaycastHit();
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if(Physics.Raycast (ray, out hit)){
                if(hit.transform != null){
                    PrintName(hit.transform.gameObject);
                    if (hit.transform.gameObject.name == "HarryBox")
                    {
                        PlayerPrefs.SetString("QuizType","harry.json");
                    }
                    else if (hit.transform.gameObject.name == "BioBox")
                    {
                        PlayerPrefs.SetString("QuizType","biology.json");
                    }
                    else if (hit.transform.gameObject.name == "ChemBox")
                    {
                        PlayerPrefs.SetString("QuizType","chemistry.json");
                    }
                    SceneManager.LoadScene("GameInstructionsScene");
                }
            }
        }
    }


    void PrintName(GameObject go){
        print(go.name);
    }
}
