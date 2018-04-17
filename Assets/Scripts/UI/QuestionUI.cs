using UnityEngine;
using UnityEngine.UI;

public class QuestionUI : MonoBehaviour {
    [SerializeField] Text question;
    [SerializeField] Text lastUpdate;
    [SerializeField] Transform choiceRoot;
    [SerializeField] ScrollRect scrollRect;
    [SerializeField] GameObject scrollArrow;
    
    public string Question {
        set {
            question.text = value;
        }
    }

    public string LastUpdateOn {
        set {
            lastUpdate.text = "Last Update: " + value;
        }
    }

    public Transform ChoiceRoot {
        get {
            return choiceRoot;
        }
    }

    public void UpdateScrollRect() {
        scrollRect.horizontal = choiceRoot.childCount > 4;
        scrollArrow.SetActive(choiceRoot.childCount > 4);
    }
}
