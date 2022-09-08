using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HUDManager : MonoBehaviour {
    [SerializeField] private TextMeshProUGUI _resourcesText;
    private Country _country;

    void Update() {
        if(_country != null) {
            _resourcesText.text = $"Resources: {_country.Resources}";
        }
    }
    
    public void SetCountry(Country country) {
        _country = country;
    }

    public void PurchaseUnit(string unitId) {
        _country.CreateUnit(unitId);
    }

    public void PurchaseStructure(string structureId) {
        _country.CreateStructure(structureId);
    }
}
