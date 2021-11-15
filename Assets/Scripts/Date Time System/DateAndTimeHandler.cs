using System;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using UniRx;
using UnityEngine;
using Utility_Classes;

namespace Monster_Rancher.DateTimeSystem
{
    public class DateAndTimeHandler : MonoBehaviour
    {
        public static DateAndTimeHandler Instance;
        
        private TimeHandler _timeHandler;
        private DateHandler _dateHandler;
        
        private void Awake ( )
        {
            Instance = this;
            _timeHandler = TimeHandler.Instance;
            _dateHandler = DateHandler.Instance;
            SetupEventReactions (  );
        }

        private void SetupEventReactions ( )
        {
            _timeHandler.OnDayElapsed.Subscribe ( eventParams =>
            {
                EndDay(  ).WrapErrors (  );
            } );
        }

        public int QueryCurrentTimeComponent (TimeHandler.TimeComponent componentToQuery )
        {
            return componentToQuery switch
            {
                TimeHandler.TimeComponent.Second => 0,
                TimeHandler.TimeComponent.Minute => _timeHandler.CurrentMinute,
                TimeHandler.TimeComponent.Hour => _timeHandler.CurrentHour,
                _ => throw new Exception("This switch statement should never reach the default state!")
            };
        }
        
        public int QueryCurrentDateComponent ( DateHandler.DateComponent componentToQuery)
        {
            return componentToQuery switch
            {
                DateHandler.DateComponent.Day => _dateHandler.CurrentDate.Day,
                DateHandler.DateComponent.Month => _dateHandler.CurrentDate.Month,
                DateHandler.DateComponent.Year => _dateHandler.CurrentDate.Year,
                _ => throw new Exception ( "This switch statement should never reach the default state!" )
            };
        }

        public void BeginDay ( )
        {
            //Date related method calls
            _dateHandler.NewDaySetup (  );
            //Time related method calls.
            _timeHandler.StartNewDay (  );
        }

        private async Task EndDay (  )
        {
            Debug.Log ( "Simulating end of day 'loading' sequence.." );
            await OffloadEndOfDaySetup ( );
            Debug.Log ( "End of day 'loading' sequence completed." );
        }

        private async Task OffloadEndOfDaySetup ( )
        {
            await Task.Delay ( 2000 );
        }
    }
}