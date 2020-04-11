using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;


[Serializable]
public class SelectedGame
{
    public string selected_game;
}

public class QuizSelectScript : MonoBehaviour
{
    // base string paths
    public static string BASE_PATH_JSON = "Assets/Quizzes/";



    void Update(){
        if(Input.GetMouseButtonDown(0)){
            RaycastHit hit = new RaycastHit();
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if(Physics.Raycast (ray, out hit)){
                if(hit.transform != null){
                    PrintName(hit.transform.gameObject);
                    if (hit.transform.gameObject.name == "HarryBox")
                    {
                        SelectedGame selection = new SelectedGame();
                        selection.selected_game = "harry.json";
                        File.WriteAllText("Assets/Quizzes/selected_quiz.json", JsonConvert.SerializeObject(selection));
                        SceneManager.LoadScene("GameScene");
                    }
                    else if (hit.transform.gameObject.name == "PhysicsBox")
                    {
                        SelectedGame selection = new SelectedGame();
                        selection.selected_game = "biology.json";
                        File.WriteAllText("Assets/Quizzes/selected_quiz.json", JsonConvert.SerializeObject(selection));
                        SceneManager.LoadScene("GameScene");
                    }
                    else if (hit.transform.gameObject.name == "ChemBox")
                    {
                        SelectedGame selection = new SelectedGame();
                        selection.selected_game = "chemistry.json";
                        File.WriteAllText("Assets/Quizzes/selected_quiz.json", JsonConvert.SerializeObject(selection));
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
