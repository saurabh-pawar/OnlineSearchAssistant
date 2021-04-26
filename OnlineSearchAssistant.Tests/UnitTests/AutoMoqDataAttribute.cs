using AutoFixture;
using AutoFixture.AutoMoq;
using AutoFixture.Xunit2;

namespace OnlineSearchAssistant.Tests.UnitTests
{
    public class AutoMoqDataAttribute : AutoDataAttribute
    {
        public AutoMoqDataAttribute()
          : base(() => new Fixture().Customize(new AutoMoqCustomization()))
        {
        }
    }
}
