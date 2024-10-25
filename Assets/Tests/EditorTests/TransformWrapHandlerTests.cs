using NUnit.Framework;
using UnityEngine;
using Asteroids.Borders;
using System.Collections.Generic;

namespace Asteroids.Tests.EditMode
{
    [TestFixture]
    public class TransformWrapHandlerTests
    {
        private MockScreenBoundsProvider mockBoundsProvider;
        private ScreenBoundsHandler _screenBoundsHandler;
        private TransformWrapHandler transformWrapHandler;
        private List<GameObject> testGameObjects;

        private float leftBound;
        private float rightBound;
        private float topBound;
        private float bottomBound;

        [SetUp]
        public void Setup()
        {
            leftBound = -10f;
            rightBound = 10f;
            topBound = 5f;
            bottomBound = -5f;

            mockBoundsProvider = new MockScreenBoundsProvider
            {
                Left = leftBound,
                Right = rightBound,
                Top = topBound,
                Bottom = bottomBound
            };
            _screenBoundsHandler = new ScreenBoundsHandler(mockBoundsProvider);
            transformWrapHandler = new TransformWrapHandler(_screenBoundsHandler);
            testGameObjects = new List<GameObject>();
        }

        [TearDown]
        public void TearDown()
        {
            foreach (var go in testGameObjects)
            {
                Object.DestroyImmediate(go);
            }
        }

        /// <summary>
        /// Tests that FixedTick wraps the positions of registered transforms.
        /// </summary>
        [Test]
        public void TestFixedTickWrapsTransforms()
        {
            GameObject obj1 = new GameObject("TestObject1");
            testGameObjects.Add(obj1);
            obj1.transform.position = new Vector2(rightBound + 1f, 0f);
            transformWrapHandler.RegisterTransform(obj1.transform);

            Vector2 expectedPosition = new Vector2(leftBound, 0f);

            transformWrapHandler.FixedTick();

            Assert.AreEqual(expectedPosition, (Vector2)obj1.transform.position);
        }

        /// <summary>
        /// Tests that FixedTick ignores transforms that are not registered.
        /// </summary>
        [Test]
        public void TestFixedTickIgnoresUnregistered()
        {
            GameObject obj1 = new GameObject("TestObject1");
            testGameObjects.Add(obj1);
            obj1.transform.position = new Vector2(rightBound + 1f, 0f);

            Vector2 initialPosition = obj1.transform.position;

            transformWrapHandler.FixedTick();

            Assert.AreEqual(initialPosition, (Vector2)obj1.transform.position);
        }

        /// <summary>
        /// Tests that RegisterTransform adds a transform to be wrapped.
        /// </summary>
        [Test]
        public void TestRegisterTransform()
        {
            GameObject obj1 = new GameObject("TestObject1");
            testGameObjects.Add(obj1);

            transformWrapHandler.RegisterTransform(obj1.transform);

            obj1.transform.position = new Vector2(rightBound + 1f, 0f);
            Vector2 expectedPosition = new Vector2(leftBound, 0f);

            transformWrapHandler.FixedTick();

            Assert.AreEqual(expectedPosition, (Vector2)obj1.transform.position);
        }

        /// <summary>
        /// Tests that UnregisterTransform removes a transform from being wrapped.
        /// </summary>
        [Test]
        public void TestUnregisterTransform()
        {
            GameObject obj1 = new GameObject("TestObject1");
            testGameObjects.Add(obj1);
            obj1.transform.position = new Vector2(rightBound + 1f, 0f);
            transformWrapHandler.RegisterTransform(obj1.transform);

            transformWrapHandler.UnregisterTransform(obj1.transform);

            Vector2 initialPosition = obj1.transform.position;

            transformWrapHandler.FixedTick();

            Assert.AreEqual(initialPosition, (Vector2)obj1.transform.position);
        }

        /// <summary>
        /// Tests that FixedTick safely skips null transforms.
        /// </summary>
        [Test]
        public void TestFixedTickSkipsNull()
        {
            GameObject obj1 = new GameObject("TestObject1");
            transformWrapHandler.RegisterTransform(obj1.transform);
            Object.DestroyImmediate(obj1);
            testGameObjects.Remove(obj1);

            Assert.DoesNotThrow(() => transformWrapHandler.FixedTick());
        }

        /// <summary>
        /// Tests that FixedTick skips inactive transforms.
        /// </summary>
        [Test]
        public void TestFixedTickSkipsInactive()
        {
            GameObject obj1 = new GameObject("TestObject1");
            testGameObjects.Add(obj1);
            obj1.SetActive(false);
            transformWrapHandler.RegisterTransform(obj1.transform);
            obj1.transform.position = new Vector2(rightBound + 1f, 0f);

            Vector2 initialPosition = obj1.transform.position;

            transformWrapHandler.FixedTick();

            Assert.AreEqual(initialPosition, (Vector2)obj1.transform.position);
        }
    }
}
