using UnityEngine;
using System.Collections;

namespace NavEditor
{
    public class MeshNavMono : MonoBehaviour 
    {
        public GameObject target;
        public Light mLight;

        public void SetTarget(int layer)
        {
            foreach (Transform e in gameObject.GetComponentInChildren<Transform>())
            {
                MeshCollider bc = e.gameObject.GetComponent<MeshCollider>();
				if(bc == null)
				{
                    bc = e.gameObject.AddComponent<MeshCollider>();
				}
				bc.gameObject.layer = layer;
            }
        }

        public GameObject mGroud;
        public GameObject mNav;
        public bool bNav;

    }
}
