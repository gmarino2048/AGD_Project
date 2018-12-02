using GameNarrative;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml;
using System.Xml.Serialization;
using UnityEngine;

public class GameNarrativeManager : MonoBehaviour
{
    private readonly string _GAME_NARRATIVE_XML_PATH = "Data/GameNarrative.xml";

    /// <summary>
    /// The monsters that were satisfied during their visit
    /// </summary>
    public List<Guid> DateableMonsterIDs { get; private set; }

    /// <summary>
    /// The current stage
    /// </summary>
    public Stage CurrentStage { get; private set; }

    /// <summary>
    /// The overall narrative
    /// </summary>
    private Narrative _Narrative { get; set; }

    /// <summary>
    /// The stages of the narrative in a queue
    /// </summary>
    private Queue<Stage> _StagesQueue { get; set; }

    public void Start()
    {
        _Narrative = LoadNarrative();
        _StagesQueue = new Queue<Stage>(_Narrative.Stages);
        DateableMonsterIDs = new List<Guid>();
    }

    /// <summary>
    /// Tells whether any stages are left to play through
    /// </summary>
    /// <returns>True if there are any stages left, false otherwise</returns>
    public bool AnyStagesLeft()
    {
        return _StagesQueue.Any();
    }

    /// <summary>
    /// Dequeues the next stage
    /// </summary>
    public void StartNextStage()
    {
        if (!_StagesQueue.Any())
        {
            throw new Exception("No stages left to dequeue");
        }

        CurrentStage = _StagesQueue.Dequeue();
    }

    /// <summary>
    /// Gets the appropriate monologue and returns its entries
    /// </summary>
    /// <returns>A list of strings, each being one entry</returns>
    public GameNarrative.Monologue GetAppropriateMonologue()
    {
        foreach (var monologue in _Narrative.Monologues)
        {
            if (monologue.IsForEnd == !AnyStagesLeft() && (AnyStagesLeft() || monologue.IsForWin == DateableMonsterIDs.Any()))
            {
                return monologue;
            }
        }

        return null; // Shouldn't ever get here
    }

    private Narrative LoadNarrative()
    {
        var xmlFilePath = Path.Combine(Application.streamingAssetsPath, _GAME_NARRATIVE_XML_PATH);

        using (var fileStream = new FileStream(xmlFilePath, FileMode.Open))
        {
            var xmlSerializer = new XmlSerializer(typeof(Narrative));
            return xmlSerializer.Deserialize(fileStream) as Narrative;
        }
    }
}