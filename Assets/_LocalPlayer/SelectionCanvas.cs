using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SelectionCanvas : MonoBehaviour {
    [SerializeField] TextMeshProUGUI _text;
    private Country _selectedCountry;

    // Start is called before the first frame update
    void Start() {
        
    }

    // Update is called once per frame
    void Update() {
        
    }

    public void Show(Country country){
        gameObject.SetActive(true);
        _selectedCountry = country;

        _text.text = $"Would you like to start the game as {_selectedCountry.Name}?";
    }

    public void Hide() {
        gameObject.SetActive(false);
    }

    public void OnClickYes() {
        FindObjectOfType<LocalPlayer>().SelectCountry(_selectedCountry);

        Destroy(gameObject);
    }
}
