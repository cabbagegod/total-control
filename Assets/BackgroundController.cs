using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// Used on the background to control hiding the current selection when clicking it
/// </summary>
public class BackgroundController : MonoBehaviour {
    private void OnMouseDown() {
        if(EventSystem.current.IsPointerOverGameObject())
            return;

        GameManager.Instance.HideOutlineEvent?.Invoke();
        FindObjectOfType<LocalPlayer>().HideActiveUI();
    }
}