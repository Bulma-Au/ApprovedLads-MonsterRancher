using System;
using TMPro;
using UnityEngine;

namespace Monster_Rancher.DateTimeSystem
{
    public class DateTimeUIFunc : MonoBehaviour
    {
        [SerializeField]
        private TextMeshProUGUI _timeTextComponent;
        [SerializeField]
        private TextMeshProUGUI _dateTextComponent;
        
        private DateAndTimeHandler _dateAndTimeHandler;

        private void Start ( )
        {
            _dateAndTimeHandler = DateAndTimeHandler.Instance;
        }

        private void LateUpdate ( )
        {
            if( _dateAndTimeHandler == null )
                Debug.Log ( "Not set!" );
            
            UpdateTimeTextComponent (  );
            UpdateDateTextComponent (  );
        }

        private void UpdateTimeTextComponent ( )
        {
            var currentMin = _dateAndTimeHandler.QueryCurrentTimeComponent ( TimeHandler.TimeComponent.Minute );
            var currentMinText = currentMin.ToString ( "D2" );
            var currentHour = _dateAndTimeHandler.QueryCurrentTimeComponent ( TimeHandler.TimeComponent.Hour );
            var currentHourText = currentHour.ToString ( "D2" );

            _timeTextComponent.text = $"Time: {currentHourText}/{currentMinText}";
        }

        private void UpdateDateTextComponent ( )
        {
            var currentDay = _dateAndTimeHandler.QueryCurrentDateComponent ( DateHandler.DateComponent.Day );
            var currentDayText = currentDay.ToString ( "D2" );
            var currentMonth = _dateAndTimeHandler.QueryCurrentDateComponent ( DateHandler.DateComponent.Month );
            var currentMonthText = currentMonth.ToString ( "D2" );
            var currentYear = _dateAndTimeHandler.QueryCurrentDateComponent ( DateHandler.DateComponent.Year );
            var currentYearText = currentYear.ToString ( "D4" );

            _dateTextComponent.text = $"Date: {currentDayText}/{currentMonthText}/{currentYearText}";
        }
    }
}