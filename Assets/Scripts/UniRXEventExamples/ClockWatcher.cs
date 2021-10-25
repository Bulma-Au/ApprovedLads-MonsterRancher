using System;
using UniRx;
using UnityEngine;

namespace Monster_Rancher.UniRxEventExamples
{
    public class ClockWatcher : MonoBehaviour
    {
        [SerializeField]
        private Clock _clockToWatch;

        private void Start ( )
        {
            _clockToWatch.OnIntervalHit.Subscribe ( unitPassed => ClockHitInterval (  ) ).AddTo ( gameObject );
        }

        private void ClockHitInterval (  )
        {
            Debug.Log ( $"Clock Interval was hit, I'm responding to the event! GameObject: {this.gameObject.name}." );
        }
    }
}