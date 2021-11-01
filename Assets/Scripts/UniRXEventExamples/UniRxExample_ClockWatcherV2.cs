using UniRx;
using UnityEngine;

namespace Monster_Rancher.UniRxEventExamples
{
    public class UniRxExample_ClockWatcherV2 : MonoBehaviour
    {
        [SerializeField]
        private UniRxExample_Clock uniRxExampleClockToWatch;

        private void Start ( )
        {
            uniRxExampleClockToWatch.OnIntervalHit.Subscribe ( unitPassed => ClockHitInterval (  ) ).AddTo ( gameObject );
        }

        private void ClockHitInterval (  )
        {
            Debug.Log ( $"WHADDDWHAWAHDAWHDADHWDAWHDAH! GameObject: {this.gameObject.name}" );
        }
    }
}