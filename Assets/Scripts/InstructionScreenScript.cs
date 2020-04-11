using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

[Serializable]
public class SelectedGame{
    public string selected_game;
}

public class InstructionScreenScript : MonoBehaviour
{
    // base string paths
    public static string BASE_PATH_JSON = "Assets/Quizzes/";

    // Start is called before the first frame update
    void Start(){
        String e = PlayerPrefs.GetString("QuizType","");  
        SelectedGame selection = new SelectedGame();
        selection.selected_game = e;
        File.WriteAllText("Assets/Quizzes/selected_quiz.json", JsonConvert.SerializeObject(selection));
    }

    void Update(){
        if(Input.GetKeyDown (KeyCode.Return)){
            SceneManager.LoadScene("GameScene");
        }
    }



}
