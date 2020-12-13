using Xunit;

namespace Haka.Hybrid.Test
{
    public class SvgHybrid_Test : BaseTest
    {
        [Fact]
        public void MockRender()
        {
            var svgHybrid = new SvgHybrid
            {
                Source = "spinner.svg"
            };

            Assert.NotNull(svgHybrid);
        }
    }
}
