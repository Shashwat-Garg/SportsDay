using Microsoft.Extensions.Logging;
using Moq;

namespace sportsdayapi.UnitTests.MockInitializers
{
    internal static class MiscMockObjects
    {
        public static ILogger<T> GetFakeLogger<T>()
        {
            return new Mock<ILogger<T>>().Object;
        }
    }
}
