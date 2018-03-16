using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositionController : MonoBehaviour
{
    public GameObject positionCaravan1;
    public GameObject positionCaravan2;
    public GameObject positionCaravan3;
    public GameObject positionPlayer;

    // Use this for initialization
    void Start()
    {
        GameObject[] caravans = GameObject.FindGameObjectsWithTag("carriage");
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        player.transform.position = positionPlayer.transform.position;
        caravans[0].transform.position = positionCaravan1.transform.position;
        caravans[1].transform.position = positionCaravan2.transform.position;
        caravans[2].transform.position = positionCaravan3.transform.position;
    }

}
