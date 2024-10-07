using UnityEngine;
using System.Collections;
public class WaterSampleBox : IGlobalSingleton<WaterSampleBox>
{
    [SerializeField] private Transform[] waterSampleSpots;

    private int availableSpotIndex;

    public void AddWaterBottle(GameObject waterBottle)
    {
        if (availableSpotIndex < waterSampleSpots.Length)
        {
           // StartCoroutine(ResetPositionCoroutine(waterBottle));
        }
    }
    IEnumerator ResetPositionCoroutine(GameObject waterBottle)
    {
        Destroy(waterBottle.GetComponent<Animator>());
        yield return new WaitForEndOfFrame();
        waterBottle.SetActive(true);
        waterBottle.transform.position = waterSampleSpots[availableSpotIndex].position;
        waterBottle.transform.SetParent(waterSampleSpots[availableSpotIndex]);
        waterBottle.transform.GetChild(2).transform.GetChild(0).gameObject.SetActive(true);
        waterBottle.transform.GetChild(2).transform.GetChild(1).gameObject.SetActive(true);
        // waterBottle.transform.Find("Cover").gameObject.SetActive(true);
        availableSpotIndex++;
    }
}
