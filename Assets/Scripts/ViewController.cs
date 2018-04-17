using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ViewController : MonoBehaviour {
    [SerializeField] QuestionUI questionUI;
    [SerializeField] Button btnRefresh;
    [SerializeField] GameObject choicePrefab;
    
	// Use this for initialization
	void Start () {
        JsonDownloader.instance.onJsonDownloaded.AddListener(OnJsonDownloaded);
        btnRefresh.onClick.AddListener(FetchJson);
    }

    #region UI Controller
    void UpdateUI(QuestionWrapper jsonData) {
        //handling one question for now
        Question quest = jsonData.questions[0];
        questionUI.Question = quest.question;
        questionUI.LastUpdateOn = quest.published_at;
        //create choices
        var choices = questionUI.ChoiceRoot.GetComponentsInChildren<ChoiceUI>();
        if(choices.Length == 0 || choices.Length < quest.choices.Length) {
            CreateChoice(quest.choices.Length - choices.Length, questionUI.ChoiceRoot, ref choices);
        }
        for(int i = 0; i < choices.Length; i++) {
            choices[i].Set(quest.choices[i].choice, quest.choices[i].votes.ToString());
        }
        questionUI.UpdateScrollRect();
        btnRefresh.interactable = true;
    }

    void CreateChoice(int count, Transform parent, ref ChoiceUI[] choices) {
        List<ChoiceUI> choiceList = new List<ChoiceUI>(choices);
        for(int i = 0; i < count; i++) {
            var choice = Instantiate(choicePrefab, parent);
            choice.transform.localScale = Vector3.one;
            choiceList.Add(choice.GetComponent<ChoiceUI>());
        }
        choices = choiceList.ToArray();
    }
    #endregion

    #region Event Handles
    public void FetchJson() {
        btnRefresh.interactable = false;
        JsonDownloader.instance.FetchDataFromServer();
    }
    #endregion

    #region JsonParser
    void OnJsonDownloaded(string jsonStr) {
        if(string.IsNullOrEmpty(jsonStr)) { return; }
        UpdateUI(JsonUtility.FromJson<QuestionWrapper>(jsonStr));
    }
    #endregion

    #region Json Data
    [System.Serializable]
    struct QuestionWrapper {
        public Question[] questions;
    }

    [System.Serializable]
    struct Question {
        public string question;
        public string published_at;
        public Choice[] choices;
    }

    [System.Serializable]
    struct Choice {
        public string choice;
        public int votes;
    }
    #endregion
}
