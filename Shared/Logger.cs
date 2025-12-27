using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared
{
    public class Logger
    {
        public static event Action<string> OnAddLogData;

        public static void Log(string message)
        {
            OnAddLogData?.Invoke(message);
        }
    }
}
