using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StatsScreenScript : MonoBehaviour
{
    

    // Update is called once per frame
    void Update(){

        if (Input.GetMouseButtonDown(0)){
            RaycastHit hit = new RaycastHit();
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if(Physics.Raycast (ray, out hit)){
                if(hit.transform != null){
                    PrintName(hit.transform.gameObject);
                    if (hit.transform.gameObject.name == "MainMenuButton"){
                        SceneManager.LoadScene("StartScreen");
                    }else if(hit.transform.gameObject.name == "RestartButton"){
                        SceneManager.LoadScene("GameScene");
                    }
                    
                }  
            }
        } 
    }

    void PrintName(GameObject go){
        print(go.name);
    }
}
