using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace LogCutter
{
    public class LogGenerator : MonoBehaviour
    {
        [SerializeField] GameObject[] logs;
        [SerializeField] float[] percentages;
        [SerializeField] float minRespawnTime, maxRespawnTime;
        [SerializeField] int maxSpawnCount, minSpawnCount;
        [SerializeField] float throwForce;
        #region collider size info
        Vector3 boxCenter;
        Vector2 extend; // half size of box extends
        #endregion
        bool blocked = false;
        static Quaternion randQuaternion(float min, float max)
        {
            float deg = Random.Range(min, max);
            return Quaternion.Euler(0, 0, deg);
        }
        static Vector2 randAngle(float min, float max)
        {
            float deg = Random.Range(min, max) * 2 * Mathf.PI;
            deg /= 360.0f;
            return new Vector2(Mathf.Cos(deg), Mathf.Sin(deg));
        }
        static int randChoice(float[] percentages)
        {
            var choice = Random.Range(0,101) / 100.0f;
            for(int u=0;u!=percentages.Length;u++)
            {
                choice -= percentages[u];
                if(choice <= 0)
                {
                    return u;
                }
            }
            return 0;
        }
        void spawnSingle()
        {
            var xDelta = Random.Range(-extend.x, extend.x);
            Vector3 pos = new Vector3(boxCenter.x, boxCenter.y, boxCenter.z);
            pos.x = pos.x + xDelta;

            pos = transform.position; // for debug :P

            var logChild = Instantiate(logs[randChoice(this.percentages)], pos, randQuaternion(0, 360));
            var force = randAngle(45, 135) * throwForce;
            logChild.GetComponent<Rigidbody2D>().AddForce(force);
        }
        IEnumerator spawnLogs(float delay, int num)
        {
            yield return new WaitForSeconds(delay);
            for(int u = 0 ; u != num ; ++u)
                spawnSingle();
            blocked = false;
        }
        // Use this for initialization
        void Start()
        {
            var box = GetComponent<BoxCollider2D>().bounds;
            boxCenter = box.center;
            extend = box.extents;
        }

        // Update is called once per frame
        void Update()
        {
            if (!blocked)
            {
                blocked = true;
                float delay = Random.Range(minRespawnTime, maxRespawnTime);
                int count = Random.Range(minSpawnCount, maxSpawnCount);
                StartCoroutine(spawnLogs(delay, count));
            }

        }
    }
}