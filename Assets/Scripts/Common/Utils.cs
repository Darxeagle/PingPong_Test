using System;

namespace Assets.Common.Scripts
{
    class Utils
    {
        public static T AsType<T>(object obj)
        {
            try
            {
                return (T)obj;
            }
            catch (Exception)
            {                
                throw new Exception("Cannot convert type");
            }
        }
    }
}
