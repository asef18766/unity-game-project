using UnityEngine;
namespace LogCutter
{
    public class Bogay : IDestroyable
    {
        [SerializeField] Sprite destroyed;
		enum LogState
		{
			Normal,
			Destroyed
		}
		LogState logState = LogState.Normal;
		GameController gameController;
		// Use this for initialization
		void Start()
		{
			gameController = GameController.instance;
		}
		// Update is called once per frame
		void Update()
		{
			if(logState == LogState.Destroyed)
			{
				GetComponent<SpriteRenderer>().sprite = destroyed;
			}
		}
		public override void Cut()
		{
            if(logState == LogState.Destroyed)
                return;
			logState = LogState.Destroyed;
            if(gameController == null)
                print("game controller is null");
			gameController.StartCoroutine("minusScore");
		}
    }
}