// AXSharp.ConnectorTests
// Copyright (c) 2023 Peter Kurhajec (PTKu), MTS,  and Contributors. All Rights Reserved.
// Contributors: https://github.com/ix-ax/axsharp/graphs/contributors
// See the LICENSE file in the repository root for more information.
// https://github.com/ix-ax/axsharp/blob/dev/LICENSE
// Third party licenses: https://github.com/ix-ax/axsharp/blob/master/notices.md

namespace AXSharp.ConnectorTests
{
    using AXSharp.Connector;
    using System;
    using Xunit;
    using AXSharp.Connector.ValueTypes;
#pragma warning disable CS8603
    public class ConnectorFactoryTests
    {
        private class TestConnectorFactory : ConnectorFactory
        {
            public override Connector CreateConnector(object[] parameters)
            {

                return default(Connector);
            }

            public override OnlinerBool CreateBOOL(ITwinObject parent, string readableTail, string symbolTail)
            {
                return default(OnlinerBool);
            }

            public override OnlinerByte CreateBYTE(ITwinObject parent, string readableTail, string symbolTail)
            {
                return default(OnlinerByte);
            }

            public override OnlinerWord CreateWORD(ITwinObject parent, string readableTail, string symbolTail)
            {
                return default(OnlinerWord);
            }

            public override OnlinerDWord CreateDWORD(ITwinObject parent, string readableTail, string symbolTail)
            {
                return default(OnlinerDWord);
            }

            public override OnlinerLWord CreateLWORD(ITwinObject parent, string readableTail, string symbolTail)
            {
                return default(OnlinerLWord);
            }

            public override OnlinerSInt CreateSINT(ITwinObject parent, string readableTail, string symbolTail)
            {
                return default(OnlinerSInt);
            }

            public override OnlinerUSInt CreateUSINT(ITwinObject parent, string readableTail, string symbolTail)
            {
                return default(OnlinerUSInt);
            }

            public override OnlinerInt CreateINT(ITwinObject parent, string readableTail, string symbolTail)
            {
                return default(OnlinerInt);
            }

            public override OnlinerUInt CreateUINT(ITwinObject parent, string readableTail, string symbolTail)
            {
                return default(OnlinerUInt);
            }

            public override OnlinerDInt CreateDINT(ITwinObject parent, string readableTail, string symbolTail)
            {
                return default(OnlinerDInt);
            }

            public override OnlinerUDInt CreateUDINT(ITwinObject parent, string readableTail, string symbolTail)
            {
                return default(OnlinerUDInt);
            }

            public override OnlinerLInt CreateLINT(ITwinObject parent, string readableTail, string symbolTail)
            {
                return default(OnlinerLInt);
            }

            public override OnlinerULInt CreateULINT(ITwinObject parent, string readableTail, string symbolTail)
            {
                return default(OnlinerULInt);
            }

            public override OnlinerReal CreateREAL(ITwinObject parent, string readableTail, string symbolTail)
            {
                return default(OnlinerReal);
            }

            public override OnlinerLReal CreateLREAL(ITwinObject parent, string readableTail, string symbolTail)
            {
                return default(OnlinerLReal);
            }

            public override OnlinerString CreateSTRING(ITwinObject parent, string readableTail, string symbolTail)
            {
                return default(OnlinerString);
            }

            public override OnlinerWString CreateWSTRING(ITwinObject parent, string readableTail, string symbolTail)
            {
                return default(OnlinerWString);
            }

            public override OnlinerTime CreateTIME(ITwinObject parent, string readableTail, string symbolTail)
            {
                return default(OnlinerTime);
            }

            public override OnlinerDateTime CreateDATE_AND_TIME(ITwinObject parent, string readableTail, string symbolTail)
            {
                return default(OnlinerDateTime);
            }

            public override OnlinerDate CreateDATE(ITwinObject parent, string readableTail, string symbolTail)
            {
                return default(OnlinerDate);
            }

            public override OnlinerTimeOfDay CreateTIME_OF_DAY(ITwinObject parent, string readableTail, string symbolTail)
            {
                return default(OnlinerTimeOfDay);
            }

            public override OnlinerLTime CreateLTIME(ITwinObject parent, string readableTail, string symbolTail)
            {
                return default(OnlinerLTime);
            }

            public override OnlinerWChar CreateWCHAR(ITwinObject parent, string readableTail, string symbolTail)
            {
                return default(OnlinerWChar);
            }

            public override OnlinerDate CreateLDATE(ITwinObject parent, string readableTail, string symbolTail)
            {
                return default(OnlinerDate);
            }

            public override OnlinerLDateTime CreateLDATE_AND_TIME(ITwinObject parent, string readableTail, string symbolTail)
            {
                return default(OnlinerLDateTime);
            }

            public override OnlinerLTimeOfDay CreateLTIME_OF_DAY(ITwinObject parent, string readableTail, string symbolTail)
            {
                return default(OnlinerLTimeOfDay);
            }

            public override OnlinerChar CreateCHAR(ITwinObject parent, string readableTail, string symbolTail)
            {
                return default(OnlinerChar);
            }
        }

        private TestConnectorFactory _testClass;

        public ConnectorFactoryTests()
        {
            _testClass = new TestConnectorFactory();
        }

        [Fact]
        public void CanCallCreateBOOLWithNoParameters()
        {
            // Act
            var result = TestConnectorFactory.CreateBOOL();

            // Assert
            Assert.IsType<OnlinerBool>(result);
        }

        [Fact]
        public void CanCallCreateBYTEWithNoParameters()
        {
            // Act
            var result = TestConnectorFactory.CreateBYTE();

            // Assert
            Assert.IsType<OnlinerByte>(result);
        }

        [Fact]
        public void CanCallCreateWORDWithNoParameters()
        {
            // Act
            var result = TestConnectorFactory.CreateWORD();

            // Assert
            Assert.IsType<OnlinerWord>(result);
        }

        [Fact]
        public void CanCallCreateDWORDWithNoParameters()
        {
            // Act
            var result = TestConnectorFactory.CreateDWORD();

            // Assert
            Assert.IsType<OnlinerDWord>(result);
        }

        [Fact]
        public void CanCallCreateLWORDWithNoParameters()
        {
            // Act
            var result = TestConnectorFactory.CreateLWORD();

            // Assert
            Assert.IsType<OnlinerLWord>(result);
        }

        [Fact]
        public void CanCallCreateSINTWithNoParameters()
        {
            // Act
            var result = TestConnectorFactory.CreateSINT();

            // Assert
            Assert.IsType<OnlinerSInt>(result);
        }

        [Fact]
        public void CanCallCreateUSINTWithNoParameters()
        {
            // Act
            var result = TestConnectorFactory.CreateUSINT();

            // Assert
            Assert.IsType<OnlinerUSInt>(result);
        }

        [Fact]
        public void CanCallCreateINTWithNoParameters()
        {
            // Act
            var result = TestConnectorFactory.CreateINT();

            // Assert
            Assert.IsType<OnlinerInt>(result);
        }

        [Fact]
        public void CanCallCreateUINTWithNoParameters()
        {
            // Act
            var result = TestConnectorFactory.CreateUINT();

            // Assert
            Assert.IsType<OnlinerUInt>(result);
        }

        [Fact]
        public void CanCallCreateDINTWithNoParameters()
        {
            // Act
            var result = TestConnectorFactory.CreateDINT();

            // Assert
            Assert.IsType<OnlinerDInt>(result);
        }

        [Fact]
        public void CanCallCreateUDINTWithNoParameters()
        {
            // Act
            var result = TestConnectorFactory.CreateUDINT();

            // Assert
            Assert.IsType<OnlinerUDInt>(result);
        }

        [Fact]
        public void CanCallCreateLINTWithNoParameters()
        {
            // Act
            var result = TestConnectorFactory.CreateLINT();

            // Assert
            Assert.IsType<OnlinerLInt>(result);
        }

        [Fact]
        public void CanCallCreateULINTWithNoParameters()
        {
            // Act
            var result = TestConnectorFactory.CreateULINT();

            // Assert
            Assert.IsType<OnlinerULInt>(result);
        }

        [Fact]
        public void CanCallCreateREALWithNoParameters()
        {
            // Act
            var result = TestConnectorFactory.CreateREAL();

            // Assert
            Assert.IsType<OnlinerReal>(result);
        }

        [Fact]
        public void CanCallCreateLREALWithNoParameters()
        {
            // Act
            var result = TestConnectorFactory.CreateLREAL();

            // Assert
            Assert.IsType<OnlinerLReal>(result);
        }

        [Fact]
        public void CanCallCreateSTRINGWithNoParameters()
        {
            // Act
            var result = TestConnectorFactory.CreateSTRING();

            // Assert
            Assert.IsType<OnlinerString>(result);
        }

        [Fact]
        public void CanCallCreateWSTRINGWithNoParameters()
        {
            // Act
            var result = TestConnectorFactory.CreateWSTRING();

            // Assert
            Assert.IsType<OnlinerWString>(result);
        }

        [Fact]
        public void CanCallCreateTIMEWithNoParameters()
        {
            // Act
            var result = TestConnectorFactory.CreateTIME();

            // Assert
            Assert.IsType<OnlinerTime>(result);
        }

        [Fact]
        public void CanCallCreateDATE_TIME()
        {
            // Act
            var result = TestConnectorFactory.CreateDATE_TIME();

            // Assert
            Assert.IsType<OnlinerDateTime>(result);
        }

        [Fact]
        public void CanCallCreateDATEWithNoParameters()
        {
            // Act
            var result = TestConnectorFactory.CreateDATE();

            // Assert
            Assert.IsType<OnlinerDate>(result);
        }

        [Fact]
        public void CanCallCreateTIME_OF_DAYWithNoParameters()
        {
            // Act
            var result = TestConnectorFactory.CreateTIME_OF_DAY();

            // Assert
            Assert.IsType<OnlinerTimeOfDay>(result);
        }

        [Fact]
        public void CanCallCreateLTIMEWithNoParameters()
        {
            // Act
            var result = TestConnectorFactory.CreateLTIME();

            // Assert
            Assert.IsType<OnlinerLTime>(result);
        }

        [Fact]
        public void CanCallCreateCHARWithNoParameters()
        {
            // Act
            var result = TestConnectorFactory.CreateCHAR();

            // Assert
            Assert.IsType<OnlinerChar>(result);
        }

        [Fact]
        public void CanCallCreateWCHARWithNoParameters()
        {
            // Act
            var result = TestConnectorFactory.CreateWCHAR();

            // Assert
            Assert.IsType<OnlinerWChar>(result);
        }

        [Fact]
        public void CanCallCreateLDATEWithNoParameters()
        {
            // Act
            var result = TestConnectorFactory.CreateLDATE();

            // Assert
            Assert.IsType<OnlinerDate>(result);
        }

        [Fact]
        public void CanCallCreateLDATE_AND_TIMEWithNoParameters()
        {
            // Act
            var result = TestConnectorFactory.CreateLDATE_AND_TIME();

            // Assert
            Assert.IsType<OnlinerLDateTime>(result);
        }

        [Fact]
        public void CanCallCreateLTIME_OF_DAYWithNoParameters()
        {
            // Act
            var result = TestConnectorFactory.CreateLTIME_OF_DAY();

            // Assert
            Assert.IsType<OnlinerLTimeOfDay>(result);
        }
    }
#pragma warning restore CS8603
}