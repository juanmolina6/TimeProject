namespace TimeProject
    {
        public class Time
        {
            private int _hour;
            private int _minute;
            private int _second;
            private int _millisecond;

            public Time()
            {
                _hour = 0;
                _minute = 0;
                _second = 0;
                _millisecond = 0;
            }

            public Time(int hour)
            {
                _hour = ValidHour(hour);
                _minute = 0;
                _second = 0;
                _millisecond = 0;
            }

            public Time(int hour, int minute)
            {   
                _hour = ValidHour(hour);
                _minute = ValidMinute(minute);
                _second = 0;
                _millisecond = 0;
            }

            public Time(int hour, int minute, int second)
            {
                _hour = ValidHour(hour);
                _minute = ValidMinute(minute);
                _second = ValidSecond(second);
                _millisecond = 0;
            }

            public Time(int hour, int minute, int second, int millisecond)
            {
                _hour = ValidHour(hour);
                _minute = ValidMinute(minute);
                _second = ValidSecond(second);
                _millisecond = ValidMillisecond(millisecond);
            }

            public int Hour
            {
                get => _hour;
                set => _hour = ValidHour(value);
            }

            public int Minute
            {
                get => _minute;
                set => _minute = ValidMinute(value);
            }

            public int Second
            {
                get => _second;
                set => _second = ValidSecond(value);
            }

            public int Millisecond
            {
                get => _millisecond;
                set => _millisecond = ValidMillisecond(value);
            }

            private int ValidHour(int hour)
            {
                if (hour < 0 || hour > 23)
                    throw new ArgumentException($"The hour: {hour}, is not valid.");
                return hour;
            }

            private int ValidMinute(int minute)
            {
                if (minute < 0 || minute > 59)
                    throw new ArgumentException($"The minute: {minute}, is not valid.");
                return minute;
            }

            private int ValidSecond(int second)
            {
                if (second < 0 || second > 59)
                    throw new ArgumentException($"The second: {second}, is not valid.");
                return second;
            }

            private int ValidMillisecond(int millisecond)
            {
                if (millisecond < 0 || millisecond > 999)
                    throw new ArgumentException($"The millisecond: {millisecond}, is not valid.");
                return millisecond;
            }

            public override string ToString()
            {
                int displayHour = _hour % 12;

                if (displayHour == 0)
                {
                displayHour = 0; 
                }

                string amPm;
                if (_hour < 12)
                {
                    amPm = "AM";
                }
                else
                {
                    amPm = "PM";
                }

                return $"{displayHour:D2}:{_minute:D2}:{_second:D2}.{_millisecond:D3} {amPm}";
            }

            public long ToMilliseconds()
            {
                return ((_hour * 3600L) + (_minute * 60L) + _second) * 1000L + _millisecond;
            }

            public long ToSeconds()
            {
                return (_hour * 3600L) + (_minute * 60L) + _second;
            }

            public long ToMinutes()
            {
                return (_hour * 60L) + _minute;
            }

            public bool IsOtherDay(Time other)
            {
            int totalHours = _hour + other._hour + (_minute + other._minute) / 60;

            if (totalHours >= 24)
            {
                return true;
            }
                return false;
           
        }
            

        public Time Add(Time other)
        {
            int totalMilliseconds = this._millisecond + other._millisecond;
            int carrySeconds = totalMilliseconds / 1000;
            int newMilliseconds = totalMilliseconds % 1000;

            int totalSeconds = this._second + other._second + carrySeconds;
            int carryMinutes = totalSeconds / 60;
            int newSeconds = totalSeconds % 60;

            int totalMinutes = this._minute + other._minute + carryMinutes;
            int carryHours = totalMinutes / 60;
            int newMinutes = totalMinutes % 60;

            int newHours = (this._hour + other._hour + carryHours) % 24;

            return new Time(newHours, newMinutes, newSeconds, newMilliseconds);
        }
     }
  }