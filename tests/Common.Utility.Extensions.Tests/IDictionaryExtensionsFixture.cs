using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Specialized;
using System.Collections;

namespace Common.Utility.Extensions.Tests
{
    /// <summary>
    /// Summary description for IDictionaryExtensionsFixture
    /// </summary>
    [TestClass]
    public class IDictionaryExtensionsFixture
    {
        [TestMethod]
        public void ShouldTryGetValue()
        {
            // arrange
            var d = new OrderedDictionary();
            d.Add("1", "some value");
            d.Add("2", 123);

            // act
            string s;
            var result = d.TryGetValue("1", out s);

            // assert
            Assert.IsTrue(result);
            Assert.AreEqual("some value", s);

            // act
            int i;
            result = d.TryGetValue("2", out i);

            // assert
            Assert.IsTrue(result);
            Assert.AreEqual(123, i);
        }

        [TestMethod]
        public void ShouldNotTryGetValue()
        {
            // arrange
            var d = new OrderedDictionary();
            d.Add("2", 123);

            // act
            string s;
            var result = d.TryGetValue("1", out s);

            // assert
            Assert.IsFalse(result);
            Assert.IsNull(s);
        }

        [TestMethod]
        public void ShouldGetValue()
        {
            // arrange
            var ht = new Hashtable();
            ht.Add("a", 1);
            ht.Add("b", "somevalue");

            // act
            var a = ht.GetValue<int>("a");
            var b = ht.GetValue<string>("b");

            // assert
            Assert.AreEqual(1, a);
            Assert.AreEqual("somevalue", b);
        }

        [TestMethod]
        public void ShouldNotGetValue()
        {
            // arrange
            var ht = new Hashtable();
            ht.Add("a", 1);
            ht.Add("b", "somevalue");

            // act
            var c = ht.GetValue<int>("c");

            // assert
            Assert.AreEqual(0, c);
        }

        [TestMethod]
        public void ShouldContainAllKeys()
        {
            // arrange
            var ht = new Hashtable();
            ht.Add("a", 1);
            ht.Add("b", "somevalue");

            // act
            var actual = ht.ContainAllKeys(new[] { "a", "b" });

            // assert
            Assert.IsTrue(actual);
        }

        [TestMethod]
        public void ShouldNotContainAllKeys()
        {
            // arrange
            var ht = new Hashtable();
            ht.Add("a", 1);
            ht.Add("b", "somevalue");

            // act
            var actual = ht.ContainAllKeys(new[] { "a", "b", "c" });

            // assert
            Assert.IsFalse(actual);
        }

        [TestMethod]
        public void ShouldContainAnyKey()
        {
            // arrange
            var ht = new Hashtable();
            ht.Add("a", 1);
            ht.Add("b", "somevalue");

            // act
            var actual = ht.ContainAnyKey(new[] { "a", "b", "c" });

            // assert
            Assert.IsTrue(actual);
        }

        [TestMethod]
        public void ShouldNotContainAnyKey()
        {
            // arrange
            var ht = new Hashtable();
            ht.Add("a", 1);
            ht.Add("b", "somevalue");

            // act
            var actual = ht.ContainAnyKey(new[] { "c", "d" });

            // assert
            Assert.IsFalse(actual);
        }

        [TestMethod]
        public void ShouldTryRemove()
        {
            // arrange
            var ht = new Hashtable();
            ht.Add("a", 1);
            ht.Add("b", "somevalue");

            // act
            var actual = ht.TryRemove("a");

            // assert
            Assert.IsTrue(actual);
            Assert.IsNull(ht["a"]);
        }

        [TestMethod]
        public void ShouldNotTryRemove()
        {
            // arrange
            var ht = new Hashtable();
            ht.Add("a", 1);
            ht.Add("b", "somevalue");

            // act
            var actual = ht.TryRemove("c");

            // assert
            Assert.IsFalse(actual);
            Assert.AreEqual(2, ht.Count);
        }

        [TestMethod]
        public void ShouldAddOrUpdate()
        {
            // arrange
            var ht = new Hashtable();

            // act
            ht.AddOrUpdate("a", 1);

            // assert
            Assert.AreEqual(1, ht["a"]);

            // act
            ht.AddOrUpdate("a", 2);

            // assert
            Assert.AreEqual(2, ht["a"]);
        }
    }
}
