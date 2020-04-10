using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class back_to_paused : MonoBehaviour{
    // Start is called before the first frame update
    void Start(){}

    void Update(){
        if(Input.GetMouseButtonDown(0)){
            RaycastHit hit = new RaycastHit();
            Ray ray = Camera.main.ScreenPointToRay(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 100.0f));
            print(Physics.Raycast (ray, out hit));

            if(Physics.Raycast (ray, out hit)){
                print("B latt!");
                if(hit.transform != null){
                    PrintName(hit.transform.gameObject);
                    SceneManager.LoadScene("GamePausedScene");
                }
            }
        }
    }

    void PrintName(GameObject go){
        print(go.name);
    }
    /*
    // Update is called once per frame
    void Update(){
        if(Input.GetMouseButtonDown(0)){
            var hit : RaycastHit;
            var ray : Ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            var select = GameObject.FindWithTag("select").transform;

            if(Physics.Raycast(ray, hit, 100.0)){
                select.tag = "none";
                hit.collider.transform.tag = "select";
                /*
                if(hit.transform != null){
                    //PrintName(hit.transform.gameObject);
                    SceneManager.LoadScene("GamePausedScene");
                }
            }
        }
    }


    void PrintName(GameObject go){
        print(go.name);
    }
*/
}
