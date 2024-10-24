using NUnit.Framework;
using UnityEngine;
using Asteroids.Borders;

namespace Asteroids.Tests.EditMode
{
    public class MockScreenBoundsProvider : IScreenBoundsProvider
    {
        public float Left { get; set; }
        public float Right { get; set; }
        public float Top { get; set; }
        public float Bottom { get; set; }
    }

    [TestFixture]
    public class ScreenWrapHandlerTests
    {
        private MockScreenBoundsProvider mockBoundsProvider;
        private ScreenWrapHandler screenWrapHandler;

        private Vector2 testPosition;
        private Vector2 expectedPosition;

        private float leftBound;
        private float rightBound;
        private float topBound;
        private float bottomBound;

        [SetUp]
        public void Setup()
        {
            mockBoundsProvider = new MockScreenBoundsProvider();
            screenWrapHandler = new ScreenWrapHandler(mockBoundsProvider);
            
            leftBound = -10f;
            rightBound = 10f;
            topBound = 5f;
            bottomBound = -5f;

            mockBoundsProvider.Left = leftBound;
            mockBoundsProvider.Right = rightBound;
            mockBoundsProvider.Top = topBound;
            mockBoundsProvider.Bottom = bottomBound;
        }

        /// <summary>
        /// Tests horizontal wrapping when position exceeds the right boundary.
        /// </summary>
        [Test]
        public void TestWrapPositionExceedsRight()
        {
            testPosition = new Vector2(rightBound + 1f, 0f);
            expectedPosition = new Vector2(leftBound, 0f);

            Vector2 wrappedPosition = screenWrapHandler.WrapPosition(testPosition);

            Assert.AreEqual(expectedPosition, wrappedPosition);
        }

        /// <summary>
        /// Tests horizontal wrapping when position exceeds the left boundary.
        /// </summary>
        [Test]
        public void TestWrapPositionExceedsLeft()
        {
            testPosition = new Vector2(leftBound - 1f, 0f);
            expectedPosition = new Vector2(rightBound, 0f);

            Vector2 wrappedPosition = screenWrapHandler.WrapPosition(testPosition);

            Assert.AreEqual(expectedPosition, wrappedPosition);
        }

        /// <summary>
        /// Tests vertical wrapping when position exceeds the top boundary.
        /// </summary>
        [Test]
        public void TestWrapPositionExceedsTop()
        {
            testPosition = new Vector2(0f, topBound + 1f);
            expectedPosition = new Vector2(0f, bottomBound);

            Vector2 wrappedPosition = screenWrapHandler.WrapPosition(testPosition);

            Assert.AreEqual(expectedPosition, wrappedPosition);
        }

        /// <summary>
        /// Tests vertical wrapping when position exceeds the bottom boundary.
        /// </summary>
        [Test]
        public void TestWrapPositionExceedsBottom()
        {
            testPosition = new Vector2(0f, bottomBound - 1f);
            expectedPosition = new Vector2(0f, topBound);

            Vector2 wrappedPosition = screenWrapHandler.WrapPosition(testPosition);

            Assert.AreEqual(expectedPosition, wrappedPosition);
        }

        /// <summary>
        /// Tests that position remains unchanged when within bounds.
        /// </summary>
        [Test]
        public void TestWrapPositionWithinBounds()
        {
            testPosition = new Vector2(0f, 0f);
            expectedPosition = testPosition;

            Vector2 wrappedPosition = screenWrapHandler.WrapPosition(testPosition);

            Assert.AreEqual(expectedPosition, wrappedPosition);
        }

        /// <summary>
        /// Tests wrapping when position exceeds both horizontal and vertical boundaries.
        /// </summary>
        [Test]
        public void TestWrapPositionExceedsBoth()
        {
            testPosition = new Vector2(rightBound + 1f, topBound + 1f);
            expectedPosition = new Vector2(leftBound, bottomBound);

            Vector2 wrappedPosition = screenWrapHandler.WrapPosition(testPosition);

            Assert.AreEqual(expectedPosition, wrappedPosition);
        }
    }
}
