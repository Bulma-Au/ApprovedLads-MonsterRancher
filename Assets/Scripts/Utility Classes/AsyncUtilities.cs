using System.Threading.Tasks;

namespace Utility_Classes
{
    public static class AsyncUtilities
    {
        public static async void WrapErrors ( this Task task)
        {
            await task;
        } 
    }
}