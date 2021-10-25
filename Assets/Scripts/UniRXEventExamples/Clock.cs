using System;
using UniRx;
using UnityEngine;

namespace Monster_Rancher.UniRxEventExamples
{
    public class Clock : MonoBehaviour
    {
        [SerializeField]
        private float _timer = 0f;
        [SerializeField]
        private float _interval;

        private Subject < Unit > onIntervalHit = new Subject < Unit > ( );
        public IObservable < Unit > OnIntervalHit => onIntervalHit;

        private void Update ( )
        {
            _timer += Time.deltaTime;
            if ( _timer <= _interval )
                return;
            
            Debug.Log ( "Clock Interval; was hit, I'm responding in Clock." );
            onIntervalHit.OnNext ( Unit.Default );
            _timer = 0f;
        }
    }
}