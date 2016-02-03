using UnityEngine;
using System.Collections;

namespace NavEditor
{
    public class MeshMoveMono : MonoBehaviour 
    {

	    // Use this for initialization
	    void Start () 
        {
	
	    }
	
	    // Update is called once per frame
        void FixedUpdate ()
        {
            if (Height) AdjustHeight();
            if (Gravity) AdjustToGravity();
	    }

        public bool Height = true;
        public bool Gravity = false;

        void AdjustHeight()
        {
            int origLayer = gameObject.layer;
            gameObject.layer = 2;

            Vector3 currentUp = transform.up;

            float Height = 0;
            RaycastHit hit;
            //Debug.DrawLine(transform.position + transform.up * 5, transform.position + transform.up * -5, Color.yellow);
            if (Physics.Raycast(transform.position + transform.up * 100, transform.up * -1, out hit, Mathf.Infinity, groundLayers.value))
            {
                Height = hit.point.y;

                Vector3 v = transform.position;
                v.y = Height;
                transform.position = v;
            }

            gameObject.layer = origLayer;

        }

        public LayerMask groundLayers;

        private void AdjustToGravity()
        {
            int origLayer = gameObject.layer;
            gameObject.layer = 2;

            Vector3 currentUp = transform.up;

            float damping = Mathf.Clamp01(Time.deltaTime * 5);

            RaycastHit hit;

            Vector3 desiredUp = Vector3.zero;
            for (int i = 0; i < 8; i++)
            {
                Vector3 rayStart =
                    transform.position
                        + transform.up
                        + Quaternion.AngleAxis(360 * i / 8.0f, transform.up) * (transform.right * 0.5f);
                Debug.DrawLine(rayStart, transform.up * -2, Color.yellow);

                if (Physics.Raycast(rayStart, transform.up * -2, out hit, 3.0f, groundLayers.value))
                {
                    desiredUp += hit.normal;
                }
            }
            desiredUp = (currentUp + desiredUp).normalized;
            Vector3 newUp = (currentUp + desiredUp * damping).normalized;

            float angle = Vector3.Angle(currentUp, newUp);
            if (angle > 0.01)
            {
                Vector3 axis = Vector3.Cross(currentUp, newUp).normalized;
                Quaternion rot = Quaternion.AngleAxis(angle, axis);
                transform.rotation = rot * transform.rotation;
            }

            gameObject.layer = origLayer;
        }
    }
}
