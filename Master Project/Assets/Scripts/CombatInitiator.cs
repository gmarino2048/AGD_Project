using System;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Used to go between cooking scenes appropriately
/// </summary>
public class CombatInitiator : MonoBehaviour
{
    private readonly string _COMBAT_SCENE_NAME = "CombatScene";

    public Guid MonsterID { get; private set; }
    public float InitialManagerMeterValue { get; private set; }

    /// <summary>
    /// Goes to the combat scene with the needed data for the next scene
    /// </summary>
    /// <param name="monsterId">The ID of the monster that is fighting the player</param>
    /// <param name="initialManagerMeterValue">The initial value for the manager meter</param>
    public void InitiateCombat(Guid monsterId, float initialManagerMeterValue)
    {
        MonsterID = monsterId;
        InitialManagerMeterValue = initialManagerMeterValue;
        SceneManager.LoadScene(_COMBAT_SCENE_NAME);
    }
}