using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Controls core game logic
/// </summary>
public class GameManager : Singleton<GameManager> {
    [SerializeField] private GameObject _aiPlayerPrefab;
    public bool GameStarted { get; private set; }
    public UnityEvent StartGameEvent { get { return _startGameEvent; } }
    private UnityEvent _startGameEvent = new UnityEvent();
    public UnityEvent HideOutlineEvent { get { return _hideOutlineEvent; } }
    private UnityEvent _hideOutlineEvent = new UnityEvent();

    List<Player> players = new List<Player>();
    Country[] _countries = null;

    // Start is called before the first frame update
    void Start() {
        //Get all the countries in the scene
        _countries = FindObjectsOfType<Country>();

        //Add the local player to the list of players
        players.Add(FindObjectOfType<LocalPlayer>());
    }

    // Update is called once per frame
    void Update() {

    }

    public void StartGame() {
        if(GameStarted)
            return;
        GameStarted = true;

        //Get the local player's country so we dont add an AI to it
        Country localPlayerCountry = players[0].Country;

        int countryIndex = 0;
        //Create an AI player for all the other countries
        for(int i = 0; i < _countries.Length - 1; i++) {
            //Create the AI
            GameObject playerGameObject = Instantiate(_aiPlayerPrefab);
            Player player = playerGameObject.GetComponent<Player>();
            players.Add(player);

            //Assign them a country, make sure it isn't the local player's
            if(_countries[countryIndex] == localPlayerCountry)
                countryIndex++;
            player.Country = _countries[countryIndex];
        }

        _startGameEvent?.Invoke();
    }

    public void ShowCountryOutline(Country country) {
        _hideOutlineEvent?.Invoke();

        country.ShowCountryOutline();
    }
}