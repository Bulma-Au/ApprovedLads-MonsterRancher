using System;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using UniRx;
using UnityEngine;
using Utility_Classes;

namespace Monster_Rancher.DateTimeSystem
{
    public class TimeHandler : MonoBehaviour
    {
        public static TimeHandler Instance;
        
        private float _currentSecond;
        private int _currentMinute;
        private int _currentHour;

        private float _timescaleMultiplier = 75;
        private int hourToStartDay = 10;
        private int hoursInDayCycle = 18;

        public IObservable < Unit > OnMinuteElapsed => _onMinuteElapsed;
        private Subject < Unit > _onMinuteElapsed = new Subject < Unit > ( );

        public IObservable < Unit > OnHourElapsed => _onHourElapsed;
        private Subject < Unit > _onHourElapsed = new Subject < Unit > ( );

        private bool _isIncrementing = false;

        private void Awake ( )
        {
            Instance = this;
            
            SetClock ( 10,0,0 );
            BeginClock (  );
        }

        private void BeginClock ( )
        {
            _isIncrementing = true;
            IncrementClock (  ).WrapErrors (  );
        }

        private void PauseClock ( )
        {
            _isIncrementing = false;
        }

        private void SetClock ( int targetHour, int targetMinute, int targetSecond )
        {
            _currentSecond = targetSecond;
            _currentMinute = targetMinute;
            _currentHour = targetHour;
        }

        private async Task IncrementClock ( )
        {
            while( _isIncrementing )
            {
                var secondsElapsed =  Time.deltaTime * _timescaleMultiplier;
                _currentSecond += secondsElapsed;
                
                if( TimeComponentOverflowCheck ( TimeComponent.Minute, _currentSecond ) )
                {
                    _currentSecond = TimeComponentOverflowValue ( TimeComponent.Second, _currentSecond );
                    _currentMinute++;
                    _onMinuteElapsed.OnNext ( Unit.Default );
                }

                if( TimeComponentOverflowCheck ( TimeComponent.Hour, _currentMinute ) )
                {
                    _currentMinute = TimeComponentOverflowValue ( TimeComponent.Minute, _currentMinute );
                    _currentHour++;
                    _onHourElapsed.OnNext ( Unit.Default );
                }
                
                await UniTask.WaitForEndOfFrame( );
            }
        }

        private bool TimeComponentOverflowCheck (TimeComponent componentToCheck, int timeframeElapsed )
        {
            return componentToCheck switch
            {
                TimeComponent.Minute => timeframeElapsed > 60,
                TimeComponent.Hour => timeframeElapsed > 60,
                _ => false
            };
        }

        private bool TimeComponentOverflowCheck ( TimeComponent componentToCheck, float timeframeElapsed )
        {
            return componentToCheck switch
            {
                TimeComponent.Minute => timeframeElapsed > 60,
                TimeComponent.Hour => timeframeElapsed > 60,
                _ => false
            };
        }

        private int TimeComponentOverflowValue (TimeComponent componentToCheck, int timeframeElapsed )
        {
            return componentToCheck switch
            {
                TimeComponent.Second => timeframeElapsed / 60,
                TimeComponent.Minute => timeframeElapsed / 60,
                TimeComponent.Hour => timeframeElapsed / 60,
                _ => 0
            };
        }

        private float TimeComponentOverflowValue ( TimeComponent componentToCheck, float timeframeElapsed )
        {
            return componentToCheck switch
            {
                TimeComponent.Second => timeframeElapsed / 60,
                TimeComponent.Minute => timeframeElapsed / 60,
                TimeComponent.Hour => timeframeElapsed / 60,
                _ => 0f
            };
        }

        public enum TimeComponent { Second, Minute, Hour };
    }
}