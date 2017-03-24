using Xunit;

namespace Ploeh.Albedo.UnitTests
{
    public class EventsTests
    {
        [Fact]
        public void SelectEventReturnsCorrectEvent()
        {
            var sut = new Events<TypeWithEvents>();
            var expected = TypeWithEvents.LocalEvent;

            var actual = sut.Select(x => x.TheEvent += null);

            Assert.Equal(expected, actual);
        }
    }
}