namespace World
{
    public struct Time
    {
        private float m_time;
        public Time(float time)
        {
            m_time = time;
        }
        
        public static bool operator <(Time left, Time right)
        {
            return left.m_time < right.m_time;
        }

        public static bool operator >(Time left, Time right)
        {
            return left.m_time > right.m_time;
        }

        public static bool operator <=(Time left, Time right)
        {
            return left.m_time <= right.m_time;
        }

        public static bool operator >=(Time left, Time right)
        {
            return left.m_time >= right.m_time;
        }
        
        public static Time operator -(Time left, Time right)
        {
            return new Time(left.m_time - right.m_time);
        }
        
        public static Time operator +(Time left, Time right)
        {
            return new Time(left.m_time + right.m_time);
        }
        
        public static Time operator -(Time other)
        {
            return new Time(-other.m_time);
        }
    }
}