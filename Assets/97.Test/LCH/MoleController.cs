using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoleController : MonoBehaviour
{
    private Transform[] holes; // Assign the hole positions in the inspector
    public GameObject molePrefab; // Assign the mole prefab
    public float visibleTime = 2.0f; // Time the mole stays visible
    public float waitTime = 1.0f; // Time interval between appearances

    private GameObject currentMole;

    void Start()
    {
        GameObject[] holeObjects = GameObject.FindGameObjectsWithTag("MoleHole");
        holes = new Transform[holeObjects.Length];
        for (int i = 0; i < holeObjects.Length; i++)
        {
            holes[i] = holeObjects[i].transform;
        }

        StartCoroutine(MoleLogic());
    }

    IEnumerator MoleLogic()
    {
        while (true)
        {
            int holeIndex = Random.Range(0, holes.Length); // Select a random hole

            if (currentMole != null)
                Destroy(currentMole); // Destroy the previous mole if exists

            // Instantiate a new mole at the selected hole
            currentMole = Instantiate(molePrefab, holes[holeIndex].position, Quaternion.identity);

            yield return new WaitForSeconds(visibleTime); // Wait for the mole to be visible

            // Hide the mole
            Destroy(currentMole);

            yield return new WaitForSeconds(waitTime); // Wait before the next mole appears
        }
    }
}