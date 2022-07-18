using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class EncounterHistory
{
    public enum Encounters
    {
        Test,
        Test2,
        DREAM_MACHINE_1_ENCOUNTER_1,
        DREAM_MACHINE_1_ENCOUNTER_2,
        DREAM_MACHINE_1_ENCOUNTER_3,
        DREAM_MACHINE_2_ENCOUNTER_1,
        DREAM_MACHINE_2_ENCOUNTER_2,
        DREAM_MACHINE_2_ENCOUNTER_3,
        DREAM_MACHINE_3_ENCOUNTER_1,
        DREAM_MACHINE_3_ENCOUNTER_2,
        DREAM_MACHINE_3_ENCOUNTER_3,
        SHEPHERD_ENCOUNTER
    }

    private static List<Encounters> encountersFinished;
    public static List<Encounters> EncountersFinished
    {
        get
        {
            if (encountersFinished == null)
            {
                RetrieveEncountersFromSave();
            }

            return encountersFinished;
        }
    }

    private static void RetrieveEncountersFromSave()
    {
        encountersFinished = PlayerProgression.GetPlayerData<List<Encounters>>(SaveKeys.COMBAT_ENCOUNTERS_COMPLETED);
    }

    public static void SaveEncounterAsFinished(Encounters encounter)
    {
        EncountersFinished.Add(encounter);
        PlayerProgression.UpdatePlayerData(SaveKeys.COMBAT_ENCOUNTERS_COMPLETED, EncountersFinished);
        PlayerProgression.SaveLoadedData();
    }
}
