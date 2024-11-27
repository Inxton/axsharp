USING AxUnit;

NAMESPACE MyTest

    {TestFixture}
    CLASS MyTestFixture
        {Test}
        METHOD PUBLIC MyTestMethod
            Assert.Equal(actual := TRUE, expected := TRUE);
        END_METHOD
    END_CLASS

END_NAMESPACE
