using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class end_control : MonoBehaviour
{
    // base string paths
    public static string BASE_PATH_TEXT = "arch_with_end/Canvas/";
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
        Debug.Log("Loading end control");
        LoadJson();
        Text q_correct = GameObject.Find(BASE_PATH_TEXT + "Correct_C_T").GetComponent<Text>();
        Text q_remaining = GameObject.Find(BASE_PATH_TEXT + "Remaining_C_T").GetComponent<Text>();
        Text title = GameObject.Find(BASE_PATH_TEXT + "Title_T").GetComponent<Text>();
        Text header = GameObject.Find(BASE_PATH_TEXT + "Header_T").GetComponent<Text>();
        Text fraction = GameObject.Find(BASE_PATH_TEXT + "Sub_data_S").GetComponent<Text>();
        Text percent = GameObject.Find(BASE_PATH_TEXT + "Percent_T").GetComponent<Text>();
        q_remaining.text = values.questions_remaining;
        q_correct.text = values.questions_correct;
        title.text = values.q_title;
        int correct = values.questions_correct;
        int total = values.question_number;
        double per = (double)correct / total;
        Debug.Log("PERCENT: " + per);
        if (per >= .7)
        {
            header.text = "Congratulations!";
        }
        else
        {
            header.text = "Good Try! Try again?";
        }
        fraction.text = values.questions_correct.ToString() + "/" + values.question_number.ToString();
        percent.text = (per * 100).ToString() + "%";
    }
}
