using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;

namespace Common.Utility.Configuration.Tests
{
    [TestClass]
    public class DictionaryConfigurationParserFixture
    {
        [TestMethod]
        public void ShouldFailResolveConfigNonExisting()
        {
            try
            {
                DictionaryConfigurationParser.Resolve<TestPluginConfig>("TestPlugin", "someSettings");
            }
            catch (ConfigurationErrorsException ex)
            {
                Assert.AreEqual("Unable to find someSettings in the configuration file", ex.Message);
                return;
            }

            Assert.Fail("ConfigurationErrorsException must be thrown");
        }

        [TestMethod]
        public void ShouldResolveConfig()
        {
            var config = DictionaryConfigurationParser.Resolve<TestPluginConfig>("TestPlugin", "plugSettings");

            Assert.IsNotNull(config);
            Assert.AreEqual("some string", config.String);
            Assert.AreEqual(32767, config.Int16);
            Assert.AreEqual(1, config.Int32);
            Assert.AreEqual(987654321, config.Int64);
            Assert.AreEqual(0.123m, config.Decimal);
            Assert.AreEqual(2.123, config.Double);
            Assert.AreEqual(1.23f, config.Single);
            Assert.AreEqual(true, config.Boolean);
            Assert.AreEqual(3, config.StringList.Count);
            Assert.AreEqual("a", config.StringList[0]);
            Assert.AreEqual("b", config.StringList[1]);
            Assert.AreEqual("c", config.StringList[2]);
            Assert.AreEqual(4, config.IntegerArray.Length);
            Assert.AreEqual(3, config.ArrayList.Count);
            Assert.AreEqual("a", config.ArrayList[0]);
            Assert.AreEqual("b", config.ArrayList[1]);
            Assert.AreEqual("c", config.ArrayList[2]);
            Assert.AreEqual(Direction.Outbound, config.Enumeration);
            Assert.AreEqual("10.88.22.95", config.Ip);
            Assert.AreEqual("40007", config.Port);
            //Assert.AreEqual("di123", config.Encrypted);
            Assert.AreEqual(32767, config.UInt16);
            Assert.AreEqual(1u, config.UInt32);
            Assert.AreEqual(987654321ul, config.UInt64);
        }

        [TestMethod]
        public void ShouldFailResolveConfig()
        {
            string[] expectedErrors = new[] {"Config Error: TestPluginFail_String [Value cannot be blank/empty] ",
                "Config Error: TestPluginFail_Int16 [Value must be numeric/number (Int16 type)] ",
                "Config Error: TestPluginFail_Int32 [Value must be numeric/number (Int32 type)] ",
                "Config Error: TestPluginFail_Int64 [Value must be numeric/number (Int64 type)] ",
                "Config Error: TestPluginFail_UInt16 [Value must be non-negative numeric/number (UInt16 type)] ",
                "Config Error: TestPluginFail_UInt32 [Value must be non-negative numeric/number (UInt32 type)] ",
                "Config Error: TestPluginFail_UInt64 [Value must be non-negative numeric/number (UInt64 type)] ",
                "Config Error: TestPluginFail_Decimal [Value must be numeric/number (Decimal type)] ",
                "Config Error: TestPluginFail_Double [Value must be numeric/number (Double type)] ",
                "Config Error: TestPluginFail_Single [Value must be numeric/number (Single type)] ",
                "Config Error: TestPluginFail_Boolean [Value must be a Boolean type. Case-insensitive allowed values: ([True = true, yes, 1], [False = false, no, 0])] ",
                "Config Error: TestPluginFail_IntegerArray [Value must be numeric/number (Int32 type)] ",
                "Config Error: TestPluginFail_Enumeration [otbound is not defined in Common.Utility.Configuration.Tests.Direction enumeration. Valid values: Outbound,Inbound)] This is a sample error message",
                //"Config Error: TestPluginFail_Encrypted [Value cannot be blank/empty] ",
                "Config Error: TestPluginFail_StringCustomLoadMethod [Tcp = 10.88.22.95 is not correct (ip:port)] "};

            try
            {
                var config = DictionaryConfigurationParser.Resolve<TestPluginConfig>("TestPluginFail", "plugSettings");
            }
            catch (ConfigurationErrorsException ex)
            {
                Assert.AreEqual(expectedErrors.Length, ex.Message.Split(new[] { Environment.NewLine }, StringSplitOptions.None).Length);

                foreach (var error in expectedErrors)
                {
                    if (!ex.Message.Contains(error))
                        Assert.Fail("Error should be thrown: " + error);
                }
                return;
            }

            Assert.Fail("ConfigurationErrorsException must be thrown");
        }

        [TestMethod]
        public void ShouldResolveConfigAppSettings()
        {
            var config = DictionaryConfigurationParser.Resolve<TestPluginConfig>("TestPlugin", "appSettings");

            Assert.IsNotNull(config);
            Assert.AreEqual("some string", config.String);
            Assert.AreEqual(32767, config.Int16);
            Assert.AreEqual(1, config.Int32);
            Assert.AreEqual(987654321, config.Int64);
            Assert.AreEqual(0.123m, config.Decimal);
            Assert.AreEqual(2.123, config.Double);
            Assert.AreEqual(1.23f, config.Single);
            Assert.AreEqual(true, config.Boolean);
            Assert.AreEqual(3, config.StringList.Count);
            Assert.AreEqual("a", config.StringList[0]);
            Assert.AreEqual("b", config.StringList[1]);
            Assert.AreEqual("c", config.StringList[2]);
            Assert.AreEqual(4, config.IntegerArray.Length);
            Assert.AreEqual(3, config.ArrayList.Count);
            Assert.AreEqual("a", config.ArrayList[0]);
            Assert.AreEqual("b", config.ArrayList[1]);
            Assert.AreEqual("c", config.ArrayList[2]);
            Assert.AreEqual(Direction.Outbound, config.Enumeration);
            Assert.AreEqual("10.88.22.95", config.Ip);
            Assert.AreEqual("40007", config.Port);
            //Assert.AreEqual("di123", config.Encrypted);
            Assert.AreEqual(32767, config.UInt16);
            Assert.AreEqual(1u, config.UInt32);
            Assert.AreEqual(987654321ul, config.UInt64);
        }

        [TestMethod]
        public void ShouldFailResolveConfigAppSettings()
        {
            string[] expectedErrors = new[] {"Config Error: TestPluginFail_String [Value cannot be blank/empty] ",
                "Config Error: TestPluginFail_Int16 [Value must be numeric/number (Int16 type)] ",
                "Config Error: TestPluginFail_Int32 [Value must be numeric/number (Int32 type)] ",
                "Config Error: TestPluginFail_Int64 [Value must be numeric/number (Int64 type)] ",
                "Config Error: TestPluginFail_UInt16 [Value must be non-negative numeric/number (UInt16 type)] ",
                "Config Error: TestPluginFail_UInt32 [Value must be non-negative numeric/number (UInt32 type)] ",
                "Config Error: TestPluginFail_UInt64 [Value must be non-negative numeric/number (UInt64 type)] ",
                "Config Error: TestPluginFail_Decimal [Value must be numeric/number (Decimal type)] ",
                "Config Error: TestPluginFail_Double [Value must be numeric/number (Double type)] ",
                "Config Error: TestPluginFail_Single [Value must be numeric/number (Single type)] ",
                "Config Error: TestPluginFail_Boolean [Value must be a Boolean type. Case-insensitive allowed values: ([True = true, yes, 1], [False = false, no, 0])] ",
                "Config Error: TestPluginFail_IntegerArray [Value must be numeric/number (Int32 type)] ",
                "Config Error: TestPluginFail_Enumeration [otbound is not defined in Common.Utility.Configuration.Tests.Direction enumeration. Valid values: Outbound,Inbound)] This is a sample error message",
                //"Config Error: TestPluginFail_Encrypted [Value cannot be blank/empty] ",
                "Config Error: TestPluginFail_StringCustomLoadMethod [Tcp = 10.88.22.95 is not correct (ip:port)] "};

            try
            {
                var config = DictionaryConfigurationParser.Resolve<TestPluginConfig>("TestPluginFail", "appSettings");
            }
            catch (ConfigurationErrorsException ex)
            {
                Assert.AreEqual(expectedErrors.Length, ex.Message.Split(new[] { Environment.NewLine }, StringSplitOptions.None).Length);

                foreach (var error in expectedErrors)
                {
                    if (!ex.Message.Contains(error))
                        Assert.Fail("Error should be thrown: " + error);
                }
                return;
            }

            Assert.Fail("ConfigurationErrorsException must be thrown");
        }

        [TestMethod]
        public void ShouldFailRequired()
        {
            try
            {
                var ht = new Hashtable();
                var config = new TestPluginConfig();
                var attrib = new ConfigurationEntryAttribute();

                DictionaryConfigurationParser.ResolveConfigurationEntry(config, "Test_Int32", typeof(TestPluginConfig).GetProperty("Int32"), attrib, ht);
            }
            catch (ConfigurationErrorsException ex)
            {
                Assert.AreEqual("Test_Int32 is not found in the configuration (Expected type: System.Int32)", ex.Message);
                return;
            }

            Assert.Fail("ConfigurationErrorsException must be thrown");
        }

        [TestMethod]
        public void ShouldPassInt32RequiredConfig()
        {
            var ht = new Hashtable();
            ht.Add("Test_Int32", "1");

            var config = new TestPluginConfig();
            var attrib = new ConfigurationEntryAttribute();

            DictionaryConfigurationParser.ResolveConfigurationEntry(config, "Test_Int32", typeof(TestPluginConfig).GetProperty("Int32"), attrib, ht);

            Assert.AreEqual(1, config.Int32);
        }

        [TestMethod]
        public void ShouldFailInt32RequiredConfig()
        {
            try
            {
                var ht = new Hashtable();
                ht.Add("Test_Int32", "x");

                var config = new TestPluginConfig();
                var attrib = new ConfigurationEntryAttribute();

                DictionaryConfigurationParser.ResolveConfigurationEntry(config, "Test_Int32", typeof(TestPluginConfig).GetProperty("Int32"), attrib, ht);
            }
            catch (ConfigurationErrorsException ex)
            {
                Assert.AreEqual("Value must be numeric/number (Int32 type)", ex.Message);
                return;
            }

            Assert.Fail("ConfigurationErrorsException must be thrown");
        }

        [TestMethod]
        public void ShouldPassInt32RequiredConfigWithDefaultValue()
        {
            var ht = new Hashtable();
            ht.Add("Test_Int32", "1");

            var config = new TestPluginConfig();
            var attrib = new ConfigurationEntryAttribute { DefaultValue = 2 };

            DictionaryConfigurationParser.ResolveConfigurationEntry(config, "Test_Int32", typeof(TestPluginConfig).GetProperty("Int32"), attrib, ht);

            Assert.AreEqual(1, config.Int32);
        }

        [TestMethod]
        public void ShouldFailInt32RequiredConfigWithDefaultValue()
        {
            try
            {
                var ht = new Hashtable();
                ht.Add("Test_Int32", "x");

                var config = new TestPluginConfig();
                var attrib = new ConfigurationEntryAttribute { DefaultValue = 2 };

                DictionaryConfigurationParser.ResolveConfigurationEntry(config, "Test_Int32", typeof(TestPluginConfig).GetProperty("Int32"), attrib, ht);
            }
            catch (ConfigurationErrorsException ex)
            {
                Assert.AreEqual("Value must be numeric/number (Int32 type)", ex.Message);
                return;
            }

            Assert.Fail("ConfigurationErrorsException must be thrown");
        }

        [TestMethod]
        public void ShouldPassInt32Optional()
        {
            var ht = new Hashtable();
            ht.Add("Test_Int32", "1");

            var config = new TestPluginConfig();
            var attrib = new ConfigurationEntryAttribute { IsRequired = false };

            DictionaryConfigurationParser.ResolveConfigurationEntry(config, "Test_Int32", typeof(TestPluginConfig).GetProperty("Int32"), attrib, ht);

            Assert.AreEqual(1, config.Int32);
        }

        [TestMethod]
        public void ShouldFailInt32OptionalConfig()
        {
            try
            {
                var ht = new Hashtable();
                ht.Add("Test_Int32", "x");

                var config = new TestPluginConfig();
                var attrib = new ConfigurationEntryAttribute { IsRequired = false, DefaultValue = 2 };

                DictionaryConfigurationParser.ResolveConfigurationEntry(config, "Test_Int32", typeof(TestPluginConfig).GetProperty("Int32"), attrib, ht);
            }
            catch (ConfigurationErrorsException ex)
            {
                Assert.AreEqual("Value must be numeric/number (Int32 type)", ex.Message);
                return;
            }

            Assert.Fail("ConfigurationErrorsException must be thrown");
        }

        [TestMethod]
        public void ShouldPassInt32OptionalWithDefaultValue()
        {
            var ht = new Hashtable();
            var config = new TestPluginConfig();
            var attrib = new ConfigurationEntryAttribute { IsRequired = false, DefaultValue = 2 };

            DictionaryConfigurationParser.ResolveConfigurationEntry(config, "Test_Int32", typeof(TestPluginConfig).GetProperty("Int32"), attrib, ht);

            Assert.AreEqual(2, config.Int32);
        }

        [TestMethod]
        public void ShouldFailInt32OptionalConfigWithDefaultValue()
        {
            try
            {
                var ht = new Hashtable();
                ht.Add("Test_Int32", "x");

                var config = new TestPluginConfig();
                var attrib = new ConfigurationEntryAttribute { IsRequired = false, DefaultValue = 2 };

                DictionaryConfigurationParser.ResolveConfigurationEntry(config, "Test_Int32", typeof(TestPluginConfig).GetProperty("Int32"), attrib, ht);
            }
            catch (ConfigurationErrorsException ex)
            {
                Assert.AreEqual("Value must be numeric/number (Int32 type)", ex.Message);
                return;
            }

            Assert.Fail("ConfigurationErrorsException must be thrown");
        }

        [TestMethod]
        public void ShouldPassInt64RequiredConfig()
        {
            var ht = new Hashtable();
            ht.Add("Test_Int64", "1");

            var config = new TestPluginConfig();
            var attrib = new ConfigurationEntryAttribute();

            DictionaryConfigurationParser.ResolveConfigurationEntry(config, "Test_Int64", typeof(TestPluginConfig).GetProperty("Int64"), attrib, ht);

            Assert.AreEqual(1, config.Int64);
        }

        [TestMethod]
        public void ShouldFailInt64RequiredConfig()
        {
            try
            {
                var ht = new Hashtable();
                ht.Add("Test_Int64", "x");

                var config = new TestPluginConfig();
                var attrib = new ConfigurationEntryAttribute();

                DictionaryConfigurationParser.ResolveConfigurationEntry(config, "Test_Int64", typeof(TestPluginConfig).GetProperty("Int64"), attrib, ht);
            }
            catch (ConfigurationErrorsException ex)
            {
                Assert.AreEqual("Value must be numeric/number (Int64 type)", ex.Message);
                return;
            }

            Assert.Fail("ConfigurationErrorsException must be thrown");
        }

        [TestMethod]
        public void ShouldPassInt64RequiredConfigWithDefaultValue()
        {
            var ht = new Hashtable();
            ht.Add("Test_Int64", "1");

            var config = new TestPluginConfig();
            var attrib = new ConfigurationEntryAttribute { DefaultValue = 2 };

            DictionaryConfigurationParser.ResolveConfigurationEntry(config, "Test_Int64", typeof(TestPluginConfig).GetProperty("Int64"), attrib, ht);

            Assert.AreEqual(1, config.Int64);
        }

        [TestMethod]
        public void ShouldFailInt64RequiredConfigWithDefaultValue()
        {
            try
            {
                var ht = new Hashtable();
                ht.Add("Test_Int64", "x");

                var config = new TestPluginConfig();
                var attrib = new ConfigurationEntryAttribute { DefaultValue = 2 };

                DictionaryConfigurationParser.ResolveConfigurationEntry(config, "Test_Int64", typeof(TestPluginConfig).GetProperty("Int64"), attrib, ht);
            }
            catch (ConfigurationErrorsException ex)
            {
                Assert.AreEqual("Value must be numeric/number (Int64 type)", ex.Message);
                return;
            }

            Assert.Fail("ConfigurationErrorsException must be thrown");
        }

        [TestMethod]
        public void ShouldPassInt64Optional()
        {
            var ht = new Hashtable();
            ht.Add("Test_Int64", "1");

            var config = new TestPluginConfig();
            var attrib = new ConfigurationEntryAttribute { IsRequired = false };

            DictionaryConfigurationParser.ResolveConfigurationEntry(config, "Test_Int64", typeof(TestPluginConfig).GetProperty("Int64"), attrib, ht);

            Assert.AreEqual(1, config.Int64);
        }

        [TestMethod]
        public void ShouldFailInt64OptionalConfig()
        {
            try
            {
                var ht = new Hashtable();
                ht.Add("Test_Int64", "x");

                var config = new TestPluginConfig();
                var attrib = new ConfigurationEntryAttribute { IsRequired = false, DefaultValue = 2 };

                DictionaryConfigurationParser.ResolveConfigurationEntry(config, "Test_Int64", typeof(TestPluginConfig).GetProperty("Int64"), attrib, ht);
            }
            catch (ConfigurationErrorsException ex)
            {
                Assert.AreEqual("Value must be numeric/number (Int64 type)", ex.Message);
                return;
            }

            Assert.Fail("ConfigurationErrorsException must be thrown");
        }

        [TestMethod]
        public void ShouldPassInt64OptionalWithDefaultValue()
        {
            var ht = new Hashtable();
            var config = new TestPluginConfig();
            var attrib = new ConfigurationEntryAttribute { IsRequired = false, DefaultValue = 2 };

            DictionaryConfigurationParser.ResolveConfigurationEntry(config, "Test_Int64", typeof(TestPluginConfig).GetProperty("Int64"), attrib, ht);

            Assert.AreEqual(2, config.Int64);
        }

        [TestMethod]
        public void ShouldFailInt64OptionalConfigWithDefaultValue()
        {
            try
            {
                var ht = new Hashtable();
                ht.Add("Test_Int64", "x");

                var config = new TestPluginConfig();
                var attrib = new ConfigurationEntryAttribute { IsRequired = false, DefaultValue = 2 };

                DictionaryConfigurationParser.ResolveConfigurationEntry(config, "Test_Int64", typeof(TestPluginConfig).GetProperty("Int64"), attrib, ht);
            }
            catch (ConfigurationErrorsException ex)
            {
                Assert.AreEqual("Value must be numeric/number (Int64 type)", ex.Message);
                return;
            }

            Assert.Fail("ConfigurationErrorsException must be thrown");
        }

        [TestMethod]
        public void ShouldPassInt16RequiredConfig()
        {
            var ht = new Hashtable();
            ht.Add("Test_Int16", "1");

            var config = new TestPluginConfig();
            var attrib = new ConfigurationEntryAttribute();

            DictionaryConfigurationParser.ResolveConfigurationEntry(config, "Test_Int16", typeof(TestPluginConfig).GetProperty("Int16"), attrib, ht);

            Assert.AreEqual(1, config.Int16);
        }

        [TestMethod]
        public void ShouldFailInt16RequiredConfig()
        {
            try
            {
                var ht = new Hashtable();
                ht.Add("Test_Int16", "x");

                var config = new TestPluginConfig();
                var attrib = new ConfigurationEntryAttribute();

                DictionaryConfigurationParser.ResolveConfigurationEntry(config, "Test_Int16", typeof(TestPluginConfig).GetProperty("Int16"), attrib, ht);
            }
            catch (ConfigurationErrorsException ex)
            {
                Assert.AreEqual("Value must be numeric/number (Int16 type)", ex.Message);
                return;
            }

            Assert.Fail("ConfigurationErrorsException must be thrown");
        }

        [TestMethod]
        public void ShouldPassInt16RequiredConfigWithDefaultValue()
        {
            var ht = new Hashtable();
            ht.Add("Test_Int16", "1");

            var config = new TestPluginConfig();
            var attrib = new ConfigurationEntryAttribute { DefaultValue = 2 };

            DictionaryConfigurationParser.ResolveConfigurationEntry(config, "Test_Int16", typeof(TestPluginConfig).GetProperty("Int16"), attrib, ht);

            Assert.AreEqual(1, config.Int16);
        }

        [TestMethod]
        public void ShouldFailInt16RequiredConfigWithDefaultValue()
        {
            try
            {
                var ht = new Hashtable();
                ht.Add("Test_Int16", "x");

                var config = new TestPluginConfig();
                var attrib = new ConfigurationEntryAttribute { DefaultValue = 2 };

                DictionaryConfigurationParser.ResolveConfigurationEntry(config, "Test_Int16", typeof(TestPluginConfig).GetProperty("Int16"), attrib, ht);
            }
            catch (ConfigurationErrorsException ex)
            {
                Assert.AreEqual("Value must be numeric/number (Int16 type)", ex.Message);
                return;
            }

            Assert.Fail("ConfigurationErrorsException must be thrown");
        }

        [TestMethod]
        public void ShouldPassInt16Optional()
        {
            var ht = new Hashtable();
            ht.Add("Test_Int16", "1");

            var config = new TestPluginConfig();
            var attrib = new ConfigurationEntryAttribute { IsRequired = false };

            DictionaryConfigurationParser.ResolveConfigurationEntry(config, "Test_Int16", typeof(TestPluginConfig).GetProperty("Int16"), attrib, ht);

            Assert.AreEqual(1, config.Int16);
        }

        [TestMethod]
        public void ShouldFailInt16OptionalConfig()
        {
            try
            {
                var ht = new Hashtable();
                ht.Add("Test_Int16", "x");

                var config = new TestPluginConfig();
                var attrib = new ConfigurationEntryAttribute { IsRequired = false, DefaultValue = 2 };

                DictionaryConfigurationParser.ResolveConfigurationEntry(config, "Test_Int16", typeof(TestPluginConfig).GetProperty("Int16"), attrib, ht);
            }
            catch (ConfigurationErrorsException ex)
            {
                Assert.AreEqual("Value must be numeric/number (Int16 type)", ex.Message);
                return;
            }

            Assert.Fail("ConfigurationErrorsException must be thrown");
        }

        [TestMethod]
        public void ShouldPassInt16OptionalWithDefaultValue()
        {
            var ht = new Hashtable();
            var config = new TestPluginConfig();
            var attrib = new ConfigurationEntryAttribute { IsRequired = false, DefaultValue = 2 };

            DictionaryConfigurationParser.ResolveConfigurationEntry(config, "Test_Int16", typeof(TestPluginConfig).GetProperty("Int16"), attrib, ht);

            Assert.AreEqual(2, config.Int16);
        }

        [TestMethod]
        public void ShouldFailInt16OptionalConfigWithDefaultValue()
        {
            try
            {
                var ht = new Hashtable();
                ht.Add("Test_Int16", "x");

                var config = new TestPluginConfig();
                var attrib = new ConfigurationEntryAttribute { IsRequired = false, DefaultValue = 2 };

                DictionaryConfigurationParser.ResolveConfigurationEntry(config, "Test_Int16", typeof(TestPluginConfig).GetProperty("Int16"), attrib, ht);
            }
            catch (ConfigurationErrorsException ex)
            {
                Assert.AreEqual("Value must be numeric/number (Int16 type)", ex.Message);
                return;
            }

            Assert.Fail("ConfigurationErrorsException must be thrown");
        }

        [TestMethod]
        public void ShouldPassStringRequiredConfig()
        {
            var ht = new Hashtable();
            ht.Add("Test_String", "dog");

            var config = new TestPluginConfig();
            var attrib = new ConfigurationEntryAttribute();

            DictionaryConfigurationParser.ResolveConfigurationEntry(config, "Test_String", typeof(TestPluginConfig).GetProperty("String"), attrib, ht);

            Assert.AreEqual("dog", config.String);
        }

        [TestMethod]
        public void ShouldFailStringRequiredConfig()
        {
            try
            {
                var ht = new Hashtable();
                ht.Add("Test_String", "");

                var config = new TestPluginConfig();
                var attrib = new ConfigurationEntryAttribute();

                DictionaryConfigurationParser.ResolveConfigurationEntry(config, "Test_String", typeof(TestPluginConfig).GetProperty("String"), attrib, ht);
            }
            catch (ConfigurationErrorsException ex)
            {
                Assert.AreEqual("Value cannot be blank/empty", ex.Message);
                return;
            }

            Assert.Fail("ConfigurationErrorsException must be thrown");
        }

        [TestMethod]
        public void ShouldPassStringRequiredConfigWithDefaultValue()
        {
            var ht = new Hashtable();
            ht.Add("Test_String", "cat");

            var config = new TestPluginConfig();
            var attrib = new ConfigurationEntryAttribute { DefaultValue = 2 };

            DictionaryConfigurationParser.ResolveConfigurationEntry(config, "Test_String", typeof(TestPluginConfig).GetProperty("String"), attrib, ht);

            Assert.AreEqual("cat", config.String);
        }

        [TestMethod]
        public void ShouldFailStringRequiredConfigWithDefaultValue()
        {
            try
            {
                var ht = new Hashtable();
                ht.Add("Test_String", "");

                var config = new TestPluginConfig();
                var attrib = new ConfigurationEntryAttribute { DefaultValue = 2 };

                DictionaryConfigurationParser.ResolveConfigurationEntry(config, "Test_String", typeof(TestPluginConfig).GetProperty("String"), attrib, ht);
            }
            catch (ConfigurationErrorsException ex)
            {
                Assert.AreEqual("Value cannot be blank/empty", ex.Message);
                return;
            }

            Assert.Fail("ConfigurationErrorsException must be thrown");
        }

        [TestMethod]
        public void ShouldPassStringOptionalConfig()
        {
            var ht = new Hashtable();
            ht.Add("Test_String", "cow");

            var config = new TestPluginConfig();
            var attrib = new ConfigurationEntryAttribute { IsRequired = false };

            DictionaryConfigurationParser.ResolveConfigurationEntry(config, "Test_String", typeof(TestPluginConfig).GetProperty("String"), attrib, ht);

            Assert.AreEqual("cow", config.String);
        }

        [TestMethod]
        public void ShouldPassStringOptionalWithDefaultValue()
        {
            var ht = new Hashtable();
            var config = new TestPluginConfig();
            var attrib = new ConfigurationEntryAttribute { IsRequired = false, DefaultValue = "earthworm" };

            DictionaryConfigurationParser.ResolveConfigurationEntry(config, "Test_String", typeof(TestPluginConfig).GetProperty("String"), attrib, ht);

            Assert.AreEqual("earthworm", config.String);
        }

        [TestMethod]
        public void ShouldPassDecimalRequiredConfig()
        {
            var ht = new Hashtable();
            ht.Add("Test_Decimal", "1");

            var config = new TestPluginConfig();
            var attrib = new ConfigurationEntryAttribute();

            DictionaryConfigurationParser.ResolveConfigurationEntry(config, "Test_Decimal", typeof(TestPluginConfig).GetProperty("Decimal"), attrib, ht);

            Assert.AreEqual(1, config.Decimal);
        }

        [TestMethod]
        public void ShouldFailDecimalRequiredConfig()
        {
            try
            {
                var ht = new Hashtable();
                ht.Add("Test_Decimal", "x");

                var config = new TestPluginConfig();
                var attrib = new ConfigurationEntryAttribute();

                DictionaryConfigurationParser.ResolveConfigurationEntry(config, "Test_Decimal", typeof(TestPluginConfig).GetProperty("Decimal"), attrib, ht);
            }
            catch (ConfigurationErrorsException ex)
            {
                Assert.AreEqual("Value must be numeric/number (Decimal type)", ex.Message);
                return;
            }

            Assert.Fail("ConfigurationErrorsException must be thrown");
        }

        [TestMethod]
        public void ShouldPassDecimalRequiredConfigWithDefaultValue()
        {
            var ht = new Hashtable();
            ht.Add("Test_Decimal", "1");

            var config = new TestPluginConfig();
            var attrib = new ConfigurationEntryAttribute { DefaultValue = 2 };

            DictionaryConfigurationParser.ResolveConfigurationEntry(config, "Test_Decimal", typeof(TestPluginConfig).GetProperty("Decimal"), attrib, ht);

            Assert.AreEqual(1, config.Decimal);
        }

        [TestMethod]
        public void ShouldFailDecimalRequiredConfigWithDefaultValue()
        {
            try
            {
                var ht = new Hashtable();
                ht.Add("Test_Decimal", "x");

                var config = new TestPluginConfig();
                var attrib = new ConfigurationEntryAttribute { DefaultValue = 2 };

                DictionaryConfigurationParser.ResolveConfigurationEntry(config, "Test_Decimal", typeof(TestPluginConfig).GetProperty("Decimal"), attrib, ht);
            }
            catch (ConfigurationErrorsException ex)
            {
                Assert.AreEqual("Value must be numeric/number (Decimal type)", ex.Message);
                return;
            }

            Assert.Fail("ConfigurationErrorsException must be thrown");
        }

        [TestMethod]
        public void ShouldPassDecimalOptional()
        {
            var ht = new Hashtable();
            ht.Add("Test_Decimal", "1");

            var config = new TestPluginConfig();
            var attrib = new ConfigurationEntryAttribute { IsRequired = false };

            DictionaryConfigurationParser.ResolveConfigurationEntry(config, "Test_Decimal", typeof(TestPluginConfig).GetProperty("Decimal"), attrib, ht);

            Assert.AreEqual(1, config.Decimal);
        }

        [TestMethod]
        public void ShouldFailDecimalOptionalConfig()
        {
            try
            {
                var ht = new Hashtable();
                ht.Add("Test_Decimal", "x");

                var config = new TestPluginConfig();
                var attrib = new ConfigurationEntryAttribute { IsRequired = false, DefaultValue = 2 };

                DictionaryConfigurationParser.ResolveConfigurationEntry(config, "Test_Decimal", typeof(TestPluginConfig).GetProperty("Decimal"), attrib, ht);
            }
            catch (ConfigurationErrorsException ex)
            {
                Assert.AreEqual("Value must be numeric/number (Decimal type)", ex.Message);
                return;
            }

            Assert.Fail("ConfigurationErrorsException must be thrown");
        }

        [TestMethod]
        public void ShouldPassDecimalOptionalWithDefaultValue()
        {
            var ht = new Hashtable();
            var config = new TestPluginConfig();
            var attrib = new ConfigurationEntryAttribute { IsRequired = false, DefaultValue = 2 };

            DictionaryConfigurationParser.ResolveConfigurationEntry(config, "Test_Decimal", typeof(TestPluginConfig).GetProperty("Decimal"), attrib, ht);

            Assert.AreEqual(2, config.Decimal);
        }

        [TestMethod]
        public void ShouldFailDecimalOptionalConfigWithDefaultValue()
        {
            try
            {
                var ht = new Hashtable();
                ht.Add("Test_Decimal", "x");

                var config = new TestPluginConfig();
                var attrib = new ConfigurationEntryAttribute { IsRequired = false, DefaultValue = 2 };

                DictionaryConfigurationParser.ResolveConfigurationEntry(config, "Test_Decimal", typeof(TestPluginConfig).GetProperty("Decimal"), attrib, ht);
            }
            catch (ConfigurationErrorsException ex)
            {
                Assert.AreEqual("Value must be numeric/number (Decimal type)", ex.Message);
                return;
            }

            Assert.Fail("ConfigurationErrorsException must be thrown");
        }

        [TestMethod]
        public void ShouldPassDoubleRequiredConfig()
        {
            var ht = new Hashtable();
            ht.Add("Test_Double", "1");

            var config = new TestPluginConfig();
            var attrib = new ConfigurationEntryAttribute();

            DictionaryConfigurationParser.ResolveConfigurationEntry(config, "Test_Double", typeof(TestPluginConfig).GetProperty("Double"), attrib, ht);

            Assert.AreEqual(1, config.Double);
        }

        [TestMethod]
        public void ShouldFailDoubleRequiredConfig()
        {
            try
            {
                var ht = new Hashtable();
                ht.Add("Test_Double", "x");

                var config = new TestPluginConfig();
                var attrib = new ConfigurationEntryAttribute();

                DictionaryConfigurationParser.ResolveConfigurationEntry(config, "Test_Double", typeof(TestPluginConfig).GetProperty("Double"), attrib, ht);
            }
            catch (ConfigurationErrorsException ex)
            {
                Assert.AreEqual("Value must be numeric/number (Double type)", ex.Message);
                return;
            }

            Assert.Fail("ConfigurationErrorsException must be thrown");
        }

        [TestMethod]
        public void ShouldPassDoubleRequiredConfigWithDefaultValue()
        {
            var ht = new Hashtable();
            ht.Add("Test_Double", "1");

            var config = new TestPluginConfig();
            var attrib = new ConfigurationEntryAttribute { DefaultValue = 2 };

            DictionaryConfigurationParser.ResolveConfigurationEntry(config, "Test_Double", typeof(TestPluginConfig).GetProperty("Double"), attrib, ht);

            Assert.AreEqual(1, config.Double);
        }

        [TestMethod]
        public void ShouldFailDoubleRequiredConfigWithDefaultValue()
        {
            try
            {
                var ht = new Hashtable();
                ht.Add("Test_Double", "x");

                var config = new TestPluginConfig();
                var attrib = new ConfigurationEntryAttribute { DefaultValue = 2 };

                DictionaryConfigurationParser.ResolveConfigurationEntry(config, "Test_Double", typeof(TestPluginConfig).GetProperty("Double"), attrib, ht);
            }
            catch (ConfigurationErrorsException ex)
            {
                Assert.AreEqual("Value must be numeric/number (Double type)", ex.Message);
                return;
            }

            Assert.Fail("ConfigurationErrorsException must be thrown");
        }

        [TestMethod]
        public void ShouldPassDoubleOptionalConfig()
        {
            var ht = new Hashtable();
            ht.Add("Test_Double", "1");

            var config = new TestPluginConfig();
            var attrib = new ConfigurationEntryAttribute { IsRequired = false };

            DictionaryConfigurationParser.ResolveConfigurationEntry(config, "Test_Double", typeof(TestPluginConfig).GetProperty("Double"), attrib, ht);

            Assert.AreEqual(1, config.Double);
        }

        [TestMethod]
        public void ShouldFailDoubleOptionalConfig()
        {
            try
            {
                var ht = new Hashtable();
                ht.Add("Test_Double", "x");

                var config = new TestPluginConfig();
                var attrib = new ConfigurationEntryAttribute { IsRequired = false, DefaultValue = 2 };

                DictionaryConfigurationParser.ResolveConfigurationEntry(config, "Test_Double", typeof(TestPluginConfig).GetProperty("Double"), attrib, ht);
            }
            catch (ConfigurationErrorsException ex)
            {
                Assert.AreEqual("Value must be numeric/number (Double type)", ex.Message);
                return;
            }

            Assert.Fail("ConfigurationErrorsException must be thrown");
        }

        [TestMethod]
        public void ShouldPassDoubleOptionalWithDefaultValue()
        {
            var ht = new Hashtable();
            var config = new TestPluginConfig();
            var attrib = new ConfigurationEntryAttribute { IsRequired = false, DefaultValue = 2 };

            DictionaryConfigurationParser.ResolveConfigurationEntry(config, "Test_Double", typeof(TestPluginConfig).GetProperty("Double"), attrib, ht);

            Assert.AreEqual(2, config.Double);
        }

        [TestMethod]
        public void ShouldFailDoubleOptionalConfigWithDefaultValue()
        {
            try
            {
                var ht = new Hashtable();
                ht.Add("Test_Double", "x");

                var config = new TestPluginConfig();
                var attrib = new ConfigurationEntryAttribute { IsRequired = false, DefaultValue = 2 };

                DictionaryConfigurationParser.ResolveConfigurationEntry(config, "Test_Double", typeof(TestPluginConfig).GetProperty("Double"), attrib, ht);
            }
            catch (ConfigurationErrorsException ex)
            {
                Assert.AreEqual("Value must be numeric/number (Double type)", ex.Message);
                return;
            }

            Assert.Fail("ConfigurationErrorsException must be thrown");
        }

        [TestMethod]
        public void ShouldPassSingleRequiredConfig()
        {
            var ht = new Hashtable();
            ht.Add("Test_Single", "1");

            var config = new TestPluginConfig();
            var attrib = new ConfigurationEntryAttribute();

            DictionaryConfigurationParser.ResolveConfigurationEntry(config, "Test_Single", typeof(TestPluginConfig).GetProperty("Single"), attrib, ht);

            Assert.AreEqual(1, config.Single);
        }

        [TestMethod]
        public void ShouldFailSingleRequiredConfig()
        {
            try
            {
                var ht = new Hashtable();
                ht.Add("Test_Single", "x");

                var config = new TestPluginConfig();
                var attrib = new ConfigurationEntryAttribute();

                DictionaryConfigurationParser.ResolveConfigurationEntry(config, "Test_Single", typeof(TestPluginConfig).GetProperty("Single"), attrib, ht);
            }
            catch (ConfigurationErrorsException ex)
            {
                Assert.AreEqual("Value must be numeric/number (Single type)", ex.Message);
                return;
            }

            Assert.Fail("ConfigurationErrorsException must be thrown");
        }

        [TestMethod]
        public void ShouldPassSingleRequiredConfigWithDefaultValue()
        {
            var ht = new Hashtable();
            ht.Add("Test_Single", "1");

            var config = new TestPluginConfig();
            var attrib = new ConfigurationEntryAttribute { DefaultValue = 2 };

            DictionaryConfigurationParser.ResolveConfigurationEntry(config, "Test_Single", typeof(TestPluginConfig).GetProperty("Single"), attrib, ht);

            Assert.AreEqual(1, config.Single);
        }

        [TestMethod]
        public void ShouldFailSingleRequiredConfigWithDefaultValue()
        {
            try
            {
                var ht = new Hashtable();
                ht.Add("Test_Single", "x");

                var config = new TestPluginConfig();
                var attrib = new ConfigurationEntryAttribute { DefaultValue = 2 };

                DictionaryConfigurationParser.ResolveConfigurationEntry(config, "Test_Single", typeof(TestPluginConfig).GetProperty("Single"), attrib, ht);
            }
            catch (ConfigurationErrorsException ex)
            {
                Assert.AreEqual("Value must be numeric/number (Single type)", ex.Message);
                return;
            }

            Assert.Fail("ConfigurationErrorsException must be thrown");
        }

        [TestMethod]
        public void ShouldPassSingleOptionalConfig()
        {
            var ht = new Hashtable();
            ht.Add("Test_Single", "1");

            var config = new TestPluginConfig();
            var attrib = new ConfigurationEntryAttribute { IsRequired = false };

            DictionaryConfigurationParser.ResolveConfigurationEntry(config, "Test_Single", typeof(TestPluginConfig).GetProperty("Single"), attrib, ht);

            Assert.AreEqual(1, config.Single);
        }

        [TestMethod]
        public void ShouldFailSingleOptionalConfig()
        {
            try
            {
                var ht = new Hashtable();
                ht.Add("Test_Single", "x");

                var config = new TestPluginConfig();
                var attrib = new ConfigurationEntryAttribute { IsRequired = false, DefaultValue = 2 };

                DictionaryConfigurationParser.ResolveConfigurationEntry(config, "Test_Single", typeof(TestPluginConfig).GetProperty("Single"), attrib, ht);
            }
            catch (ConfigurationErrorsException ex)
            {
                Assert.AreEqual("Value must be numeric/number (Single type)", ex.Message);
                return;
            }

            Assert.Fail("ConfigurationErrorsException must be thrown");
        }

        [TestMethod]
        public void ShouldPassSingleOptionalWithDefaultValue()
        {
            var ht = new Hashtable();
            var config = new TestPluginConfig();
            var attrib = new ConfigurationEntryAttribute { IsRequired = false, DefaultValue = 2 };

            DictionaryConfigurationParser.ResolveConfigurationEntry(config, "Test_Single", typeof(TestPluginConfig).GetProperty("Single"), attrib, ht);

            Assert.AreEqual(2, config.Single);
        }

        [TestMethod]
        public void ShouldFailSingleOptionalConfigWithDefaultValue()
        {
            try
            {
                var ht = new Hashtable();
                ht.Add("Test_Single", "x");

                var config = new TestPluginConfig();
                var attrib = new ConfigurationEntryAttribute { IsRequired = false, DefaultValue = 2 };

                DictionaryConfigurationParser.ResolveConfigurationEntry(config, "Test_Single", typeof(TestPluginConfig).GetProperty("Single"), attrib, ht);
            }
            catch (ConfigurationErrorsException ex)
            {
                Assert.AreEqual("Value must be numeric/number (Single type)", ex.Message);
                return;
            }

            Assert.Fail("ConfigurationErrorsException must be thrown");
        }

        [TestMethod]
        public void ShouldPassGenericListRequiredConfig()
        {
            var ht = new Hashtable();
            ht.Add("Test_StringList", "1,2,3");

            var config = new TestPluginConfig();
            var attrib = new ConfigurationEntryAttribute { Delimiter = ',' };

            DictionaryConfigurationParser.ResolveConfigurationEntry(config, "Test_StringList", typeof(TestPluginConfig).GetProperty("StringList"), attrib, ht);

            Assert.AreEqual(3, config.StringList.Count);
        }

        [TestMethod]
        public void ShouldPassGenericListRequiredConfigWithTrailingDelimiter()
        {
            var ht = new Hashtable();
            ht.Add("Test_StringList", "1,2,");

            var config = new TestPluginConfig();
            var attrib = new ConfigurationEntryAttribute { Delimiter = ',' };

            DictionaryConfigurationParser.ResolveConfigurationEntry(config, "Test_StringList", typeof(TestPluginConfig).GetProperty("StringList"), attrib, ht);

            Assert.AreEqual(2, config.StringList.Count);
        }

        [TestMethod]
        public void ShouldFailGenericListRequiredConfig()
        {
            try
            {
                var ht = new Hashtable();
                ht.Add("Test_StringList", "");

                var config = new TestPluginConfig();
                var attrib = new ConfigurationEntryAttribute { Delimiter = ',' };

                DictionaryConfigurationParser.ResolveConfigurationEntry(config, "Test_StringList", typeof(TestPluginConfig).GetProperty("StringList"), attrib, ht);
            }
            catch (ConfigurationErrorsException ex)
            {
                Assert.AreEqual("Value cannot be blank/empty", ex.Message);
                return;
            }

            Assert.Fail("ConfigurationErrorsException must be thrown");
        }

        [TestMethod]
        public void ShouldPassGenericListOptionalConfigExists()
        {
            var ht = new Hashtable();
            ht.Add("Test_StringList", "1,2,3");

            var config = new TestPluginConfig();
            var attrib = new ConfigurationEntryAttribute { Delimiter = ',', IsRequired = false };

            DictionaryConfigurationParser.ResolveConfigurationEntry(config, "Test_StringList", typeof(TestPluginConfig).GetProperty("StringList"), attrib, ht);

            Assert.AreEqual(3, config.StringList.Count);
        }

        [TestMethod]
        public void ShouldPassGenericListOptionalConfig()
        {
            var ht = new Hashtable();

            var config = new TestPluginConfig();
            var attrib = new ConfigurationEntryAttribute { Delimiter = ',', IsRequired = false, DefaultValue = "1,2,3" };

            DictionaryConfigurationParser.ResolveConfigurationEntry(config, "Test_StringList", typeof(TestPluginConfig).GetProperty("StringList"), attrib, ht);

            Assert.AreEqual(3, config.StringList.Count);
        }

        [TestMethod]
        public void ShouldPassArrayRequiredConfig()
        {
            var ht = new Hashtable();
            ht.Add("Test_IntegerArray", "1,2,3");

            var config = new TestPluginConfig();
            var attrib = new ConfigurationEntryAttribute { Delimiter = ',' };

            DictionaryConfigurationParser.ResolveConfigurationEntry(config, "Test_IntegerArray", typeof(TestPluginConfig).GetProperty("IntegerArray"), attrib, ht);

            Assert.AreEqual(3, config.IntegerArray.Length);
        }

        [TestMethod]
        public void ShouldPassArrayRequiredConfigWithTrailingDelimiter()
        {
            var ht = new Hashtable();
            ht.Add("Test_IntegerArray", "1,,2,");

            var config = new TestPluginConfig();
            var attrib = new ConfigurationEntryAttribute { Delimiter = ',' };

            DictionaryConfigurationParser.ResolveConfigurationEntry(config, "Test_IntegerArray", typeof(TestPluginConfig).GetProperty("IntegerArray"), attrib, ht);

            Assert.AreEqual(2, config.IntegerArray.Length);
        }

        [TestMethod]
        public void ShouldFailArrayRequiredConfig()
        {
            try
            {
                var ht = new Hashtable();
                ht.Add("Test_IntegerArray", "");

                var config = new TestPluginConfig();
                var attrib = new ConfigurationEntryAttribute { Delimiter = ',' };

                DictionaryConfigurationParser.ResolveConfigurationEntry(config, "Test_IntegerArray", typeof(TestPluginConfig).GetProperty("IntegerArray"), attrib, ht);
            }
            catch (ConfigurationErrorsException ex)
            {
                Assert.AreEqual("Value cannot be blank/empty", ex.Message);
                return;
            }

            Assert.Fail("ConfigurationErrorsException must be thrown");
        }

        [TestMethod]
        public void ShouldFailArrayIntegerRequiredConfig()
        {
            try
            {
                var ht = new Hashtable();
                ht.Add("Test_IntegerArray", "1,a");

                var config = new TestPluginConfig();
                var attrib = new ConfigurationEntryAttribute { Delimiter = ',' };

                DictionaryConfigurationParser.ResolveConfigurationEntry(config, "Test_IntegerArray", typeof(TestPluginConfig).GetProperty("IntegerArray"), attrib, ht);
            }
            catch (ConfigurationErrorsException ex)
            {
                Assert.AreEqual("Value must be numeric/number (Int32 type)", ex.Message);
                return;
            }

            Assert.Fail("ConfigurationErrorsException must be thrown");
        }

        [TestMethod]
        public void ShouldPassEnumerationRequiredConfig()
        {
            var ht = new Hashtable();
            ht.Add("Test_Enumeration", "Outbound");

            var config = new TestPluginConfig();
            var attrib = new ConfigurationEntryAttribute();

            DictionaryConfigurationParser.ResolveConfigurationEntry(config, "Test_Enumeration", typeof(TestPluginConfig).GetProperty("Enumeration"), attrib, ht);

            Assert.AreEqual(Direction.Outbound, config.Enumeration);
        }

        [TestMethod]
        public void ShouldPassEnumerationRequiredConfig_CaseInsensitive()
        {
            var ht = new Hashtable();
            ht.Add("Test_Enumeration", "outbound");

            var config = new TestPluginConfig();
            var attrib = new ConfigurationEntryAttribute();

            DictionaryConfigurationParser.ResolveConfigurationEntry(config, "Test_Enumeration", typeof(TestPluginConfig).GetProperty("Enumeration"), attrib, ht);

            Assert.AreEqual(Direction.Outbound, config.Enumeration);
        }

        [TestMethod]
        public void ShouldFailEnumerationRequiredConfig()
        {
            try
            {
                var ht = new Hashtable();
                ht.Add("Test_Enumeration", "");

                var config = new TestPluginConfig();
                var attrib = new ConfigurationEntryAttribute();

                DictionaryConfigurationParser.ResolveConfigurationEntry(config, "Test_Enumeration", typeof(TestPluginConfig).GetProperty("Enumeration"), attrib, ht);
            }
            catch (ConfigurationErrorsException ex)
            {
                Assert.AreEqual(" is not defined in Common.Utility.Configuration.Tests.Direction enumeration. Valid values: Outbound,Inbound)", ex.Message);
                return;
            }

            Assert.Fail("ConfigurationErrorsException must be thrown");
        }

        [TestMethod]
        public void ShouldPassEnumerationOptionalConfig()
        {
            var ht = new Hashtable();
            ht.Add("Test_Enumeration", "outbound");

            var config = new TestPluginConfig();
            var attrib = new ConfigurationEntryAttribute { IsRequired = false };

            DictionaryConfigurationParser.ResolveConfigurationEntry(config, "Test_Enumeration", typeof(TestPluginConfig).GetProperty("Enumeration"), attrib, ht);

            Assert.AreEqual(Direction.Outbound, config.Enumeration);
        }

        [TestMethod]
        public void ShouldPassEnumerationOptionalConfigWithDefaultValue()
        {
            var ht = new Hashtable();
            ht.Add("Test_Enumeration", "outbound");

            var config = new TestPluginConfig();
            var attrib = new ConfigurationEntryAttribute { IsRequired = false, DefaultValue = Direction.Inbound };

            DictionaryConfigurationParser.ResolveConfigurationEntry(config, "Test_Enumeration", typeof(TestPluginConfig).GetProperty("Enumeration"), attrib, ht);

            Assert.AreEqual(Direction.Outbound, config.Enumeration);
        }

        [TestMethod]
        public void ShouldPassEnumerationOptionalWithDefaultValue()
        {
            var ht = new Hashtable();

            var config = new TestPluginConfig();
            var attrib = new ConfigurationEntryAttribute { IsRequired = false, DefaultValue = Direction.Inbound };

            DictionaryConfigurationParser.ResolveConfigurationEntry(config, "Test_Enumeration", typeof(TestPluginConfig).GetProperty("Enumeration"), attrib, ht);

            Assert.AreEqual(Direction.Inbound, config.Enumeration);
        }

        [TestMethod]
        public void ShouldPassUInt32RequiredConfig()
        {
            var ht = new Hashtable();
            ht.Add("Test_UInt32", "1");

            var config = new TestPluginConfig();
            var attrib = new ConfigurationEntryAttribute();

            DictionaryConfigurationParser.ResolveConfigurationEntry(config, "Test_UInt32", typeof(TestPluginConfig).GetProperty("UInt32"), attrib, ht);

            Assert.AreEqual(1u, config.UInt32);
        }

        [TestMethod]
        public void ShouldFailUInt32RequiredConfig()
        {
            try
            {
                var ht = new Hashtable();
                ht.Add("Test_UInt32", "x");

                var config = new TestPluginConfig();
                var attrib = new ConfigurationEntryAttribute();

                DictionaryConfigurationParser.ResolveConfigurationEntry(config, "Test_UInt32", typeof(TestPluginConfig).GetProperty("UInt32"), attrib, ht);
            }
            catch (ConfigurationErrorsException ex)
            {
                Assert.AreEqual("Value must be non-negative numeric/number (UInt32 type)", ex.Message);
                return;
            }

            Assert.Fail("ConfigurationErrorsException must be thrown");
        }

        [TestMethod]
        public void ShouldPassUInt32RequiredConfigWithDefaultValue()
        {
            var ht = new Hashtable();
            ht.Add("Test_UInt32", "1");

            var config = new TestPluginConfig();
            var attrib = new ConfigurationEntryAttribute { DefaultValue = 2 };

            DictionaryConfigurationParser.ResolveConfigurationEntry(config, "Test_UInt32", typeof(TestPluginConfig).GetProperty("UInt32"), attrib, ht);

            Assert.AreEqual(1u, config.UInt32);
        }

        [TestMethod]
        public void ShouldFailUInt32RequiredConfigWithDefaultValue()
        {
            try
            {
                var ht = new Hashtable();
                ht.Add("Test_UInt32", "x");

                var config = new TestPluginConfig();
                var attrib = new ConfigurationEntryAttribute { DefaultValue = 2 };

                DictionaryConfigurationParser.ResolveConfigurationEntry(config, "Test_UInt32", typeof(TestPluginConfig).GetProperty("UInt32"), attrib, ht);
            }
            catch (ConfigurationErrorsException ex)
            {
                Assert.AreEqual("Value must be non-negative numeric/number (UInt32 type)", ex.Message);
                return;
            }

            Assert.Fail("ConfigurationErrorsException must be thrown");
        }

        [TestMethod]
        public void ShouldPassUInt32Optional()
        {
            var ht = new Hashtable();
            ht.Add("Test_UInt32", "1");

            var config = new TestPluginConfig();
            var attrib = new ConfigurationEntryAttribute { IsRequired = false };

            DictionaryConfigurationParser.ResolveConfigurationEntry(config, "Test_UInt32", typeof(TestPluginConfig).GetProperty("UInt32"), attrib, ht);

            Assert.AreEqual(1u, config.UInt32);
        }

        [TestMethod]
        public void ShouldFailUInt32OptionalConfig()
        {
            try
            {
                var ht = new Hashtable();
                ht.Add("Test_UInt32", "x");

                var config = new TestPluginConfig();
                var attrib = new ConfigurationEntryAttribute { IsRequired = false, DefaultValue = 2 };

                DictionaryConfigurationParser.ResolveConfigurationEntry(config, "Test_UInt32", typeof(TestPluginConfig).GetProperty("UInt32"), attrib, ht);
            }
            catch (ConfigurationErrorsException ex)
            {
                Assert.AreEqual("Value must be non-negative numeric/number (UInt32 type)", ex.Message);
                return;
            }

            Assert.Fail("ConfigurationErrorsException must be thrown");
        }

        [TestMethod]
        public void ShouldPassUInt32OptionalWithDefaultValue()
        {
            var ht = new Hashtable();
            var config = new TestPluginConfig();
            var attrib = new ConfigurationEntryAttribute { IsRequired = false, DefaultValue = 2 };

            DictionaryConfigurationParser.ResolveConfigurationEntry(config, "Test_UInt32", typeof(TestPluginConfig).GetProperty("UInt32"), attrib, ht);

            Assert.AreEqual(2u, config.UInt32);
        }

        [TestMethod]
        public void ShouldFailUInt32OptionalConfigWithDefaultValue()
        {
            try
            {
                var ht = new Hashtable();
                ht.Add("Test_UInt32", "x");

                var config = new TestPluginConfig();
                var attrib = new ConfigurationEntryAttribute { IsRequired = false, DefaultValue = 2 };

                DictionaryConfigurationParser.ResolveConfigurationEntry(config, "Test_UInt32", typeof(TestPluginConfig).GetProperty("UInt32"), attrib, ht);
            }
            catch (ConfigurationErrorsException ex)
            {
                Assert.AreEqual("Value must be non-negative numeric/number (UInt32 type)", ex.Message);
                return;
            }

            Assert.Fail("ConfigurationErrorsException must be thrown");
        }

        [TestMethod]
        public void ShouldPassUInt64RequiredConfig()
        {
            var ht = new Hashtable();
            ht.Add("Test_UInt64", "1");

            var config = new TestPluginConfig();
            var attrib = new ConfigurationEntryAttribute();

            DictionaryConfigurationParser.ResolveConfigurationEntry(config, "Test_UInt64", typeof(TestPluginConfig).GetProperty("UInt64"), attrib, ht);

            Assert.AreEqual(1ul, config.UInt64);
        }

        [TestMethod]
        public void ShouldFailUInt64RequiredConfig()
        {
            try
            {
                var ht = new Hashtable();
                ht.Add("Test_UInt64", "x");

                var config = new TestPluginConfig();
                var attrib = new ConfigurationEntryAttribute();

                DictionaryConfigurationParser.ResolveConfigurationEntry(config, "Test_UInt64", typeof(TestPluginConfig).GetProperty("UInt64"), attrib, ht);
            }
            catch (ConfigurationErrorsException ex)
            {
                Assert.AreEqual("Value must be non-negative numeric/number (UInt64 type)", ex.Message);
                return;
            }

            Assert.Fail("ConfigurationErrorsException must be thrown");
        }

        [TestMethod]
        public void ShouldPassUInt64RequiredConfigWithDefaultValue()
        {
            var ht = new Hashtable();
            ht.Add("Test_UInt64", "1");

            var config = new TestPluginConfig();
            var attrib = new ConfigurationEntryAttribute { DefaultValue = 2 };

            DictionaryConfigurationParser.ResolveConfigurationEntry(config, "Test_UInt64", typeof(TestPluginConfig).GetProperty("UInt64"), attrib, ht);

            Assert.AreEqual(1ul, config.UInt64);
        }

        [TestMethod]
        public void ShouldFailUInt64RequiredConfigWithDefaultValue()
        {
            try
            {
                var ht = new Hashtable();
                ht.Add("Test_UInt64", "x");

                var config = new TestPluginConfig();
                var attrib = new ConfigurationEntryAttribute { DefaultValue = 2 };

                DictionaryConfigurationParser.ResolveConfigurationEntry(config, "Test_UInt64", typeof(TestPluginConfig).GetProperty("UInt64"), attrib, ht);
            }
            catch (ConfigurationErrorsException ex)
            {
                Assert.AreEqual("Value must be non-negative numeric/number (UInt64 type)", ex.Message);
                return;
            }

            Assert.Fail("ConfigurationErrorsException must be thrown");
        }

        [TestMethod]
        public void ShouldPassUInt64Optional()
        {
            var ht = new Hashtable();
            ht.Add("Test_UInt64", "1");

            var config = new TestPluginConfig();
            var attrib = new ConfigurationEntryAttribute { IsRequired = false };

            DictionaryConfigurationParser.ResolveConfigurationEntry(config, "Test_UInt64", typeof(TestPluginConfig).GetProperty("UInt64"), attrib, ht);

            Assert.AreEqual(1ul, config.UInt64);
        }

        [TestMethod]
        public void ShouldFailUInt64OptionalConfig()
        {
            try
            {
                var ht = new Hashtable();
                ht.Add("Test_UInt64", "x");

                var config = new TestPluginConfig();
                var attrib = new ConfigurationEntryAttribute { IsRequired = false, DefaultValue = 2 };

                DictionaryConfigurationParser.ResolveConfigurationEntry(config, "Test_UInt64", typeof(TestPluginConfig).GetProperty("UInt64"), attrib, ht);
            }
            catch (ConfigurationErrorsException ex)
            {
                Assert.AreEqual("Value must be non-negative numeric/number (UInt64 type)", ex.Message);
                return;
            }

            Assert.Fail("ConfigurationErrorsException must be thrown");
        }

        [TestMethod]
        public void ShouldPassUInt64OptionalWithDefaultValue()
        {
            var ht = new Hashtable();
            var config = new TestPluginConfig();
            var attrib = new ConfigurationEntryAttribute { IsRequired = false, DefaultValue = 2 };

            DictionaryConfigurationParser.ResolveConfigurationEntry(config, "Test_UInt64", typeof(TestPluginConfig).GetProperty("UInt64"), attrib, ht);

            Assert.AreEqual(2ul, config.UInt64);
        }

        [TestMethod]
        public void ShouldFailUInt64OptionalConfigWithDefaultValue()
        {
            try
            {
                var ht = new Hashtable();
                ht.Add("Test_UInt64", "x");

                var config = new TestPluginConfig();
                var attrib = new ConfigurationEntryAttribute { IsRequired = false, DefaultValue = 2 };

                DictionaryConfigurationParser.ResolveConfigurationEntry(config, "Test_UInt64", typeof(TestPluginConfig).GetProperty("UInt64"), attrib, ht);
            }
            catch (ConfigurationErrorsException ex)
            {
                Assert.AreEqual("Value must be non-negative numeric/number (UInt64 type)", ex.Message);
                return;
            }

            Assert.Fail("ConfigurationErrorsException must be thrown");
        }

        [TestMethod]
        public void ShouldPassUInt16RequiredConfig()
        {
            var ht = new Hashtable();
            ht.Add("Test_UInt16", "1");

            var config = new TestPluginConfig();
            var attrib = new ConfigurationEntryAttribute();

            DictionaryConfigurationParser.ResolveConfigurationEntry(config, "Test_UInt16", typeof(TestPluginConfig).GetProperty("UInt16"), attrib, ht);

            Assert.AreEqual((ushort)1, config.UInt16);
        }

        [TestMethod]
        public void ShouldFailUInt16RequiredConfig()
        {
            try
            {
                var ht = new Hashtable();
                ht.Add("Test_UInt16", "x");

                var config = new TestPluginConfig();
                var attrib = new ConfigurationEntryAttribute();

                DictionaryConfigurationParser.ResolveConfigurationEntry(config, "Test_UInt16", typeof(TestPluginConfig).GetProperty("UInt16"), attrib, ht);
            }
            catch (ConfigurationErrorsException ex)
            {
                Assert.AreEqual("Value must be non-negative numeric/number (UInt16 type)", ex.Message);
                return;
            }

            Assert.Fail("ConfigurationErrorsException must be thrown");
        }

        [TestMethod]
        public void ShouldPassUInt16RequiredConfigWithDefaultValue()
        {
            var ht = new Hashtable();
            ht.Add("Test_UInt16", "1");

            var config = new TestPluginConfig();
            var attrib = new ConfigurationEntryAttribute { DefaultValue = 2 };

            DictionaryConfigurationParser.ResolveConfigurationEntry(config, "Test_UInt16", typeof(TestPluginConfig).GetProperty("UInt16"), attrib, ht);

            Assert.AreEqual((ushort)1, config.UInt16);
        }

        [TestMethod]
        public void ShouldFailUInt16RequiredConfigWithDefaultValue()
        {
            try
            {
                var ht = new Hashtable();
                ht.Add("Test_UInt16", "x");

                var config = new TestPluginConfig();
                var attrib = new ConfigurationEntryAttribute { DefaultValue = 2 };

                DictionaryConfigurationParser.ResolveConfigurationEntry(config, "Test_UInt16", typeof(TestPluginConfig).GetProperty("UInt16"), attrib, ht);
            }
            catch (ConfigurationErrorsException ex)
            {
                Assert.AreEqual("Value must be non-negative numeric/number (UInt16 type)", ex.Message);
                return;
            }

            Assert.Fail("ConfigurationErrorsException must be thrown");
        }

        [TestMethod]
        public void ShouldPassUInt16Optional()
        {
            var ht = new Hashtable();
            ht.Add("Test_UInt16", "1");

            var config = new TestPluginConfig();
            var attrib = new ConfigurationEntryAttribute { IsRequired = false };

            DictionaryConfigurationParser.ResolveConfigurationEntry(config, "Test_UInt16", typeof(TestPluginConfig).GetProperty("UInt16"), attrib, ht);

            Assert.AreEqual((ushort)1, config.UInt16);
        }

        [TestMethod]
        public void ShouldFailUInt16OptionalConfig()
        {
            try
            {
                var ht = new Hashtable();
                ht.Add("Test_UInt16", "x");

                var config = new TestPluginConfig();
                var attrib = new ConfigurationEntryAttribute { IsRequired = false, DefaultValue = 2 };

                DictionaryConfigurationParser.ResolveConfigurationEntry(config, "Test_UInt16", typeof(TestPluginConfig).GetProperty("UInt16"), attrib, ht);
            }
            catch (ConfigurationErrorsException ex)
            {
                Assert.AreEqual("Value must be non-negative numeric/number (UInt16 type)", ex.Message);
                return;
            }

            Assert.Fail("ConfigurationErrorsException must be thrown");
        }

        [TestMethod]
        public void ShouldPassUInt16OptionalWithDefaultValue()
        {
            var ht = new Hashtable();
            var config = new TestPluginConfig();
            var attrib = new ConfigurationEntryAttribute { IsRequired = false, DefaultValue = 2 };

            DictionaryConfigurationParser.ResolveConfigurationEntry(config, "Test_UInt16", typeof(TestPluginConfig).GetProperty("UInt16"), attrib, ht);

            Assert.AreEqual((ushort)2, config.UInt16);
        }

        [TestMethod]
        public void ShouldFailUInt16OptionalConfigWithDefaultValue()
        {
            try
            {
                var ht = new Hashtable();
                ht.Add("Test_UInt16", "x");

                var config = new TestPluginConfig();
                var attrib = new ConfigurationEntryAttribute { IsRequired = false, DefaultValue = 2 };

                DictionaryConfigurationParser.ResolveConfigurationEntry(config, "Test_UInt16", typeof(TestPluginConfig).GetProperty("UInt16"), attrib, ht);
            }
            catch (ConfigurationErrorsException ex)
            {
                Assert.AreEqual("Value must be non-negative numeric/number (UInt16 type)", ex.Message);
                return;
            }

            Assert.Fail("ConfigurationErrorsException must be thrown");
        }
    }

    public class TestPluginConfig
    {
        [ConfigurationEntry]
        public string String { get; private set; }

        [ConfigurationEntry]
        public short Int16 { get; private set; }

        [ConfigurationEntry]
        public int Int32 { get; private set; }

        [ConfigurationEntry]
        public long Int64 { get; private set; }

        [ConfigurationEntry]
        public decimal Decimal { get; private set; }

        [ConfigurationEntry]
        public double Double { get; private set; }

        [ConfigurationEntry]
        public float Single { get; private set; }

        [ConfigurationEntry]
        public bool Boolean { get; private set; }

        [ConfigurationEntry(Delimiter = ',')]
        public List<string> StringList { get; private set; }

        [ConfigurationEntry(Delimiter = ',')]
        public int[] IntegerArray { get; private set; }

        [ConfigurationEntry(Delimiter = ',')]
        public ArrayList ArrayList { get; private set; }

        [ConfigurationEntry(ErrorMessage = "This is a sample error message")]
        public Direction Enumeration { get; private set; }

        //[ConfigurationEntry(DecryptIfEncrypted = true)]
        //public string Encrypted { get; private set; }

        [ConfigurationEntry(CustomLoadMethod = "SplitStringCustomLoadMethod")]
        private string StringCustomLoadMethod { get; set; }

        public string Ip { get; private set; }

        public string Port { get; private set; }

        private void SplitStringCustomLoadMethod()
        {
            var array = StringCustomLoadMethod.Split(':');
            if (array.Length == 2)
            {
                Ip = array[0];
                Port = array[1];
            }
            else
            {
                throw new ConfigurationErrorsException("Tcp = " + StringCustomLoadMethod + " is not correct (ip:port)");
            }
        }

        [ConfigurationEntry]
        public ushort UInt16 { get; private set; }

        [ConfigurationEntry]
        public uint UInt32 { get; private set; }

        [ConfigurationEntry]
        public ulong UInt64 { get; private set; }

        [ConfigurationEntry(Delimiter = ';')]
        public IEnumerable<string> EnumerableStringList { get; private set; }
    }

    public enum Direction
    {
        Outbound,
        Inbound
    }
}
