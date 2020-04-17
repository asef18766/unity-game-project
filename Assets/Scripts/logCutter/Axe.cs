using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace LogCutter
{
	public class Axe : MonoBehaviour
	{		
		// Update is called once per frame
		void Update ()
		{
			Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			transform.position = new Vector3(mousePosition.x , mousePosition.y , transform.position.z);
		}
		void OnTriggerEnter2D(Collider2D c)
        {
            if (!Input.GetMouseButton(0))
                return;
            if (c.tag == "destroyable")
            {
                var obj = c.gameObject.GetComponent<IDestroyable>();
                obj.Cut();
            }
        }
    }

}