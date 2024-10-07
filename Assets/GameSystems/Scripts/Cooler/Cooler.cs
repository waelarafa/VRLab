using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cooler : MonoBehaviour
{
    [SerializeField] private GameObject[] bottleArray = new GameObject[12];


    public GameObject[] BottleArray
    {
        get => bottleArray;
        set => bottleArray = value;
    }
}
