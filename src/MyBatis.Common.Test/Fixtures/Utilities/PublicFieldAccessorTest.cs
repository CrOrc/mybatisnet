
using System;
using MyBatis.Common.Test.Domain;
using MyBatis.Common.Utilities;
using MyBatis.Common.Utilities.Objects.Members;
using NUnit.Framework;

namespace MyBatis.Common.Test.Fixtures.Utilities
{
    [TestFixture] 
    public class PublicFieldAccessorTest
    {
        protected ISetAccessorFactory factorySet = null;
        protected IGetAccessorFactory factoryGet = null;

        protected ISetAccessor intSetAccessor = null;
        protected IGetAccessor intGetAccessor = null;

        protected ISetAccessor longSetAccessor = null;
        protected IGetAccessor longGetAccessor = null;

        protected ISetAccessor sbyteSetAccessor = null;
        protected IGetAccessor sbyteGetAccessor = null;

        protected ISetAccessor datetimeSetAccessor = null;
        protected IGetAccessor datetimeGetAccessor = null;

        protected ISetAccessor decimalSetAccessor = null;
        protected IGetAccessor decimalGetAccessor = null;

        protected ISetAccessor byteSetAccessor = null;
        protected IGetAccessor byteGetAccessor = null;

        protected ISetAccessor stringSetAccessor = null;
        protected IGetAccessor stringGetAccessor = null;

        protected ISetAccessor charSetAccessor = null;
        protected IGetAccessor charGetAccessor = null;

        protected ISetAccessor shortSetAccessor = null;
        protected IGetAccessor shortGetAccessor = null;

        protected ISetAccessor ushortSetAccessor = null;
        protected IGetAccessor ushortGetAccessor = null;

        protected ISetAccessor uintSetAccessor = null;
        protected IGetAccessor uintGetAccessor = null;

        protected ISetAccessor ulongSetAccessor = null;
        protected IGetAccessor ulongGetAccessor = null;

        protected ISetAccessor boolSetAccessor = null;
        protected IGetAccessor boolGetAccessor = null;

        protected ISetAccessor doubleSetAccessor = null;
        protected IGetAccessor doubleGetAccessor = null;

        protected ISetAccessor floatSetAccessor = null;
        protected IGetAccessor floatGetAccessor = null;

        protected ISetAccessor guidSetAccessor = null;
        protected IGetAccessor guidGetAccessor = null;

        protected ISetAccessor timespanSetAccessor = null;
        protected IGetAccessor timespanGetAccessor = null;

        protected ISetAccessor accountSetAccessor = null;
        protected IGetAccessor accountGetAccessor = null;

        protected ISetAccessor enumSetAccessor = null;
        protected IGetAccessor enumGetAccessor = null;

        protected ISetAccessor nullableSetAccessor = null;
        protected IGetAccessor nullableGetAccessor = null;
        
        #region SetUp & TearDown

        /// <summary>
        /// SetUp
        /// </summary>
        [SetUp]
        public void SetUp()
        {
            intSetAccessor = factorySet.CreateSetAccessor(typeof(Property), "publicInt");
            intGetAccessor = factoryGet.CreateGetAccessor(typeof(Property), "publicInt");

            longSetAccessor = factorySet.CreateSetAccessor(typeof(Property), "publicLong");
            longGetAccessor = factoryGet.CreateGetAccessor(typeof(Property), "publicLong");

            sbyteSetAccessor = factorySet.CreateSetAccessor(typeof(Property), "publicSbyte");
            sbyteGetAccessor = factoryGet.CreateGetAccessor(typeof(Property), "publicSbyte");

            stringSetAccessor = factorySet.CreateSetAccessor(typeof(Property), "publicString");
            stringGetAccessor = factoryGet.CreateGetAccessor(typeof(Property), "publicString");

            datetimeSetAccessor = factorySet.CreateSetAccessor(typeof(Property), "publicDateTime");
            datetimeGetAccessor = factoryGet.CreateGetAccessor(typeof(Property), "publicDateTime");

            decimalSetAccessor = factorySet.CreateSetAccessor(typeof(Property), "publicDecimal");
            decimalGetAccessor = factoryGet.CreateGetAccessor(typeof(Property), "publicDecimal");

            byteSetAccessor = factorySet.CreateSetAccessor(typeof(Property), "publicByte");
            byteGetAccessor = factoryGet.CreateGetAccessor(typeof(Property), "publicByte");

            charSetAccessor = factorySet.CreateSetAccessor(typeof(Property), "publicChar");
            charGetAccessor = factoryGet.CreateGetAccessor(typeof(Property), "publicChar");

            shortSetAccessor = factorySet.CreateSetAccessor(typeof(Property), "publicShort");
            shortGetAccessor = factoryGet.CreateGetAccessor(typeof(Property), "publicShort");

            ushortSetAccessor = factorySet.CreateSetAccessor(typeof(Property), "publicUshort");
            ushortGetAccessor = factoryGet.CreateGetAccessor(typeof(Property), "publicUshort");

            uintSetAccessor = factorySet.CreateSetAccessor(typeof(Property), "publicUint");
            uintGetAccessor = factoryGet.CreateGetAccessor(typeof(Property), "publicUint");

            ulongSetAccessor = factorySet.CreateSetAccessor(typeof(Property), "publicUlong");
            ulongGetAccessor = factoryGet.CreateGetAccessor(typeof(Property), "publicUlong");

            boolSetAccessor = factorySet.CreateSetAccessor(typeof(Property), "publicBool");
            boolGetAccessor = factoryGet.CreateGetAccessor(typeof(Property), "publicBool");

            doubleSetAccessor = factorySet.CreateSetAccessor(typeof(Property), "publicDouble");
            doubleGetAccessor = factoryGet.CreateGetAccessor(typeof(Property), "publicDouble");

            floatSetAccessor = factorySet.CreateSetAccessor(typeof(Property), "publicFloat");
            floatGetAccessor = factoryGet.CreateGetAccessor(typeof(Property), "publicFloat");

            guidSetAccessor = factorySet.CreateSetAccessor(typeof(Property), "publicGuid");
            guidGetAccessor = factoryGet.CreateGetAccessor(typeof(Property), "publicGuid");

            timespanSetAccessor = factorySet.CreateSetAccessor(typeof(Property), "publicTimeSpan");
            timespanGetAccessor = factoryGet.CreateGetAccessor(typeof(Property), "publicTimeSpan");

            accountSetAccessor = factorySet.CreateSetAccessor(typeof(Property), "publicAccount");
            accountGetAccessor = factoryGet.CreateGetAccessor(typeof(Property), "publicAccount");

            enumSetAccessor = factorySet.CreateSetAccessor(typeof(Property), "publicDay");
            enumGetAccessor = factoryGet.CreateGetAccessor(typeof(Property), "publicDay");

            nullableSetAccessor = factorySet.CreateSetAccessor(typeof(Property), "publicintNullable");
            nullableGetAccessor = factoryGet.CreateGetAccessor(typeof(Property), "publicintNullable");
        }


        /// <summary>
        /// TearDown
        /// </summary>
        [TearDown]
        public void Dispose()
        {
        }

        #endregion


        /// <summary>
        /// Initialize an sqlMap
        /// </summary>
        [TestFixtureSetUp]
        protected virtual void SetUpFixture()
        {
            factoryGet = new GetAccessorFactory(true);
            factorySet = new SetAccessorFactory(true);
        }

        /// <summary>
        /// Dispose the SqlMap
        /// </summary>
        [TestFixtureTearDown]
        protected virtual void TearDownFixture()
        {
            factoryGet = null;
            factorySet = null;
        }

        /// <summary>
        /// Test setting null on integer property.
        /// </summary>
        [Test]
        public void TestSetNullOnIntegerField()
        {
            Property prop = new Property();
            prop.publicInt = -99;

            // Property accessor
            intSetAccessor.Set(prop, null);
            Assert.AreEqual(0, prop.publicInt);
        }

        /// <summary>
        /// Test setting an integer property.
        /// </summary>
        [Test]
        public void TestSetIntegerField()
        {
            Property prop = new Property();
            prop.publicInt = -99;

            // Property accessor
            int test = 57;
            intSetAccessor.Set(prop, test);
            Assert.AreEqual(test, prop.publicInt);
        }

        /// <summary>
        /// Test getting an integer property.
        /// </summary>
        [Test]
        public void TestGetIntegerField()
        {
            int test = -99;
            Property prop = new Property();
            prop.publicInt = test;

            // Property accessor
            Assert.AreEqual(test, intGetAccessor.Get(prop));
        }

        /// <summary>
        /// Test setting null on Long property.
        /// </summary>
        [Test]
        public void TestSetNullOnLongField()
        {
            Property prop = new Property();
            prop.publicLong = 78945566664213223;

            // Property accessor
            longSetAccessor.Set(prop, null);
            Assert.AreEqual((long)0, prop.publicLong);
        }

        /// <summary>
        /// Test setting an Long property.
        /// </summary>
        [Test]
        public void TestSetLongField()
        {
            Property prop = new Property();
            prop.publicLong = 78945566664213223;

            // Property accessor
            long test = 123456789987456;
            longSetAccessor.Set(prop, test);
            Assert.AreEqual(test, prop.publicLong);
        }

        /// <summary>
        /// Test getting an long property.
        /// </summary>
        [Test]
        public void TestGetLongField()
        {
            long test = 78945566664213223;
            Property prop = new Property();
            prop.publicLong = test;

            // Property accessor
            Assert.AreEqual(test, longGetAccessor.Get(prop));
        }

        /// <summary>
        /// Test setting null on sbyte property.
        /// </summary>
        [Test]
        public void TestSetNullOnSbyteField()
        {
            Property prop = new Property();
            prop.publicSbyte = 78;

            // Property accessor
            sbyteSetAccessor.Set(prop, null);
            Assert.AreEqual((sbyte)0, prop.publicSbyte);
        }

        /// <summary>
        /// Test setting an sbyte property.
        /// </summary>
        [Test]
        public void TestSetSbyteField()
        {
            Property prop = new Property();
            prop.publicSbyte = 78;

            // Property accessor
            sbyte test = 19;
            sbyteSetAccessor.Set(prop, test);
            Assert.AreEqual(test, prop.publicSbyte);
        }

        /// <summary>
        /// Test getting an sbyte property.
        /// </summary>
        [Test]
        public void TestGetSbyteField()
        {
            sbyte test = 78;
            Property prop = new Property();
            prop.publicSbyte = test;

            // Property accessor
            Assert.AreEqual(test, sbyteGetAccessor.Get(prop));
        }

        /// <summary>
        /// Test setting null on String property.
        /// </summary>
        [Test]
        public void TestSetNullOnStringField()
        {
            Property prop = new Property();
            prop.publicString = "abc";

            // Property accessor
            stringSetAccessor.Set(prop, null);
            Assert.IsNull(prop.publicString);
        }

        /// <summary>
        /// Test setting an String property.
        /// </summary>
        [Test]
        public void TestSetStringField()
        {
            Property prop = new Property();
            prop.publicString = "abc";

            // Property accessor
            string test = "wxc";
            stringSetAccessor.Set(prop, test);
            Assert.AreEqual(test, prop.publicString);
        }

        /// <summary>
        /// Test getting an String property.
        /// </summary>
        [Test]
        public void TestGetStringField()
        {
            string test = "abc";
            Property prop = new Property();
            prop.publicString = test;

            // Property accessor
            Assert.AreEqual(test, stringGetAccessor.Get(prop));
        }

        /// <summary>
        /// Test setting null on DateTime property.
        /// </summary>
        [Test]
        public void TestSetNullOnDateTimeField()
        {
            Property prop = new Property();
            prop.publicDateTime = DateTime.Now;

            // Property accessor
            datetimeSetAccessor.Set(prop, null);
            Assert.AreEqual(DateTime.MinValue, prop.publicDateTime);
        }

        /// <summary>
        /// Test setting an DateTime property.
        /// </summary>
        [Test]
        public void TestSetDateTimeField()
        {
            Property prop = new Property();
            prop.publicDateTime = DateTime.Now;

            // Property accessor
            DateTime test = new DateTime(1987, 11, 25);
            datetimeSetAccessor.Set(prop, test);
            Assert.AreEqual(test, prop.publicDateTime);
        }

        /// <summary>
        /// Test getting an DateTime property.
        /// </summary>
        [Test]
        public void TestGetDateTimeField()
        {
            DateTime test = new DateTime(1987, 11, 25);
            Property prop = new Property();
            prop.publicDateTime = test;

            // Property accessor
            Assert.AreEqual(test, datetimeGetAccessor.Get(prop));
        }

        /// <summary>
        /// Test setting null on decimal property.
        /// </summary>
        [Test]
        public void TestSetNullOnDecimalField()
        {
            Property prop = new Property();
            prop.publicDecimal = 45.187M;

            // Property accessor
            decimalSetAccessor.Set(prop, null);
            Assert.AreEqual(0.0M, prop.publicDecimal);
        }

        /// <summary>
        /// Test setting an decimal property.
        /// </summary>
        [Test]
        public void TestSetDecimalField()
        {
            Property prop = new Property();
            prop.publicDecimal = 45.187M;

            // Property accessor
            Decimal test = 789456.141516M;
            decimalSetAccessor.Set(prop, test);
            Assert.AreEqual(test, prop.publicDecimal);
        }

        /// <summary>
        /// Test getting an decimal property.
        /// </summary>
        [Test]
        public void TestGetDecimalField()
        {
            Decimal test = 789456.141516M;
            Property prop = new Property();
            prop.publicDecimal = test;

            // Property accessor
            Assert.AreEqual(test, decimalGetAccessor.Get(prop));
        }

        /// <summary>
        /// Test setting null on byte property.
        /// </summary>
        [Test]
        public void TestSetNullOnByteField()
        {
            Property prop = new Property();
            prop.publicByte = 78;

            // Property accessor
            byteSetAccessor.Set(prop, null);
            Assert.AreEqual((byte)0, prop.publicByte);
        }

        /// <summary>
        /// Test setting an byte property.
        /// </summary>
        [Test]
        public void TestSetByteField()
        {
            Property prop = new Property();
            prop.publicByte = 15;

            // Property accessor
            byte test = 94;
            byteSetAccessor.Set(prop, test);
            Assert.AreEqual(test, prop.publicByte);
        }

        /// <summary>
        /// Test getting an byte property.
        /// </summary>
        [Test]
        public void TestGetByteField()
        {
            byte test = 78;
            Property prop = new Property();
            prop.publicByte = test;

            // Property accessor
            Assert.AreEqual(test, byteGetAccessor.Get(prop));
        }

        /// <summary>
        /// Test setting null on char property.
        /// </summary>
        [Test]
        public void TestSetNullOnCharField()
        {
            Property prop = new Property();
            prop.publicChar = 'r';

            // Property accessor
            charSetAccessor.Set(prop, null);
            Assert.AreEqual('\0', prop.publicChar);
        }

        /// <summary>
        /// Test setting an char property.
        /// </summary>
        [Test]
        public void TestSetCharField()
        {
            Property prop = new Property();
            prop.publicChar = 'b';

            // Property accessor
            char test = 'j';
            charSetAccessor.Set(prop, test);
            Assert.AreEqual(test, prop.publicChar);
        }

        /// <summary>
        /// Test getting an char property.
        /// </summary>
        [Test]
        public void TestGetCharField()
        {
            char test = 'z';
            Property prop = new Property();
            prop.publicChar = test;

            // Property accessor
            Assert.AreEqual(test, charGetAccessor.Get(prop));
        }

        /// <summary>
        /// Test setting null on short property.
        /// </summary>
        [Test]
        public void TestSetNullOnShortField()
        {
            Property prop = new Property();
            prop.publicShort = 5;

            // Property accessor
            shortSetAccessor.Set(prop, null);
            Assert.AreEqual((short)0, prop.publicShort);
        }

        /// <summary>
        /// Test setting an short property.
        /// </summary>
        [Test]
        public void TestSetShortField()
        {
            Property prop = new Property();
            prop.publicShort = 9;

            // Property accessor
            short test = 45;
            shortSetAccessor.Set(prop, test);
            Assert.AreEqual(test, prop.publicShort);
        }

        /// <summary>
        /// Test getting an short property.
        /// </summary>
        [Test]
        public void TestGetShortField()
        {
            short test = 99;
            Property prop = new Property();
            prop.publicShort = test;

            // Property accessor
            Assert.AreEqual(test, shortGetAccessor.Get(prop));
        }

        /// <summary>
        /// Test setting null on ushort property.
        /// </summary>
        [Test]
        public void TestSetNullOnUShortField()
        {
            Property prop = new Property();
            prop.publicUshort = 5;

            // Property accessor
            ushortSetAccessor.Set(prop, null);
            Assert.AreEqual((ushort)0, prop.publicUshort);
        }

        /// <summary>
        /// Test setting an ushort property.
        /// </summary>
        [Test]
        public void TestSetUShortField()
        {
            Property prop = new Property();
            prop.publicUshort = 9;

            // Property accessor
            ushort test = 45;
            ushortSetAccessor.Set(prop, test);
            Assert.AreEqual(test, prop.publicUshort);
        }

        /// <summary>
        /// Test getting an ushort property.
        /// </summary>
        [Test]
        public void TestGetUShortField()
        {
            ushort test = 99;
            Property prop = new Property();
            prop.publicUshort = test;

            // Property accessor
            Assert.AreEqual(test, ushortGetAccessor.Get(prop));
        }

        /// <summary>
        /// Test setting null on uint property.
        /// </summary>
        [Test]
        public void TestSetNullOnUIntField()
        {
            Property prop = new Property();
            prop.publicUint = 5;

            // Property accessor
            uintSetAccessor.Set(prop, null);
            Assert.AreEqual((uint)0, prop.publicUint);
        }

        /// <summary>
        /// Test setting an uint property.
        /// </summary>
        [Test]
        public void TestSetUIntField()
        {
            Property prop = new Property();
            prop.publicUint = 9;

            // Property accessor
            uint test = 45;
            uintSetAccessor.Set(prop, test);
            Assert.AreEqual(test, prop.publicUint);
        }

        /// <summary>
        /// Test getting an uint property.
        /// </summary>
        [Test]
        public void TestGetUIntField()
        {
            uint test = 99;
            Property prop = new Property();
            prop.publicUint = test;

            // Property accessor
            Assert.AreEqual(test, uintGetAccessor.Get(prop));
        }

        /// <summary>
        /// Test setting null on ulong property.
        /// </summary>
        [Test]
        public void TestSetNullOnULongField()
        {
            Property prop = new Property();
            prop.publicUlong = 5L;

            // Property accessor
            ulongSetAccessor.Set(prop, null);
            Assert.AreEqual((ulong)0, prop.publicUlong);
        }

        /// <summary>
        /// Test setting an ulong property.
        /// </summary>
        [Test]
        public void TestSetULongField()
        {
            Property prop = new Property();
            prop.publicUlong = 45464646578;

            // Property accessor
            ulong test = 45;
            ulongSetAccessor.Set(prop, test);
            Assert.AreEqual(test, prop.publicUlong);
        }

        /// <summary>
        /// Test getting an ulong property.
        /// </summary>
        [Test]
        public void TestGetULongField()
        {
            ulong test = 99;
            Property prop = new Property();
            prop.publicUlong = test;

            // Property accessor
            Assert.AreEqual(test, ulongGetAccessor.Get(prop));
        }

        /// <summary>
        /// Test setting null on bool property.
        /// </summary>
        [Test]
        public void TestSetNullOnBoolField()
        {
            Property prop = new Property();
            prop.publicBool = true;

            // Property accessor
            boolSetAccessor.Set(prop, null);
            Assert.AreEqual(false, prop.publicBool);
        }

        /// <summary>
        /// Test setting an bool property.
        /// </summary>
        [Test]
        public void TestSetBoolField()
        {
            Property prop = new Property();
            prop.publicBool = false;

            // Property accessor
            bool test = true;
            boolSetAccessor.Set(prop, test);
            Assert.AreEqual(test, prop.publicBool);
        }

        /// <summary>
        /// Test getting an bool property.
        /// </summary>
        [Test]
        public void TestGetBoolField()
        {
            bool test = false;
            Property prop = new Property();
            prop.publicBool = test;

            // Property accessor
            Assert.AreEqual(test, boolGetAccessor.Get(prop));
        }

        /// <summary>
        /// Test setting null on double property.
        /// </summary>
        [Test]
        public void TestSetNullOnDoubleField()
        {
            Property prop = new Property();
            prop.publicDouble = 788956.56D;

            // Property accessor
            doubleSetAccessor.Set(prop, null);
            Assert.AreEqual(0.0D, prop.publicDouble);
        }

        /// <summary>
        /// Test setting an double property.
        /// </summary>
        [Test]
        public void TestSetDoubleField()
        {
            Property prop = new Property();
            prop.publicDouble = 56789123.45888D;

            // Property accessor
            double test = 788956.56D;
            doubleSetAccessor.Set(prop, test);
            Assert.AreEqual(test, prop.publicDouble);
        }

        /// <summary>
        /// Test getting an double property.
        /// </summary>
        [Test]
        public void TestGetDoubleField()
        {
            double test = 788956.56D;
            Property prop = new Property();
            prop.publicDouble = test;

            // Property accessor
            Assert.AreEqual(test, doubleGetAccessor.Get(prop));
        }

        /// <summary>
        /// Test setting null on float property.
        /// </summary>
        [Test]
        public void TestSetNullOnFloatField()
        {
            Property prop = new Property();
            prop.publicFloat = 565.45F;

            // Property accessor
            floatSetAccessor.Set(prop, null);
            Assert.AreEqual(0.0D, prop.publicFloat);
        }

        /// <summary>
        /// Test setting an float property.
        /// </summary>
        [Test]
        public void TestSetFloatField()
        {
            Property prop = new Property();
            prop.publicFloat = 565.45F;

            // Property accessor
            float test = 4567.45F;
            floatSetAccessor.Set(prop, test);
            Assert.AreEqual(test, prop.publicFloat);
        }

        /// <summary>
        /// Test getting an float property.
        /// </summary>
        [Test]
        public void TestGetFloatField()
        {
            float test = 565.45F;
            Property prop = new Property();
            prop.publicFloat = test;

            // Property accessor
            Assert.AreEqual(test, floatGetAccessor.Get(prop));
        }


        /// <summary>
        /// Test setting null on Guid property.
        /// </summary>
        [Test]
        public void TestSetNullOnGuidField()
        {
            Property prop = new Property();
            prop.publicGuid = Guid.NewGuid();

            // Property accessor
            guidSetAccessor.Set(prop, null);
            Assert.AreEqual(Guid.Empty, prop.publicGuid);
        }

        /// <summary>
        /// Test setting an Guid property.
        /// </summary>
        [Test]
        public void TestSetGuidField()
        {
            Property prop = new Property();
            prop.publicGuid = Guid.NewGuid();

            // Property accessor
            Guid test = Guid.NewGuid();
            guidSetAccessor.Set(prop, test);
            Assert.AreEqual(test, prop.publicGuid);
        }

        /// <summary>
        /// Test getting an Guid property.
        /// </summary>
        [Test]
        public void TestGetGuidField()
        {
            Guid test = Guid.NewGuid();
            Property prop = new Property();
            prop.publicGuid = test;

            // Property accessor
            Assert.AreEqual(test, guidGetAccessor.Get(prop));
        }


        /// <summary>
        /// Test the setting null on a TimeSpan property.
        /// </summary>
        [Test]
        public void TestSetNullOnTimeSpanField()
        {
            Property prop = new Property();
            prop.publicTimeSpan = new TimeSpan(5, 12, 57, 21, 13);

            // Property accessor
            timespanSetAccessor.Set(prop, null);
            Assert.AreEqual(new TimeSpan(0, 0, 0), prop.publicTimeSpan);
        }

        /// <summary>
        /// Test setting an TimeSpan property.
        /// </summary>
        [Test]
        public void TestSetTimeSpanField()
        {
            Property prop = new Property();
            prop.publicTimeSpan = new TimeSpan(5, 12, 57, 21, 13);

            // Property accessor
            TimeSpan test = new TimeSpan(15, 5, 21, 45, 35);
            timespanSetAccessor.Set(prop, test);
            Assert.AreEqual(test, prop.publicTimeSpan);
        }

        /// <summary>
        /// Test getting an TimeSpan property.
        /// </summary>
        [Test]
        public void TestGetTimeSpanField()
        {
            TimeSpan test = new TimeSpan(5, 12, 57, 21, 13);
            Property prop = new Property();
            prop.publicTimeSpan = test;

            // Property accessor
            Assert.AreEqual(test, timespanGetAccessor.Get(prop));
        }

        /// <summary>
        /// Test the setting null on a object property.
        /// </summary>
        [Test]
        public void TestSetNullOnAccountField()
        {
            Property prop = new Property();
            prop.publicAccount = new Account();
            prop.publicAccount.FirstName = "test";

            // Property accessor
            accountSetAccessor.Set(prop, null);
            Assert.AreEqual(null, prop.publicAccount);
        }

        /// <summary>
        /// Test getting an object property.
        /// </summary>
        [Test]
        public void TestGetAccountField()
        {
            Account test = new Account();
            test.FirstName = "Gilles";

            Property prop = new Property();
            prop.publicAccount = test;

            // Property accessor
            Assert.AreEqual(HashCodeProvider.GetIdentityHashCode(test), HashCodeProvider.GetIdentityHashCode(prop.publicAccount));
            Assert.AreEqual(test.FirstName, ((Account)accountGetAccessor.Get(prop)).FirstName);
        }

        /// <summary>
        /// Test setting an object property.
        /// </summary>
        [Test]
        public void TestSetAccountField()
        {
            Property prop = new Property();
            prop.publicAccount = new Account();
            prop.publicAccount.FirstName = "test";

            // Property accessor
            string firstName = "Gilles";
            Account test = new Account();
            test.FirstName = firstName;
            accountSetAccessor.Set(prop, test);

            Assert.AreEqual(firstName, prop.publicAccount.FirstName);
        }

        /// <summary>
        /// Test the setting null on a Enum Field.
        /// </summary>
        [Test]
        public void TestSetNullOnEnumField()
        {
            Property prop = new Property();

            // Property accessor
            enumSetAccessor.Set(prop, null);
            Assert.AreEqual(0, (int)prop.publicDay);
        }

        /// <summary>
        /// Test setting an Enum Field.
        /// </summary>
        [Test]
        public void TestSetEnumField()
        {
            Property prop = new Property();
            prop.publicDay = Days.Thu;

            // Property accessor
            Days test = Days.Wed;
            enumSetAccessor.Set(prop, test);
            Assert.AreEqual(test, prop.publicDay);
        }

        /// <summary>
        /// Test getting an Enum Field.
        /// </summary>
        [Test]
        public void TestGetEnumField()
        {
            Days test = Days.Wed;
            Property prop = new Property();
            prop.publicDay = test;

            // Property accessor
            Assert.AreEqual(test, enumGetAccessor.Get(prop));
        }

        /// <summary>
        /// Test the setting null on a nullable int Field.
        /// </summary>
        [Test]
        public void TestSetNullOnNullableIntField()
        {
            Property prop = new Property();
            prop.publicintNullable = 85;

            // Property accessor
            nullableSetAccessor.Set(prop, null);
            Assert.AreEqual(null, prop.IntNullable);
        }

        /// <summary>
        /// Test getting an nullable int Field.
        /// </summary>
        [Test]
        public void TestGetNullableIntField()
        {
            Int32? test = 55;
            Property prop = new Property();
            prop.publicintNullable = test;

            // Property accessor
            Assert.AreEqual(test, nullableGetAccessor.Get(prop));
        }

        /// <summary>
        /// Test setting an nullable int Field.
        /// </summary>
        [Test]
        public void TestSetNullableIntField()
        {
            Property prop = new Property();
            prop.publicintNullable = 99;

            // Property accessor
            Int32? test = 55;
            nullableSetAccessor.Set(prop, test);
            Assert.AreEqual(test, prop.publicintNullable);

        }
    	
    	public void SetPublicAccount(Property p, Account ac)
    	{
    		p.publicAccount = ac;
    	}
    }
}
