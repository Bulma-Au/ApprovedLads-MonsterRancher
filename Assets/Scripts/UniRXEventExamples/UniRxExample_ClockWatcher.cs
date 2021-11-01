using System;
using UniRx;
using UnityEngine;

namespace Monster_Rancher.UniRxEventExamples
{
    public class UniRxExample_ClockWatcher : MonoBehaviour
    {
        [SerializeField]
        private UniRxExample_Clock uniRxExampleClockToWatch;

        private void Start ( )
        {
            uniRxExampleClockToWatch.OnIntervalHit.Subscribe ( unitPassed => ClockHitInterval (  ) ).AddTo ( gameObject );
        }

        private void ClockHitInterval (  )
        {
            Debug.Log ( $"Clock Interval was hit, I'm responding to the event! GameObject: {this.gameObject.name}." );
        }
    }
}