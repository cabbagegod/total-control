using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CountryInfoUI : MonoBehaviour {
    [SerializeField] private GameObject _selectCountryBttn;
    [SerializeField] private GameObject _attackCountryBttn;
    [SerializeField] private TextMeshProUGUI _nameText;
    private Country _country;

    // Start is called before the first frame update
    void Start() {

    }

    // Update is called once per frame
    void Update() {

    }

    /// <summary>
    /// Show info about a country
    /// </summary>
    /// <param name="country">The country you wish to show</param>
    /// <param name="playersCountry">The local player's country</param>
    public void Show(Country country, Country playersCountry) {
        Hide();

        gameObject.SetActive(true);
        _country = country;
        _nameText.text = country.Name;

        if(country == playersCountry)
            return;

        //If the game has started then show the attack button, otherwise show the claim button
        if(GameManager.Instance.GameStarted) {
            _attackCountryBttn.SetActive(true);
        } else {
            _selectCountryBttn.SetActive(true);
        }
    }

    public void Hide() {
        _selectCountryBttn.SetActive(false);
        _attackCountryBttn.SetActive(false);
        gameObject.SetActive(false);
    }

    public void SelectCountry() {
        FindObjectOfType<LocalPlayer>().SetCountry(_country);
        Hide();
    }

    public void AttackCountry() {
        FindObjectOfType<LocalPlayer>().Country.AttackCountry(_country);
        Hide();
    }
}