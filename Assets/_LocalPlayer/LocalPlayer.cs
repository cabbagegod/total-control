using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocalPlayer : Player {
    //The local player handles the UI because there is no UI Manager
    [SerializeField] private HUDManager _hudManager;
    [SerializeField] private CountryInfoUI _countryInfoUI;
    [SerializeField] private GameObject _selectionCanvasPrefab;
    public SelectionCanvas SelectionCanvas { get; private set; }

    // Start is called before the first frame update
    void Start() {
        SelectionCanvas = Instantiate(_selectionCanvasPrefab).GetComponent<SelectionCanvas>();
    }

    public void SelectCountry(Country country) {
        _countryInfoUI.Show(country, Country);
    }

    public void SetCountry(Country country) {
        Country = country;

        GameManager.Instance.StartGame();

        _hudManager.SetCountry(country);
        _hudManager.gameObject.SetActive(true);
    }

    public void HideActiveUI() {
        _countryInfoUI.Hide();
    }
}