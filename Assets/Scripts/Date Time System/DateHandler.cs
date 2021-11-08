using System;
using UnityEngine;

namespace Monster_Rancher.DateTimeSystem
{
    public class DateHandler : MonoBehaviour
    {
        public static DateHandler Instance;
        
        private int _currentDay;
        private int _currentMonth;
        private int _currentYear;
        private DateTime _currentDateTime;

        private void Awake ( )
        {
            Instance = this;
        }

        public void SetDate ( int targetDay, int targetMonth, int targetYear)
        {
            _currentDateTime = new DateTime ( targetYear, targetMonth, targetDay );
        }

        public void IncrementDay ( )
        {
            if( !DateComponentOverflowCheck ( DateComponent.Month ) )
            {
                _currentDay++;
                return;
            }

            IncrementMonth ( );
            _currentDay = 1;
        }

        public void IncrementMonth ( )
        {
            if( !DateComponentOverflowCheck ( DateComponent.Year ) )
            {
                _currentMonth++;
                return;
            }
            
            IncrementYear (  );
            _currentMonth = 1;
        }

        public void IncrementYear ( )
        {
            _currentYear++;
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