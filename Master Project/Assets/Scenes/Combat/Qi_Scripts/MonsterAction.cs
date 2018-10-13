using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MonsterAction : MonoBehaviour {

    public enum Monster {Nessie, Cerberus, REDACTED};
    public Animator Hitted;

    public void MonsterHitted()
    {
        Hitted.SetTrigger("MonsterHitted");
    }
}
