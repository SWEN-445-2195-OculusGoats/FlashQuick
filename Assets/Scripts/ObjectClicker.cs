using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ObjectClicker : MonoBehaviour
{
    void Update(){
        if(Input.GetMouseButtonDown(0)){
            RaycastHit hit = new RaycastHit();
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            print(Physics.Raycast (ray, out hit));

            if(Physics.Raycast (ray, out hit)){
                print("B latt!");
                if(hit.transform != null){
                    PrintName(hit.transform.gameObject);
                    SceneManager.LoadScene("QuizSelectScreen");
                }
            }
        }
    }

    void PrintName(GameObject go){
        print(go.name);
    }
}
