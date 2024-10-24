using NUnit.Framework;
using Asteroids.Borders;

namespace Asteroids.Tests.EditMode
{
    public class MockCameraBoundsProvider : ICameraBoundsProvider
    {
        public float OrthographicSize { get; set; }
        public float AspectRatio { get; set; }
    }

    [TestFixture]
    public class ScreenBoundsProviderTests
    {
        private MockCameraBoundsProvider mockCameraBounds;
        private ScreenBoundsProvider screenBoundsProvider;

        private float testOrthographicSize;
        private float testAspectRatio;
        private float expectedTop;
        private float expectedBottom;
        private float expectedLeft;
        private float expectedRight;

        [SetUp]
        public void Setup()
        {
            mockCameraBounds = new MockCameraBoundsProvider();
            screenBoundsProvider = new ScreenBoundsProvider(mockCameraBounds);
        }

        /// <summary>
        /// Tests that UpdateBounds correctly calculates the screen bounds.
        /// </summary>
        [Test]
        public void TestUpdateBounds()
        {
            testOrthographicSize = 5f;
            testAspectRatio = 1.6f; // 16:10 aspect ratio
            mockCameraBounds.OrthographicSize = testOrthographicSize;
            mockCameraBounds.AspectRatio = testAspectRatio;

            expectedTop = testOrthographicSize;
            expectedBottom = -testOrthographicSize;
            expectedRight = testAspectRatio * testOrthographicSize;
            expectedLeft = -expectedRight;
            
            screenBoundsProvider.UpdateBounds();
            
            Assert.AreEqual(expectedTop, screenBoundsProvider.Top);
            Assert.AreEqual(expectedBottom, screenBoundsProvider.Bottom);
            Assert.AreEqual(expectedRight, screenBoundsProvider.Right);
            Assert.AreEqual(expectedLeft, screenBoundsProvider.Left);
        }

        /// <summary>
        /// Verifies that Left and Right properties are calculated correctly.
        /// </summary>
        [Test]
        public void TestLeftRightProperties()
        {
            testOrthographicSize = 3f;
            testAspectRatio = 2f; // 2:1 aspect ratio
            mockCameraBounds.OrthographicSize = testOrthographicSize;
            mockCameraBounds.AspectRatio = testAspectRatio;

            expectedRight = testAspectRatio * testOrthographicSize;
            expectedLeft = -expectedRight;
            
            screenBoundsProvider.UpdateBounds();
            
            Assert.AreEqual(expectedRight, screenBoundsProvider.Right);
            Assert.AreEqual(expectedLeft, screenBoundsProvider.Left);
        }

        /// <summary>
        /// Verifies that Top and Bottom properties are calculated correctly.
        /// </summary>
        [Test]
        public void TestTopBottomProperties()
        {
            testOrthographicSize = 4f;
            mockCameraBounds.OrthographicSize = testOrthographicSize;

            expectedTop = testOrthographicSize;
            expectedBottom = -testOrthographicSize;
            
            screenBoundsProvider.UpdateBounds();
            
            Assert.AreEqual(expectedTop, screenBoundsProvider.Top);
            Assert.AreEqual(expectedBottom, screenBoundsProvider.Bottom);
        }
    }
}
