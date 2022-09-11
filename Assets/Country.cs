using cakeslice;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Country : MonoBehaviour {
    public string Name { get { return _name; } set { _name = value; } }
    private string _name;

    public int Resources = 0;

    private List<Structure> _structures = new List<Structure>();
    private List<Unit> _reserveUnits = new List<Unit>();
    private List<Unit> _activeUnits = new List<Unit>();

    private Outline _outline;

    void Awake() {
        //Create a name for teh country
        _name = RandomNameGenerator.GenerateName();
        //Get it's outline
        _outline = GetComponent<Outline>();

        //Start listening for the game to start
        GameManager.Instance.StartGameEvent.AddListener(OnStartGame);
        GameManager.Instance.HideOutlineEvent.AddListener(HideCountryOutline);
    }

    /// <summary>
    /// Used to select this country when clicking on it's gameobject
    /// </summary>
    private void OnMouseDown() {
        if(EventSystem.current.IsPointerOverGameObject())
            return;

        GameManager.Instance.ShowCountryOutline(this);

        //Select this country
        FindObjectOfType<LocalPlayer>().SelectCountry(this);
    }

    /// <summary>
    /// Triggered when the game starts
    /// </summary>
    public void OnStartGame() {
        StartCoroutine(GenerateResources());

        CreateStructure("townhall");
    }

    /// <summary>
    /// Called once to begin generating resources
    /// </summary>
    IEnumerator GenerateResources() {
        while(true) {
            Resources += 10;
            yield return new WaitForSeconds(1);
        }
    }

    public void GiveResources(int amount) {
        Resources += amount;
    }

    public void RemoveResources(int amount) {
        Resources -= amount;
    }

    public void CreateUnit(string unitId) {
        //Get data about this unit from the UnitDatabase
        UnitData data = BuildingDatabase.Instance.GetUnitFromId(unitId);
        if(data == null)
            return;
         
        //Make sure you can afford it
        if(Resources < data.unitCost)
            return;

        //Check for required structure to build this unit type
        if(data.requiresStructure != string.Empty) {
            //Make sure you have a structure that can create this unit
            if(!HasStructure(data.requiresStructure))
                return;
        }

        //Create the unit
        RemoveResources(data.unitCost);
        GameObject unitGameObject = Instantiate(data.unitPrefab, transform.position + data.offset, Quaternion.identity);
        Unit unit = unitGameObject.GetComponent<Unit>();

        //Add it to your reserves
        _reserveUnits.Add(unit);
    }

    public void CreateStructure(string structureId) {
        StructureData data = BuildingDatabase.Instance.GetStructureFromId(structureId);
        if(data == null)
            return;

        //Make sure you can afford it
        if(Resources < data.structureCost)
            return;

        //If you have reached the max number of this structure type, don't create it
        if(AmountOfStructures(structureId) >= data.maxType)
            return;

        //Create the structure
        RemoveResources(data.structureCost);
        GameObject structureGameObject = Instantiate(data.structurePrefab, transform.position + data.offset, Quaternion.identity);
        Structure structure = structureGameObject.GetComponent<Structure>();
        structure.StructureId = data.structureId;

        //Add it to your structures
        _structures.Add(structure);
    }

    public bool HasStructure(string structureId) {
        foreach(Structure structure in _structures) {
            if(structure.StructureId == structureId)
                return true;
        }

        return false;
    }

    public int AmountOfStructures(string structureId) {
        int amount = 0;

        foreach(Structure structure in _structures) {
            if(structure.StructureId == structureId)
                amount++;
        }

        return amount;
    }

    public void ShowCountryOutline() {
        //Figure out what color to set the outline to
        if(!GameManager.Instance.GameStarted) {
            _outline.color = 2;
        } else if (FindObjectOfType<LocalPlayer>().Country == this) {
            _outline.color = 1;
        } else {
            _outline.color = 0;
        }

        //Show the outline
        _outline.eraseRenderer = false;
    }

    private void HideCountryOutline() {
        _outline.eraseRenderer = true;
    }

    public void AttackCountry(Country country) {
        Debug.Log("Attack started. Units in reserve: " + _reserveUnits.Count);

        StartCoroutine(SlowReleaseTroops(country));
    }

    private void UnitAttack(Unit unit, Transform target) {
        _reserveUnits.Remove(unit);
        _activeUnits.Add(unit);

        unit.WalkToTarget(target);
    }

    private IEnumerator SlowReleaseTroops(Country country) {
        //Reverse 'for' iteration so you can remove items from the list
        for(int i = _reserveUnits.Count - 1; i >= 0; i--) {
            UnitAttack(_reserveUnits[i], country.transform);
            yield return new WaitForSeconds(.25f);
        }
    }
}