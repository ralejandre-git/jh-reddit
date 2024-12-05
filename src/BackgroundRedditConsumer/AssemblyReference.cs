using System.Reflection;

namespace BackgroundRedditConsumer
{
    public static class AssemblyReference
    {
        public static readonly Assembly Assembly = typeof(AssemblyReference).Assembly;
    }
}
