using System;

namespace StalkerZero.Infrastructure
{
    public class ReactiveProperty<T>
    {
        
        public Action<object, T> OnChanged;
        public T Value => m_value;

        private T m_value;

        public ReactiveProperty()
        {
            m_value = default(T);
        }

        public void SetValue(object sender, T value)
        {
            if (m_value.Equals(value))
                return;

            m_value = value;
            OnChanged?.Invoke(sender, value);
        }
    }
}
