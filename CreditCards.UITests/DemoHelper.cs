using System.Threading;

namespace CreditCards.UITests
{
    internal static class DemoHelper
    {
        /// <summary>
        /// brief delay to slow down browser interactions
        /// for demo purposes
        /// </summary>
        public static void Pause(int secondsToPause = 3000)
        {
            Thread.Sleep(secondsToPause);
        }
    }
}
