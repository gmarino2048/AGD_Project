using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class ScorekeeperBehavior : MonoBehaviour
{

    #region Parameters

    [SerializeField]
    public ChopManager ChopManager;

    public float TimeInSeconds;

    Text TimerText;
    Button FinishButton;

    float CurrentTime;
    bool TimerActive;
    bool TimerDone;

    #endregion

    #region MonoBehaviour

    void Start()
    {
        TimerText = GetComponentInChildren<Text>();
        FinishButton = GetComponentInChildren<Button>();

        CurrentTime = TimeInSeconds;
        TimerActive = true;
        TimerDone = false;
    }


    void Update()
    {
        if (CurrentTime > 0 && TimerActive)
        {
            TimerText.text = ((int)CurrentTime).ToString();
            CurrentTime -= Time.deltaTime;
        }
        else if (TimerActive)
        {
            TimerDone = true;
            TimerActive = false;
        }

        if (TimerDone)
        {
            Score();
            TimerDone = false;
        }
    }

    #endregion

    #region Auxiliary

    float Score () {
        float average = AverageDistance(SortChops(ChopManager));
        Debug.Log(average);
        return average;
    }

    List<float> SortChops (ChopManager manager){
        List<float> chops = new List<float>();

        manager.AlreadyChopped.ForEach(chop => chops.Add(chop.ActualPosition));

        chops.Sort();

        return chops;
    }

    float AverageDistance(List<float> values)
    {
        if (values.Count > 1)
        {
            int current = 1;
            int total = values.Count;

            float average = values[0];

            while (current < total)
            {
                float val = values[current];
                average = ((average * (current - 1)) + val) / current;
                current++;
            }

            return average;
        }
        return 0;
    }

    #endregion
}
