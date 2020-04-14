using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StatsScreenScript : MonoBehaviour
{
    // base string paths
    public static string BASE_PATH_TEXT = "arch_with_stats/Canvas/";
    public static string BASE_PATH_JSON = "Assets/Quizzes/";
    // JSON object
    public dynamic values;

    void LoadJson()
    {
        using (StreamReader r = new StreamReader(BASE_PATH_JSON + "game_data.json"))
        {
            string json = r.ReadToEnd();
            values = JsonConvert.DeserializeObject(json);
            Debug.Log("JSON loaded for game_data.json");
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Loading statistics control");
        LoadJson();
        Text q_correct = GameObject.Find(BASE_PATH_TEXT + "Correct_C_T").GetComponent<Text>();
        Text q_incorrect = GameObject.Find(BASE_PATH_TEXT + "Incorrect_C_T").GetComponent<Text>();
        Text title = GameObject.Find(BASE_PATH_TEXT + "Title_T").GetComponent<Text>();
        Text stat = GameObject.Find(BASE_PATH_TEXT + "Stat_T").GetComponent<Text>();
        Text percent = GameObject.Find(BASE_PATH_TEXT + "Sub_data_S").GetComponent<Text>();
        q_correct.text = values.questions_correct;
        stat.text = "Question Review:\n";
        title.text = values.q_title;
        int correct = values.questions_correct;
        int total = values.question_number;
        int incorrect = total - correct;
        q_incorrect.text = incorrect.ToString();
        double per = (double)correct / total;
        Debug.Log("PERCENT: " + per);
        percent.text =  "Result: " + (per * 100).ToString() + "%";
    }

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
