using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reason : MonoBehaviour {

    public ManagerBar bar;

    public void reason()
    {
        bar.ChangeManagerBar(-10);
    }
}
