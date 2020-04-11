using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OptionsScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update(){

        if (Input.GetMouseButtonDown(0)){
            RaycastHit hit = new RaycastHit();
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if(Physics.Raycast (ray, out hit)){
                if(hit.transform != null){
                    PrintName(hit.transform.gameObject);
                    if (hit.transform.gameObject.name == "QuitButton"){
                        SceneManager.LoadScene("StartScreen");
                    }else if(hit.transform.gameObject.name == "ResumeButton"){
                        SceneManager.LoadScene("GameScene");
                        //Right now game starts over, does this matter for us?
                    }else if(hit.transform.gameObject.name == "StartOverButton"){
                        SceneManager.LoadScene("GameScene");
                    }else if(hit.transform.gameObject.name == "OptionsButton"){
                        SceneManager.LoadScene("GameOptionsScene");
                    }
                    
                }  
            }else{
                print("Nada");
            }
        } 
    }

    void PrintName(GameObject go){
        print(go.name);
    }
	
}
