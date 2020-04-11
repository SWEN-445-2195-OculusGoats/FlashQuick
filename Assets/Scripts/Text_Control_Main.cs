using System.Collections;
using System.Collections.Generic;
using System;
using System.IO;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[Serializable]
public class GameInfo
{
    public int questions_remaining;
    public int questions_correct;
    public string q_title;
    public int question_number;
}


public class Text_Control_Main : MonoBehaviour
{
    // base string paths
    public static string BASE_PATH_TEXT = "arch_with_question/Canvas/";
    public static string BASE_PATH_REG = "arch_with_question/";
    public static string BASE_PATH_JSON = "Assets/Quizzes/";
    // JSON object
    public dynamic values;
    // Scoring mechanisms
    private int questions_remaining;
    private int questions_correct;
    private int current_correct_answer;
    private int question_number;
    private float current_timer;
    private bool allow_interaction;
    // Common objects where other fields are updated
    private GameObject question_bar;
    private GameObject option_1_display;
    private GameObject option_2_display;
    private GameObject option_3_display;
    private GameObject option_4_display;
    // Common text objects for reference in update
    private Text q_remaining;
    private Text q_correct;
    private Text question;
    private Text option_1;
    private Text option_2;
    private Text option_3;
    private Text option_4;
    // Material values pulled at start
    private Material Base;
    private Material Correct;
    private Material Incorrect;

    void HandleMaterials()
    {
        MeshRenderer arch_mesh = GameObject.Find("arch_with_question").GetComponent<MeshRenderer>();
        Material[] materials = arch_mesh.materials;
        foreach (Material material in materials)
        {
            if (material.name == "Material (Instance)")
            {
                Incorrect = material;
            } else if (material.name == "Material.001 (Instance)")
            {
                Correct = material;
            } else
            {
                Base = material;
            }
        }
    }

    void ChangeElementState(int id, bool display)
    {
        GameObject change_game_o = null;
        Text change_txt = null;
        switch (id)
        {
            case 1:
                change_game_o = option_1_display;
                change_txt = option_1;
                break;
            case 2:
                change_game_o = option_2_display;
                change_txt = option_2;
                break;
            case 3:
                change_game_o = option_3_display;
                change_txt = option_3;
                break;
            case 4:
                change_game_o = option_4_display;
                change_txt = option_4;
                break;
        }
        
        if (change_game_o != null && change_txt != null)
        {
            change_game_o.GetComponent<MeshRenderer>().enabled = display;
            change_txt.enabled = display;
        }
    }

    void HandleQuestionResponse(int choice)
    {
        questions_remaining -= 1;
        question_number += 1;
        q_remaining.text = questions_remaining.ToString();
        if (current_correct_answer == choice)
        {
            questions_correct += 1;
            q_correct.text = questions_correct.ToString();
            MeshRenderer mesh = question_bar.GetComponent<MeshRenderer>();
            mesh.material = Correct;
            ChangeElementState(1, false);
            ChangeElementState(2, false);
            ChangeElementState(3, false);
            ChangeElementState(4, false);
            Debug.Log("Question answered correctly");
        }
        else
        {
            Debug.Log("Question answered incorrectly");
            switch (current_correct_answer)
            {
                case 1:
                    option_1.text = "Correct: " + option_1.text;
                    ChangeElementState(2, false);
                    ChangeElementState(3, false);
                    ChangeElementState(4, false);
                    break;
                case 2:
                    option_2.text = "Correct: " + option_2.text;
                    ChangeElementState(1, false);
                    ChangeElementState(3, false);
                    ChangeElementState(4, false);
                    break;
                case 3:
                    option_3.text = "Correct: " + option_3.text;
                    ChangeElementState(1, false);
                    ChangeElementState(2, false);
                    ChangeElementState(4, false);
                    break;
                case 4:
                    option_4.text = "Correct: " + option_4.text;
                    ChangeElementState(1, false);
                    ChangeElementState(2, false);
                    ChangeElementState(3, false);
                    break;
            }
            MeshRenderer mesh = question_bar.GetComponent<MeshRenderer>();
            mesh.material = Incorrect;
        }
        StartCoroutine(HandleCountdown());
    }

    IEnumerator HandleCountdown(float countdown=5)
    {
        current_timer = countdown;
        allow_interaction = false;
        while(current_timer > 0)
        {
            Debug.Log("Counting down by 1. Current: " + current_timer);
            yield return new WaitForSeconds(1.0f);
            current_timer--;
        }
        HandleTransition();
    }

    void HandleTransition()
    {
        if (questions_remaining == 0)
        {
            Debug.Log("Quiz completed, switching scenes.");
            GameInfo gameinfo = new GameInfo();
            gameinfo.questions_remaining = questions_remaining;
            gameinfo.questions_correct = questions_correct;
            gameinfo.q_title = values.title;
            gameinfo.question_number = question_number;
            File.WriteAllText("Assets/Quizzes/game_data.json", JsonConvert.SerializeObject(gameinfo));
            SceneManager.LoadScene("EndScene");
        }
        else
        {
            Debug.Log("Moving questions. Active question #" + question_number);
            UpdateQuestion();
            allow_interaction = true;
        }
    }

    void UpdateQuestion()
    {
        MeshRenderer mesh = question_bar.GetComponent<MeshRenderer>();
        mesh.material = Base;
        question.text = values.questions[question_number].question_title;
        current_correct_answer = values.questions[question_number].correct;
        option_1.text = values.questions[question_number].question_option_1;
        option_2.text = values.questions[question_number].question_option_2;
        option_3.text = values.questions[question_number].question_option_3;
        option_4.text = values.questions[question_number].question_option_4;
        ChangeElementState(1, true);
        ChangeElementState(2, true);
        ChangeElementState(3, true);
        ChangeElementState(4, true);
    }

    void LoadJson(string quiz = "harry.json")
    {
        using (StreamReader r = new StreamReader(BASE_PATH_JSON + quiz))
        {
            string json = r.ReadToEnd();
            values = JsonConvert.DeserializeObject(json);
            Debug.Log("JSON loaded for " + quiz);
        }
    }

    void CollectObjects()
    {
        q_correct = GameObject.Find(BASE_PATH_TEXT + "Correct_C_T").GetComponent<Text>();
        question_bar = GameObject.Find(BASE_PATH_REG + "Question_Pane");
        q_remaining = GameObject.Find(BASE_PATH_TEXT + "Remaining_C_T").GetComponent<Text>();
        question = GameObject.Find(BASE_PATH_TEXT + "Question_T").GetComponent<Text>();
        option_1 = GameObject.Find(BASE_PATH_TEXT + "Q_Opt_1_T").GetComponent<Text>();
        option_2 = GameObject.Find(BASE_PATH_TEXT + "Q_Opt_2_T").GetComponent<Text>();
        option_3 = GameObject.Find(BASE_PATH_TEXT + "Q_Opt_3_T").GetComponent<Text>();
        option_4 = GameObject.Find(BASE_PATH_TEXT + "Q_Opt_4_T").GetComponent<Text>();
        option_1_display = GameObject.Find(BASE_PATH_REG + "Option1");
        option_2_display = GameObject.Find(BASE_PATH_REG + "Option2");
        option_3_display = GameObject.Find(BASE_PATH_REG + "Option3");
        option_4_display = GameObject.Find(BASE_PATH_REG + "Option4");
    }

    // Start is called before the first frame update
    void Start()
    {
        dynamic selection;
        using (StreamReader r = new StreamReader(BASE_PATH_JSON + "selected_quiz.json"))
        {
            string json = r.ReadToEnd();
            selection = JsonConvert.DeserializeObject(json);
            Debug.Log("JSON loaded for selected_quiz.json");
            Debug.Log("Quiz Selected: " + selection.selected_game);
        }
        LoadJson(selection.selected_game.ToString());
        HandleMaterials();
        CollectObjects();
        questions_correct = 0;
        question_number = 0;
        Text title = GameObject.Find(BASE_PATH_TEXT + "Title_T").GetComponent<Text>();
        title.text = values.title;
        q_remaining.text = values.num_questions;
        questions_remaining = values.num_questions;
        UpdateQuestion();
        allow_interaction = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (!allow_interaction)
        {
            return;
        }
        if (Input.GetKeyDown(KeyCode.Keypad1) || Input.GetKeyDown(KeyCode.Alpha1))
        {
            HandleQuestionResponse(1);
        }
        else if (Input.GetKeyDown(KeyCode.Keypad2) || Input.GetKeyDown(KeyCode.Alpha2))
        {
            HandleQuestionResponse(2);
        }
        else if (Input.GetKeyDown(KeyCode.Keypad3) || Input.GetKeyDown(KeyCode.Alpha3))
        {
            HandleQuestionResponse(3);
        }
        else if (Input.GetKeyDown(KeyCode.Keypad4) || Input.GetKeyDown(KeyCode.Alpha4))
        {
            HandleQuestionResponse(4);
        }
    }
}
