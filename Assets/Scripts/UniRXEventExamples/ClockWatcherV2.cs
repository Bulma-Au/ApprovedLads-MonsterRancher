using UniRx;
using UnityEngine;

namespace Monster_Rancher.UniRxEventExamples
{
    public class ClockWatcherV2 : MonoBehaviour
    {
        [SerializeField]
        private Clock _clockToWatch;

        private void Start ( )
        {
            _clockToWatch.OnIntervalHit.Subscribe ( unitPassed => ClockHitInterval (  ) ).AddTo ( gameObject );
        }

        private void ClockHitInterval (  )
        {
            Debug.Log ( $"WHADDDWHAWAHDAWHDADHWDAWHDAH! GameObject: {this.gameObject.name}" );
        }
    }
}