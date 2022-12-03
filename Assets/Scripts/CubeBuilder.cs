using UnityEngine;
using System.Collections.Generic;
using System.Linq;

namespace Assets.Scripts
{
    public class CubeBuilder : MonoBehaviour
    {
        [SerializeField] private List<Cube> m_AllCubes;
        [SerializeField] private Transform m_PlayerModelTransform;
        [SerializeField] private Transform m_CubeStartTransform;

        private const float CUBE_OFFSET_Y = 0.044f;

        private Vector3 m_PlayerStartPos;

        private List<Cube> m_CubesToDestroy;

        private void Start()
        {
            m_PlayerStartPos = m_PlayerModelTransform.position;
        }

        public void AddCube(Cube cube)
        {
            cube.transform.SetParent(this.transform);
            cube.transform.position = new Vector3(m_PlayerModelTransform.position.x, m_CubeStartTransform.position.y, m_CubeStartTransform.position.z);

            for (int i = 0; i < m_AllCubes.Count; i++)
            {
                m_AllCubes[i].transform.position = new Vector3(m_PlayerModelTransform.position.x, m_AllCubes[i].transform.position.y + CUBE_OFFSET_Y, m_AllCubes[i].transform.position.z);
            }
            m_PlayerModelTransform.position = new Vector3(m_PlayerModelTransform.position.x, m_PlayerModelTransform.position.y + CUBE_OFFSET_Y, m_PlayerModelTransform.position.z);

            m_AllCubes.Add(cube);

            if (CheckIfCubesMatch())
            {
                Debug.Log("All cubes matched!");
                DestroyOneOfEachCube();
                ReorderCubes();
            }
        }

        private bool CheckIfCubesMatch()
        {
            List<CubeColor> oneOfEachColor = new List<CubeColor>();
            m_CubesToDestroy = new List<Cube>();
            for (int i = 0; i < m_AllCubes.Count; i++)
            {
                if (!oneOfEachColor.Contains(m_AllCubes[i].color))
                {
                    oneOfEachColor.Add(m_AllCubes[i].color);
                    m_CubesToDestroy.Add(m_AllCubes[i]);
                }
            }

            return oneOfEachColor.Count > 4;
        }

        private void DestroyOneOfEachCube()
        {
            m_AllCubes.RemoveAll(t => m_CubesToDestroy.Contains(t));

            for (int i = 0; i < m_CubesToDestroy.Count; i++)
            {
                m_CubesToDestroy[i].gameObject.SetActive(false);
                Debug.Log("Destroying " + m_CubesToDestroy[i]);
            }
        }

        private void ReorderCubes()
        {
            for (int i = 0; i < m_AllCubes.Count; i++)
            {
                m_AllCubes[i].transform.position = new Vector3(m_PlayerModelTransform.position.x, m_CubeStartTransform.position.y + CUBE_OFFSET_Y * i, m_AllCubes[i].transform.position.z);
            }

            m_PlayerModelTransform.position = new Vector3(m_PlayerModelTransform.position.x, m_PlayerStartPos.y + CUBE_OFFSET_Y * m_AllCubes.Count, m_PlayerModelTransform.position.z);
        }
    }
}