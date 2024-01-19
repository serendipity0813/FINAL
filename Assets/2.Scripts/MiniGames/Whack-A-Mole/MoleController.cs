using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoleController : MonoBehaviour
{
    public static MoleController Instance; // Singleton instance for easy access

    public GameObject molePrefab; // Prefab for the live mole
    public GameObject moleDeadPrefab; // Prefab for the dead mole
    public float visibleTime = 2.0f; // Time each live mole stays visible
    public float deadMoleVisibleTime = 1.0f; // Time each dead mole stays visible
    public float minSpawnDelay = 1.0f; // Min delay before spawning another mole
    public float maxSpawnDelay = 3.0f; // Max delay
    public float animationTime = 0.5f; // Duration of mole's animation
    public float targetHeight = 1f; // Target height for moles to pop up

    private Transform[] holes; // Array to store the positions of the holes
    private Dictionary<GameObject, Coroutine> moleCoroutines = new Dictionary<GameObject, Coroutine>(); // Track coroutines for each mole

    void Awake()
    {
        // Singleton pattern setup
        if (Instance == null)
            Instance = this;
        else if (Instance != this)
            Destroy(gameObject);
    }

    void Start()
    {
        // Initialize hole positions
        GameObject[] holeObjects = GameObject.FindGameObjectsWithTag("MoleHole");
        holes = new Transform[holeObjects.Length];
        for (int i = 0; i < holeObjects.Length; i++)
        {
            holes[i] = holeObjects[i].transform;
        }

        // Start spawning moles
        StartCoroutine(MoleSpawner());
    }

    IEnumerator MoleSpawner()
    {
        // Continuously spawn moles at random intervals
        while (true)
        {
            float delay = Random.Range(minSpawnDelay, maxSpawnDelay);
            yield return new WaitForSeconds(delay);
            SpawnMole();
        }
    }

    void SpawnMole()
    {
        // Spawn and manage a mole's lifecycle
        int holeIndex = Random.Range(0, holes.Length);
        Vector3 startPosition = holes[holeIndex].position + new Vector3(0, -targetHeight, 0);
        Vector3 showPosition = holes[holeIndex].position + new Vector3(0, targetHeight, 0);

        GameObject mole = Instantiate(molePrefab, startPosition, Quaternion.identity);
        Coroutine moleCoroutine = StartCoroutine(HandleMoleLifecycle(mole, showPosition, startPosition));
        moleCoroutines.Add(mole, moleCoroutine);
    }

    IEnumerator HandleMoleLifecycle(GameObject mole, Vector3 showPosition, Vector3 startPosition)
    {
        // Move mole up and handle visibility
        yield return MoveMole(mole.transform, showPosition, animationTime);
        yield return new WaitForSeconds(visibleTime);

        // Check if mole has been hit before moving it down
        if (moleCoroutines.ContainsKey(mole))
        {
            yield return MoveMole(mole.transform, startPosition, animationTime);
            moleCoroutines.Remove(mole);
            Destroy(mole);
        }
    }

    public void MoleHit(GameObject mole)
    {
        // Replace a live mole with a dead mole when hit
        if (moleCoroutines.ContainsKey(mole))
        {
            ScoreManager.Instance.AddScore(mole.GetComponent<MoleHit>().points); // Update the score
            StopCoroutine(moleCoroutines[mole]);
            moleCoroutines.Remove(mole);
            ReplaceWithDeadMole(mole);
        }
    }

    private void ReplaceWithDeadMole(GameObject mole)
    {
        Vector3 position = mole.transform.position;
        Destroy(mole);

        GameObject deadMole = Instantiate(moleDeadPrefab, position, Quaternion.identity);
        // Start coroutine to animate and then remove the dead mole
        StartCoroutine(RemoveDeadMoleAfterDelay(deadMole, deadMoleVisibleTime));
    }

    IEnumerator RemoveDeadMoleAfterDelay(GameObject deadMole, float delay)
    {
        // Wait before starting to hide the dead mole
        yield return new WaitForSeconds(delay);

        // Animate the dead mole moving down
        Vector3 hidePosition = deadMole.transform.position + new Vector3(0, -targetHeight, 0);
        yield return MoveMole(deadMole.transform, hidePosition, animationTime);

        // Remove the dead mole after the animation
        Destroy(deadMole);
    }

    IEnumerator MoveMole(Transform moleTransform, Vector3 targetPos, float duration)
    {
        // Animate mole movement
        float time = 0;
        Vector3 startPosition = moleTransform.position;

        while (time < duration)
        {
            if (moleTransform == null) yield break;
            moleTransform.position = Vector3.Lerp(startPosition, targetPos, time / duration);
            time += Time.deltaTime;
            yield return null;
        }
    }
}
