using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeSpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] m_CubePrefabs;
    [SerializeField] private Transform m_CubeRoot;
    [SerializeField] private Transform m_EnvironmentPreset1;
    [SerializeField] private Transform m_EnvironmentPreset2;
    [Range(0.1f, 5f)] [SerializeField] private float m_SpawnDelay;
    [Range(0.1f, 15f)] [SerializeField] private float m_MapMovementSpeed;

    private const float MIN_POS_X = -0.17f;
    private const float MAX_POS_X = 0.14f;

    private const float ENVIRONMENT_RESET_POS_Z = -17f;
    private const float Z_DIST_OF_ENVIRONMENTS = 27f;

    private void Start()
    {
        StartCoroutine(Spawning());
    }


    private void Update()
    {
        m_CubeRoot.Translate(Vector3.back * m_MapMovementSpeed * Time.deltaTime);
        m_EnvironmentPreset1.Translate(Vector3.back * m_MapMovementSpeed * Time.deltaTime);
        m_EnvironmentPreset2.Translate(Vector3.back * m_MapMovementSpeed * Time.deltaTime);

        if(m_EnvironmentPreset2.transform.position.z < ENVIRONMENT_RESET_POS_Z)
        {
            m_EnvironmentPreset2.transform.position = new Vector3(
                m_EnvironmentPreset2.transform.position.x, 
                m_EnvironmentPreset2.transform.position.y, 
                m_EnvironmentPreset2.transform.position.z + Z_DIST_OF_ENVIRONMENTS
                );
        }
        else if (m_EnvironmentPreset1.transform.position.z < ENVIRONMENT_RESET_POS_Z)
        {
            m_EnvironmentPreset1.transform.position = new Vector3(
                m_EnvironmentPreset1.transform.position.x,
                m_EnvironmentPreset1.transform.position.y,
                m_EnvironmentPreset1.transform.position.z + Z_DIST_OF_ENVIRONMENTS
                );
        }
    }


    IEnumerator Spawning()
    {
        while (true)
        {
            yield return new WaitForSeconds(m_SpawnDelay);
            Spawn();
        }
    }

    private void Spawn()
    {
        GameObject go = Instantiate(
            m_CubePrefabs[Random.Range(0, m_CubePrefabs.Length)], 
            new Vector3(Random.Range(MIN_POS_X, MAX_POS_X), transform.position.y, transform.position.z), 
            Quaternion.identity);
        go.transform.SetParent(m_CubeRoot);
    }
}
