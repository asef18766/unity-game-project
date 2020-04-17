using UnityEngine;
namespace LogCutter
{
    class Border : MonoBehaviour
    {
        void OnTriggerEnter2D(Collider2D other)
        {
            if(other.tag == "destroyable")
            {
                Destroy(other.gameObject);
            }
        }
    }
    
}