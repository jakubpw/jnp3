﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlatformSpawner : MonoBehaviour
{
    [SerializeField]
    private Vector3 m_Velocity;

    [SerializeField]
    private float m_SpawnInterval = 2f;

    [SerializeField]
    private PlatformSet m_PlatformSet = null;

    private Coroutine m_SpawningCoroutine = null;

    private List<GameObject> m_SpawnedPlatforms = new List<GameObject>();

    // Temp
    private void Start()
    {
        StartSpawning();
    }

    private void Update()
    {
        if (m_SpawningCoroutine == null)
        {
            return;
        }

        transform.position += m_Velocity * Time.deltaTime;
    }

    public void StartSpawning()
    {
        StopSpawning();
        m_SpawningCoroutine = StartCoroutine(PlatformSpawningCoroutine());
    }

    public void StopSpawning()
    {
        if (m_SpawningCoroutine != null)
        {
            StopCoroutine(m_SpawningCoroutine);
            m_SpawningCoroutine = null;
        }
    }

    private IEnumerator PlatformSpawningCoroutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(m_SpawnInterval);
            SpawnPlatform();
        }
    }

    private void SpawnPlatform()
    {
        GameObject platformPrefab = m_PlatformSet.GetRandom();
        GameObject platformInstance = Instantiate(platformPrefab, transform.position, Quaternion.identity);

        m_SpawnedPlatforms.Add(platformInstance);
    }

    public void ResetToPosition(Vector3 position, bool destroyAllPlatforms = true)
    {
        transform.position = position;

        if (destroyAllPlatforms)
        {
            for (int i = 0; i < m_SpawnedPlatforms.Count; i++)
            {
                Destroy(m_SpawnedPlatforms[i]);
            }

            m_SpawnedPlatforms.Clear();
        }
    }
}
