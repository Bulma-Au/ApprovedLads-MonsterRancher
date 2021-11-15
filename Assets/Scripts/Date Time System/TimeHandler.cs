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

        public float CurrentSecond => _currentSecond;
        private float _currentSecond;
        public int CurrentMinute => _currentMinute;
        private int _currentMinute;
        public int CurrentHour => _currentHour;
        private int _currentHour;

        private int _currentElapsedHours = 0;

        private float _timescaleMultiplier = 75;
        private int _hourToStartDay = 10;
        private int _hoursInDayCycle = 18;

        public IObservable < int > OnMinuteElapsed => _onMinuteElapsed;
        private Subject < int > _onMinuteElapsed = new Subject < int > ( );

        public IObservable < int > OnHourElapsed => _onHourElapsed;
        private Subject < int > _onHourElapsed = new Subject < int > ( );

        public IObservable < Unit > OnDayElapsed => _onDayElapsed;
        private Subject < Unit > _onDayElapsed = new Subject < Unit > ( );

        private bool _isIncrementing = false;

        private void Awake ( )
        {
            Instance = this;
            
            SetTime ( 0,0, _hourToStartDay );
            StartTime (  );
        }

        public void StartNewDay ( )
        {
            _isIncrementing = true;
            IncrementClock (  ).WrapErrors (  );
        }

        private void StartTime ( )
        {
            _isIncrementing = true;
            IncrementClock (  ).WrapErrors (  );
        }

        private void PauseTime ( )
        {
            _isIncrementing = false;
        }

        private void SetTime ( int targetSecond, int targetMinute, int targetHour)
        {
            _currentSecond = targetSecond;
            _currentMinute = targetMinute;
            _currentHour = targetHour;
        }

        private void ResetTime ( )
        {
            SetTime ( 0, 0, _hourToStartDay );
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
                    _onMinuteElapsed.OnNext ( _currentMinute );
                }

                if( TimeComponentOverflowCheck ( TimeComponent.Hour, _currentMinute ) )
                {
                    _currentMinute = TimeComponentOverflowValue ( TimeComponent.Minute, _currentMinute );
                    _currentHour++;
                    _currentElapsedHours++;
                    _onHourElapsed.OnNext ( _currentHour );
                    if( _currentElapsedHours >= _hoursInDayCycle )
                        await EndOfDayCleanup ( );
                }
                
                await UniTask.WaitForEndOfFrame( );
            }
        }

        private Task EndOfDayCleanup ( )
        {
            PauseTime (  );
            ResetTime (  );
            _onDayElapsed.OnNext ( Unit.Default );
            return Task.CompletedTask;
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