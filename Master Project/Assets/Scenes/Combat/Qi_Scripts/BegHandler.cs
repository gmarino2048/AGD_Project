using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BegHandler : MonoBehaviour {

    public ManagerBar bar;
    public GameObject beg;
    public bool begshown;
    private void Awake()
    {
        beg.SetActive(false);
        begshown = false;
    }
    
    public void Beg()
    {
        bar.ChangeManagerBar(-35);
        begshown = true;
        beg.SetActive(false);
    }
}
