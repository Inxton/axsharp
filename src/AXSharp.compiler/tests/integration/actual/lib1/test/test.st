USING AxUnit;
USING MyLibrary;

NAMESPACE MyTest

    {TestFixture}
    {S7.extern=ReadWrite}
    CLASS MyTestFixture

        VAR PROTECTED
            testInstance : MyClass;
        END_VAR

        {Test}
        METHOD PUBLIC MyTestMethod
            VAR_TEMP
                result : BOOL;
            END_VAR

            result := testInstance.MyMethod();

            Assert.Equal(actual := result, expected := FALSE);
        
        END_METHOD
    END_CLASS

END_NAMESPACE
