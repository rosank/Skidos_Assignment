using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CustomButton : Button {
    private readonly Vector2 defaultPosition = new Vector2(32, 32);
    private RectTransform rectTransform;

    protected override void Awake() {
        base.Awake();
        rectTransform = GetComponent<RectTransform>();
    }

    protected override void DoStateTransition(SelectionState state, bool instant) {
        base.DoStateTransition(state, instant);
        if(state == SelectionState.Normal || state == SelectionState.Highlighted) {
            rectTransform.anchoredPosition = defaultPosition;
        } else if(state == SelectionState.Pressed || state == SelectionState.Disabled) {
            rectTransform.anchoredPosition = Vector2.zero;
        }
    }
}
