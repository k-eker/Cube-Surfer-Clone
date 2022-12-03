using UnityEngine;
using System.Collections;

namespace Assets.Scripts
{
    public enum CubeColor
    {
        Green,
        Blue,
        Purple,
        Red,
        Yellow
    }

    public class Cube : MonoBehaviour
    {
        public CubeColor color;
        
        private void OnTriggerEnter(Collider other)
        {
            if (other.tag == "Player")
            {
                other.GetComponentInChildren<CubeBuilder>().AddCube(this);
            }
        }
    }
   
}