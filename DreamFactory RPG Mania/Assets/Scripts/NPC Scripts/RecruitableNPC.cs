using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecruitableNPC : MonoBehaviour
{
    [Header("Party Member Config")]
    [SerializeField] private PlayerPartyMemberConfig partyMemberToRecruit;

    public void RecruitPartyMember()
    {
        PlayerPartyManager.UnlockPartyMemberById(partyMemberToRecruit);
        PlayerProgression.SaveLoadedData();

        return;
    }
}
