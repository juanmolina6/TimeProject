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
            //hola mundo
            var isPM = _hour >= 12;
            var period = isPM ? "PM" : "AM";

          
            int hourIn12Format;
            if (_hour == 0) 
                hourIn12Format = 00;
            else if (_hour > 12) 
                hourIn12Format = _hour - 12;
            else 
                hourIn12Format = _hour;

            return string.Format("{0:00}:{1:00}:{2:00}.{3:000} {4}",
                                hourIn12Format, _minute, _second, _millisecond, period);
        }

        public long ToMilliseconds()
        {
            try { return ((_hour * 3600 + _minute * 60 + _second) * 1000L) + _millisecond; }
            catch { return 0; }
        }

        public long ToSeconds()
        {
            try { return _hour * 3600 + _minute * 60 + _second; }
            catch { return 0; }
        }

        public long ToMinutes()
        {
            try { return _hour * 60 + _minute; }
            catch { return 0; }
        }

        public bool IsOtherDay(Time other)
        {
            return (this.ToSeconds() + other.ToSeconds()) >= 86400;
        }

        public Time Add(Time other)
        {
            int newMillisecond = _millisecond + other._millisecond;
            int carrySecond = newMillisecond / 1000;
            newMillisecond %= 1000;

            int newSecond = _second + other._second + carrySecond;
            int carryMinute = newSecond / 60;
            newSecond %= 60;

            int newMinute = _minute + other._minute + carryMinute;
            int carryHour = newMinute / 60;
            newMinute %= 60;

            int newHour = (_hour + other._hour + carryHour) % 24;

            return new Time(newHour, newMinute, newSecond, newMillisecond);
        }
    }
}
