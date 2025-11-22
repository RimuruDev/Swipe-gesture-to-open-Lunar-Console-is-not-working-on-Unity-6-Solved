using UnityEngine;
using LunarConsolePlugin;

namespace AbyssMoth.Lunar
{
    [DisallowMultipleComponent]
    public sealed class LunarSwipeOpener : MonoBehaviour
    {
        [Min(10f)] 
        public float MinSwipeDistance = 80f;
      
        [Range(0.05f, 0.5f)] 
        public float StartZoneHeight = 0.25f;

        public bool IsImmortalGameObject = true;
        
        private bool tracking;
        private Vector2 startCenter;

        private void Awake()
        {
            if (!IsImmortalGameObject)
                return;
            
            transform.SetParent(null);
            DontDestroyOnLoad(gameObject);
        }

        public void Update()
        {
            if (!LunarConsole.isConsoleEnabled)
            {
                tracking = false;
                return;
            }

#if UNITY_ANDROID || UNITY_IOS
            if (Input.touchCount < 2)
            {
                tracking = false;
                return;
            }

            var touchA = Input.touches[0];
            var touchB = Input.touches[1];

            if (!tracking)
            {
                var yTop = Screen.height * (1f - StartZoneHeight);

                var beganA = touchA.phase == TouchPhase.Began;
                var beganB = touchB.phase == TouchPhase.Began;

                if ((beganA || beganB) &&
                    touchA.position.y >= yTop &&
                    touchB.position.y >= yTop)
                {
                    tracking = true;
                    startCenter = (touchA.position + touchB.position) * 0.5f;
                }
            }

            if (!tracking)
                return;

            if (touchA.phase == TouchPhase.Canceled || touchB.phase == TouchPhase.Canceled)
            {
                tracking = false;
                return;
            }

            var currentCenter = (touchA.position + touchB.position) * 0.5f;
            var deltaY = currentCenter.y - startCenter.y;

            if (deltaY <= -MinSwipeDistance)
            {
                tracking = false;
                LunarConsole.Show();
            }

            var endedA = touchA.phase == TouchPhase.Ended;
            var endedB = touchB.phase == TouchPhase.Ended;

            if (endedA || endedB)
                tracking = false;
#endif
        }
    }
}
