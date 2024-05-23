using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PBuild
{
    public class BuiltOn : MonoBehaviour
    {
        public bool _builtOn = false;
        void Update()
        {
            if(_builtOn == true)
            {
                gameObject.GetComponent<Renderer>().material.color = Color.red;
            }
        }
        
    }
}
