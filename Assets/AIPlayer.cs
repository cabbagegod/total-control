using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class AIPlayer : Player {
    void Start() {
        PerformActions();
    }

    private async void PerformActions() {
        while(true) {
            if(Country == null) {
                await Task.Delay(2500);
                continue;
            }

            int action = Random.Range(0, 100);
            if(action >= 50) {
                await DoRandomAction();
            } else {
                await DoLogicalAction();
            }
        }
    }

    private async Task DoLogicalAction() {
        if(!GameManager.Instance.IsBeingAttacked(Country)) {
                
        }
        
        await CreateArmy();
    }

    private async Task DoRandomAction() {
        int action = Random.Range(0, 100);

        if(action >= 50) {
            //Try to create units
            await CreateArmy();
        } else if(action >= 40) {
            //Do nothing
            await Task.Delay(5000);
        } else {

        }

        await Task.Delay(100);
    }

    private async Task CreateArmy() {
        if(Country.Resources >= 500) {
            bool hasAirbase = Country.HasStructure("airbase");
            if(hasAirbase) {
                Country.CreateUnit("jet");
                Country.CreateUnit("jet");

                await Task.Delay(10000);
            } else {
                Country.CreateStructure("airbase");
            }
        } else if (Country.Resources >= 200) {
            Country.CreateUnit("Tank");
            Country.CreateUnit("Tank");
        } else if(Country.Resources >= 100) {
            Country.CreateUnit("Tank");
        } else {
            await Task.Delay(10000);
        }

        await Task.Delay(5000);
    }
}
