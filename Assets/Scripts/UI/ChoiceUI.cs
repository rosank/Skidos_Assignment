using UnityEngine;
using UnityEngine.UI;

public class ChoiceUI : MonoBehaviour {
    [SerializeField] Text language;
    [SerializeField] Text votes;

    public void Set(string l, string v) {
        this.language.text = l;
        this.votes.text = v;
    }

}
