using System.Collections;
using UnityEngine;

public class MoleController : MonoBehaviour
{
    private Transform[] holes; // Array to store the positions of the holes
    public GameObject molePrefab; // Prefab for the mole GameObject
    public float visibleTime = 2.0f; // Time for which the mole remains visible
    public float waitTime = 1.0f; // Waiting time between mole appearances
    public float animationTime = 0.5f; // Duration of the up/down animation
    public float targetHeight = 1f; // Height the mole pops up above the hole

    private GameObject currentMole; // Reference to the currently active mole
    private Coroutine currentMoveCoroutine; // Reference to the current moving coroutine

    void Start()
    {
        // Find and store all holes tagged as "MoleHole"
        GameObject[] holeObjects = GameObject.FindGameObjectsWithTag("MoleHole");
        holes = new Transform[holeObjects.Length];
        for (int i = 0; i < holeObjects.Length; i++)
        {
            holes[i] = holeObjects[i].transform;
        }

        // Start the main mole logic coroutine
        StartCoroutine(MoleLogic());
    }

    IEnumerator MoleLogic()
    {
        while (true)
        {
            // Choose a random hole for the mole to appear
            int holeIndex = Random.Range(0, holes.Length);

            // Hide and destroy the current mole if it exists
            if (currentMole != null)
            {
                if (currentMoveCoroutine != null)
                {
                    StopCoroutine(currentMoveCoroutine); // Stop the current coroutine
                }

                // Start moving the mole down and wait for the animation to finish
                Vector3 hidePosition = holes[holeIndex].position + new Vector3(0, -targetHeight, 0);
                currentMoveCoroutine = StartCoroutine(MoveMole(currentMole.transform, hidePosition, animationTime));
                yield return new WaitForSeconds(animationTime);

                Destroy(currentMole); // Destroy the mole after hiding
            }

            // Instantiate a new mole and start moving it up
            Vector3 startPosition = holes[holeIndex].position + new Vector3(0, -targetHeight, 0);
            Vector3 showPosition = holes[holeIndex].position + new Vector3(0, targetHeight, 0);
            currentMole = Instantiate(molePrefab, startPosition, Quaternion.identity);
            currentMoveCoroutine = StartCoroutine(MoveMole(currentMole.transform, showPosition, animationTime));

            // Wait for the mole to be visible for the specified time
            yield return new WaitForSeconds(visibleTime + animationTime);

            // Start moving the mole down again
            currentMoveCoroutine = StartCoroutine(MoveMole(currentMole.transform, startPosition, animationTime));
            yield return new WaitForSeconds(animationTime + waitTime); // Wait for the animation to complete and additional waiting time
        }
    }

    IEnumerator MoveMole(Transform moleTransform, Vector3 targetPos, float duration)
    {
        // Exit coroutine if the mole GameObject is destroyed
        if (moleTransform == null) yield break;

        float time = 0;
        Vector3 startPosition = moleTransform.position;

        // Gradually move the mole to the target position
        while (time < duration)
        {
            if (moleTransform == null) yield break; // Check again in case the mole is destroyed mid-animation

            moleTransform.position = Vector3.Lerp(startPosition, targetPos, time / duration);
            time += Time.deltaTime;
            yield return null;
        }

        // Ensure the mole ends exactly at the target position
        if (moleTransform != null)
        {
            moleTransform.position = targetPos;
        }
    }
}