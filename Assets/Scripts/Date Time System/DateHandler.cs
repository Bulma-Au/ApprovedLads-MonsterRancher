using System;
using UniRx;
using UnityEngine;

namespace Monster_Rancher.DateTimeSystem
{
    public class DateHandler : MonoBehaviour
    {
        public static DateHandler Instance;
        
        private int _currentDay;
        private int _currentMonth;
        private int _currentYear;
        public DateTime CurrentDate => _currentDate;
        private DateTime _currentDate;

        public Subject < int > OnDayElapsed => _onDayElapsed;
        private Subject < int > _onDayElapsed = new Subject < int > (  );
        public Subject < int > OnMonthElapsed => _onMonthElapsed;
        private Subject < int > _onMonthElapsed = new Subject < int > (  );
        public Subject < int > OnYearElapsed => _onYearElapsed;
        private Subject < int > _onYearElapsed = new Subject < int > ( );


        private void Awake ( )
        {
            Instance = this;
            SetDate ( 1, 1, 2021 );
        }

        public void SetDate ( int targetDay, int targetMonth, int targetYear)
        {
            _currentDate = new DateTime ( targetYear, targetMonth, targetDay );
        }

        public void NewDaySetup ( )
        {
            IncrementDay (  );
        }

        private void IncrementDay ( )
        {
            if( !DateComponentOverflowCheck ( DateComponent.Month ) )
            {
                _currentDay++;
                SetDate ( _currentDay, _currentMonth, _currentYear );
                return;
            }

            _currentDay = 1;
            IncrementMonth ( );
        }

        private void IncrementMonth ( )
        {
            if( !DateComponentOverflowCheck ( DateComponent.Year ) )
            {
                _currentMonth++;
                SetDate ( _currentDay, _currentMonth, _currentYear );
                return;
            }
            
            _currentMonth = 1;
            IncrementYear (  );
        }

        private void IncrementYear ( )
        {
            _currentYear++;
            SetDate ( _currentDay, _currentMonth, _currentYear );
        }

        private bool DateComponentOverflowCheck ( DateComponent targetDateComponent )
        {
            switch( targetDateComponent )
            {
                //Valid
                case DateComponent.Month:
                    var daysInCurrentMonth = DateTime.DaysInMonth ( _currentYear, _currentMonth );
                    return _currentDay == daysInCurrentMonth;
                case DateComponent.Year:
                    return _currentMonth == 12;
                //Invalid
                case DateComponent.Day:
                    throw new Exception ( "This DateComponent should not be used in this function! You're doing something wrong IDIOT!" );
                default:
                    throw new Exception ( "This case should never be reached! You're doing something wrong IDIOT!" );
            }
        }

        public enum DateComponent { Day, Month, Year };
    }
}