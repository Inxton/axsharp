NAMESPACE nmspc
 TYPE tGlobalData_22 :
STRUCT
         TraceabilityActive : Bool;
      END_STRUCT;
END_TYPE

TYPE tGlobalData_21 :
STRUCT
         TraceabilityActive : Bool;
      END_STRUCT;
END_TYPE

TYPE tBasicType_20 :
STRUCT
         tempBool : Bool;
      END_STRUCT;
END_TYPE

TYPE tBasicType_19 :
STRUCT
         tempBool : Bool;
      END_STRUCT;
END_TYPE

TYPE tBasicType_18 :
STRUCT
         tempBool : Bool;
      END_STRUCT;
END_TYPE

TYPE tBasicType_17 :
STRUCT
         tempBool : Bool;
      END_STRUCT;
END_TYPE

TYPE tLDM_Screewing_2_16 :
STRUCT
		{ S7_SetPoint := 'False'}
         Status  : tCheckDataDigital;
		{ S7_SetPoint := 'False'}
         Torque  : tCheckDataAnalog;
		{ S7_SetPoint := 'False'}
         Angle  : tCheckDataAnalog;
      END_STRUCT;
END_TYPE

TYPE tLDM_Screewing_1_15 :
STRUCT
		{ S7_SetPoint := 'False'}
         Status  : tCheckDataDigital;
		{ S7_SetPoint := 'False'}
         Torque  : tCheckDataAnalog;
		{ S7_SetPoint := 'False'}
         Angle  : tCheckDataAnalog;
      END_STRUCT;
END_TYPE

TYPE tBasicType_14 :
STRUCT
         tempBool : Bool;
      END_STRUCT;
END_TYPE

TYPE tBasicType_13 :
STRUCT
         tempBool : Bool;
      END_STRUCT;
END_TYPE

TYPE tLDM_Screewing_4_12 :
STRUCT
		{ S7_SetPoint := 'False'}
         Status  : tCheckDataDigital;
		{ S7_SetPoint := 'False'}
         Torque  : tCheckDataAnalog;
		{ S7_SetPoint := 'False'}
         Angle  : tCheckDataAnalog;
      END_STRUCT;
END_TYPE

TYPE tLDM_Screewing_3_11 :
STRUCT
		{ S7_SetPoint := 'False'}
         Status  : tCheckDataDigital;
		{ S7_SetPoint := 'False'}
         Torque  : tCheckDataAnalog;
		{ S7_SetPoint := 'False'}
         Angle  : tCheckDataAnalog;
      END_STRUCT;
END_TYPE

TYPE tLDM_Screewing_2_10 :
STRUCT
		{ S7_SetPoint := 'False'}
         Status  : tCheckDataDigital;
		{ S7_SetPoint := 'False'}
         Torque  : tCheckDataAnalog;
		{ S7_SetPoint := 'False'}
         Angle  : tCheckDataAnalog;
      END_STRUCT;
END_TYPE

TYPE tLDM_Screewing_1_9 :
STRUCT
		{ S7_SetPoint := 'False'}
         Status  : tCheckDataDigital;
		{ S7_SetPoint := 'False'}
         Torque  : tCheckDataAnalog;
		{ S7_SetPoint := 'False'}
         Angle  : tCheckDataAnalog;
      END_STRUCT;
END_TYPE

TYPE tSMI_Screewing_3_8 :
STRUCT
		{ S7_SetPoint := 'False'}
         Status  : tCheckDataDigital;
         Torque : tCheckDataAnalog;
         Angle : tCheckDataAnalog;
      END_STRUCT;
END_TYPE

TYPE tSMI_Screewing_2_7 :
STRUCT
		{ S7_SetPoint := 'False'}
         Status  : tCheckDataDigital;
         Torque : tCheckDataAnalog;
         Angle : tCheckDataAnalog;
      END_STRUCT;
END_TYPE

TYPE tSMI_Screewing_1_6 :
STRUCT
		{ S7_SetPoint := 'False'}
         Status  : tCheckDataDigital;
		{ S7_SetPoint := 'False'}
         Torque  : tCheckDataAnalog;
		{ S7_SetPoint := 'False'}
         Angle  : tCheckDataAnalog;
      END_STRUCT;
END_TYPE

TYPE tBasicType_5 :
STRUCT
         tempBool : Bool;
      END_STRUCT;
END_TYPE

TYPE tdigital_4 :
STRUCT
         Required : Bool;
         Measured : Bool;
      END_STRUCT;
END_TYPE

TYPE tanalog_3 :
STRUCT
         Min : Real;
         Measured : Real;
         Max : Real;
      END_STRUCT;
END_TYPE

TYPE tdata_2 :
STRUCT
         Required : String[128];
         Measured : String[128];
         StarNotationEnabled : Bool;
      END_STRUCT;
END_TYPE

TYPE tanalog_1 :
STRUCT
         Min : Real;
         Measured : Real;
         Max : Real;
      END_STRUCT;
END_TYPE

TYPE
{S7.extern=ReadWrite}
tCheckDataAnalog_1 :
   STRUCT
      IsExcluded : Bool;
      IsVisible : Bool;
      Typecheck : Int;   // 0=inactive, 1-logic,2-analog,3:data

      PassedTime : Time := T#50ms;
      FailedTime : Time := T#100ms;
      Result : Byte;
      NumberOfRetries : UInt;
      AllowedNumberOfRetries : UInt;
      SourceID : UInt;   // identifikator stanice/kroku/rezimu
      TimeStamp : Date_And_Time;
   END_STRUCT;

END_TYPE

TYPE
{S7.extern=ReadWrite}
tHeaderResults :
   STRUCT
      InProgress : Bool;
      IsPassed : Bool;
      IsFailed : Bool;
      IsReset : Bool;
      IsRework : Bool;
      IsMaster : Bool;
      IsSkipped : Bool;
      wasManual : Bool;
      wasReset : Bool;
   END_STRUCT;

END_TYPE

TYPE
{S7.extern=ReadWrite}
tCheckDataData :
   STRUCT
      IsExcluded : Bool;
      IsVisible : Bool;
      Typecheck : Int;   // 0=inactive, 1-logic,2-analog,3:data

      PassedTime : Time := T#50ms;
      FailedTime : Time := T#100ms;
      Result : Byte;
      NumberOfRetries : UInt;
      AllowedNumberOfRetries : UInt;
      SourceID : UInt;   // identifikator stanice/kroku/rezimu
      TimeStamp : Date_And_Time;
   END_STRUCT;

END_TYPE

TYPE
{S7.extern=ReadWrite}
tCheckDataAnalog :
   STRUCT
      IsExcluded : Bool;
      IsVisible : Bool;
      Typecheck : Int;   // 0=inactive, 1-logic,2-analog,3:data

      PassedTime : Time := T#50ms;
      FailedTime : Time := T#100ms;
      Result : Byte;
      NumberOfRetries : UInt;
      AllowedNumberOfRetries : UInt;
      SourceID : UInt;   // identifikator stanice/kroku/rezimu
      TimeStamp : Date_And_Time;
   END_STRUCT;

END_TYPE

TYPE
{S7.extern=ReadWrite}
tCheckDataDigital :
   STRUCT
      IsExcluded : Bool;
      IsVisible : Bool;
      Typecheck : Int;   // 0=inactive, 1-logic,2-analog,3:data

      PassedTime : Time := T#50ms;
      FailedTime : Time := T#100ms;
      Result : Byte;
      NumberOfRetries : UInt;
      AllowedNumberOfRetries : UInt;
      SourceID : UInt;   // identifikator stanice/kroku/rezimu
      TimeStamp : Date_And_Time;
   END_STRUCT;

END_TYPE

TYPE
{S7.extern=ReadWrite}
tRivetingData :
   STRUCT
      RivetingExclusion : Bool;
		{ S7_SetPoint := 'False'}
      InitialHeightCheck  : tCheckDataAnalog_1;
      InitialPulseLenght : Time;   // pociatocny pulz, nech sa urychli nahrievanie
      InitialPulseDutyCycle : Real;   // plnenie 0-100percent
      WorkingPulseLenghtMax : Time;   // pracovny kde sa tavi material, stop ked dosiahne potrebnu vysku
      WorkingPulseDutyCycle : Real;   // plnenie 0-100percent
      DeatachPulseDutyCycle : Real;   // plnenie 0-100percent
      PrePinCoolingStandBy : Time;
      PinCoolingTime : Time;
      SwitchOffHeatingHeight : Real;
		{ S7_SetPoint := 'False'}
      EndHeightCheck  : tCheckDataAnalog_1;
      DeatachPulseTime : Time;   // odtrhove nahriatie taviacej ihly, aby neotrhlo hlavicku
      RivetingTimingCheck : Time;
      PostCooling : Time;
   END_STRUCT;

END_TYPE

TYPE
{S7.extern=ReadWrite}
tHeaderCu :
   STRUCT
      NextOnPassed : UInt;
      NextOnFailed : UInt;
   END_STRUCT;

END_TYPE

TYPE
{S7.extern=ReadWrite}
tCu20Data :
   STRUCT
		{ S7_SetPoint := 'False'}
      Flow  : tHeaderCu;

		{ S7_SetPoint := 'False'}
      svetlovyska  : tCheckDataAnalog;
      svetlopritomnost : tCheckDataDigital;
      DataCheck_Priklad_10 : tCheckDataData;
      GearAdjusterFront : tCheckDataDigital;
      GearAdjusterInputFront : tCheckDataDigital;
      GearAdjusterBack : tCheckDataDigital;
      GearAdjusterInputBack : tCheckDataDigital;
      StudBallBack : tCheckDataDigital;
      StudBallTop : tCheckDataDigital;
      SlideLevelingMotor : tCheckDataDigital;
      SlideHorizontal : tCheckDataDigital;
      GearAdjusterFrontPressingOK : tCheckDataDigital;
      GearAdjusterBackPressingOK : tCheckDataDigital;
      ScrewingBackStatus : tCheckDataDigital;
      ScrewingTopStatus : tCheckDataDigital;
      ScrewingBackTorque : tCheckDataAnalog;
      ScrewingTopTorque : tCheckDataAnalog;
      ScrewingBackAngle : tCheckDataAnalog;
      ScrewingTopAngle : tCheckDataAnalog;
      RivetingResult : tCheckDataDigital;
      InitialHeightCheck : tCheckDataAnalog;
      EndHeightCheck : tCheckDataAnalog;
      PresentPCB : tCheckDataDigital;
      PCB_DATA : tCheckDataData;
		{ S7_SetPoint := 'True'}
      HotRivetingActive  : Bool;
      HotRivetingSetup : Int;
      HotRivetingData : tRivetingData;
      DummyCheckDigital : tCheckDataDigital;   // DigitalCheckData
      DummyCheckAnalog : tCheckDataAnalog;   // AnalogCheckData
      DummyCheckData : tCheckDataData;   // DataCheckData
   END_STRUCT;

END_TYPE

TYPE
{S7.extern=ReadWrite}
tLightType :
   STRUCT
      ID_Type : String[32];
      ID_Part : String[32];     
      Type_empty : Bool;
      Type_Base : Bool;
      Type_Mid : Bool;
      Type_High : Bool;
      Type_4 : Bool;
      Type_5 : Bool;
      Type_6 : Bool;
      Type_7 : Bool;
      Type_8 : Bool;
      LHD : Bool;   // EU verzia svetla
      RHD : Bool;   // UK verzia svetla
      Muster : Bool;
      Rework : Bool;
      LH : Bool;
      RH : Bool;
      PartINStation : Bool;
   END_STRUCT;

END_TYPE

TYPE
{S7.extern=ReadWrite}
tHeader :
   STRUCT
      Identificator : String[50];
      Reference : String[50];
      NextStation : UInt;
      LastStation : UInt;
      OpenOnStation : UInt;
      Carrier : UInt;
		{ S7_SetPoint := 'False'}
      Results  : tHeaderResults;
		{ S7_SetPoint := 'True'}
      Result  : Byte;
		
      TimeStampLast  : DTL;
      FailureCode : UDInt;
   END_STRUCT;

END_TYPE

TYPE
{S7.extern=ReadWrite}
tCu10Data :
   STRUCT
		{ S7_SetPoint := 'False'}
      Flow  : tHeaderCu;
      LabelRefText : String[32];
      LabelDMC : String[32];
      SerialNO : String[8];     
      LabelName : String[16];   // nazovetikety v tlaciarni
      sufix : String[2];
      TempDMC : String[32];
		{ S7_SetPoint := 'False'}
      Label  : tCheckDataData;



		{ S7_SetPoint := 'False'}
      SMI_Code  : tCheckDataData;




      LDM_Code : tCheckDataData;
      HoleHigh : tCheckDataDigital;
      DummyCheckDigital : tCheckDataDigital;   // DigitalCheckData
      DummyCheckAnalog : tCheckDataAnalog;   // AnalogCheckData
      DummyCheckData : tCheckDataData;   // DataCheckData
   END_STRUCT;

END_TYPE

TYPE
{S7.extern=ReadWrite}
tCu30Data :
   STRUCT
		{ S7_SetPoint := 'False'}
      Flow  : tHeaderCu;

		{ S7_SetPoint := 'False'}
      svetlovyska  : tCheckDataAnalog;
      svetlopritomnost : tCheckDataDigital;
      CameraCheckConnector : tCheckDataDigital;
		{ S7_SetPoint := 'True'}
      CameraJob  : Int;
      CameraExcluded : Bool;
      DummyCheckDigital : tCheckDataDigital;   // DigitalCheckData
      DummyCheckAnalog : tCheckDataAnalog;   // AnalogCheckData
      DummyCheckData : tCheckDataData;   // DataCheckData
   END_STRUCT;

END_TYPE

TYPE
{S7.extern=ReadWrite}
tCu40Data :
   STRUCT
		{ S7_SetPoint := 'False'}
      Flow  : tHeaderCu;

		{ S7_SetPoint := 'False'}
      svetlovyska  : tCheckDataAnalog;
      svetlopritomnost : tCheckDataDigital;


      LDM_Code : tCheckDataData;
      SBL_Code : tCheckDataData;
      ReflectorProjector_Code : tCheckDataData;
		{ S7_SetPoint := 'False'}
      Reflector  : tCheckDataDigital;
      BaseLDMModul : tCheckDataDigital;
      HighSBLModul : tCheckDataDigital;
      DummyCheckDigital : tCheckDataDigital;   // DigitalCheckData
      DummyCheckAnalog : tCheckDataAnalog;   // AnalogCheckData
      DummyCheckData : tCheckDataData;   // DataCheckData
   END_STRUCT;

END_TYPE

TYPE
{S7.extern=ReadWrite}
tCu50Data :
   STRUCT
		{ S7_SetPoint := 'False'}
      Flow  : tHeaderCu;

		{ S7_SetPoint := 'False'}
      svetlovyska  : tCheckDataAnalog;
      svetlopritomnost : tCheckDataDigital;
      svetlopritomnost_15 : tCheckDataDigital;
      FTI_Code : tCheckDataData;
      CameraCheckConnector : tCheckDataDigital;
      CameraCheckGuma : tCheckDataDigital;
      CameraCheckLight : tCheckDataDigital;
		{ S7_SetPoint := 'True'}
      CameraJob  : Int;
      CameraExcluded : Bool;
      DummyCheckDigital : tCheckDataDigital;   // DigitalCheckData
      DummyCheckAnalog : tCheckDataAnalog;   // AnalogCheckData
      DummyCheckData : tCheckDataData;   // DataCheckData
   END_STRUCT;

END_TYPE

TYPE
{S7.extern=ReadWrite}
tCu60Data :
   STRUCT
		{ S7_SetPoint := 'False'}
      Flow  : tHeaderCu;

      svetlopritomnost : tCheckDataDigital;
      ATE_ResultTest : tCheckDataDigital;
      DummyCheckDigital : tCheckDataDigital;   // DigitalCheckData
      DummyCheckAnalog : tCheckDataAnalog;   // AnalogCheckData
      DummyCheckData : tCheckDataData;   // DataCheckData
   END_STRUCT;

END_TYPE

TYPE
{S7.extern=ReadWrite}
tCu70Data :
   STRUCT
		{ S7_SetPoint := 'False'}
      Flow  : tHeaderCu;

      DummyCheckDigital : tCheckDataDigital;   // DigitalCheckData
      DummyCheckAnalog : tCheckDataAnalog;   // AnalogCheckData
      DummyCheckData : tCheckDataData;   // DataCheckData
   END_STRUCT;

END_TYPE

TYPE
{S7.extern=ReadWrite}
tCu80Data :
   STRUCT
		{ S7_SetPoint := 'False'}
      Flow  : tHeaderCu;

      DummyCheckDigital : tCheckDataDigital;   // DigitalCheckData
      DummyCheckAnalog : tCheckDataAnalog;   // AnalogCheckData
      DummyCheckData : tCheckDataData;   // DataCheckData
		{ S7_SetPoint := 'True'}
      CasOfukovania  : Time;
   END_STRUCT;

END_TYPE

TYPE
{S7.extern=ReadWrite}
tTier :
   STRUCT
      FirstRun : Bool;
      Progress : UInt;
      PreviousProgress : UInt;
		{ S7_SetPoint := 'True'}
      CycleCounter  : UInt;
		{ S7_SetPoint := 'True'}
      ActualCycleCounter  : UInt;
		{ S7_SetPoint := 'True'}
      ProgressTime  : Time;
		{InstructionName := 'TON_TIME'; LibVersion := '1.0'; S7_SetPoint := 'False'}
		{ S7_SetPoint := 'True'}
      Message  : String[50];
   END_STRUCT;

END_TYPE

TYPE
{S7.extern=ReadWrite}
tDataManager :
   STRUCT
      RequiredReference : String[50];
      PreviousReference : String[50];
      RequiredReferenceAck : String[50];
      ActiveStationNo : Int;
      HmiRequest : Int;
      ProcessingStatus : Byte;
      ReferenceLoadStart : Bool;
      ReferenceLoadDone : Bool;
      DataChangedOnHmi : Bool;
   END_STRUCT;

END_TYPE

TYPE
{S7.extern=ReadWrite}
tData :
   STRUCT
		{ S7_SetPoint := 'False'}
      EntityHeader  : tHeader;
		{ S7_SetPoint := 'False'}
      LightType  : tLightType;

      Cu10 : tCu10Data;
      Cu20 : tCu20Data;
      Cu30 : tCu30Data;
      Cu40 : tCu40Data;
      Cu50 : tCu50Data;
      Cu60 : tCu60Data;
      Cu70 : tCu70Data;
      Cu80 : tCu80Data;
		{ S7_SetPoint := 'True'}
      ReferenceNo  : Int;
   END_STRUCT;

END_TYPE

TYPE
{S7.extern=ReadWrite}
tHmiButtonMts :
   STRUCT
      Appearance : Int;
      ControlEnable : Bool;
      Visibility : Bool;
      ClickSetBit : Bool;
      ClickSwitchBit : Bool;
      PressReleaseBit : Bool;
   END_STRUCT;

END_TYPE

TYPE
{S7.extern=ReadWrite}
tData_bobock :
   STRUCT
		{ S7_SetPoint := 'False'}
      EntityHeader  : tHeader;
		{ S7_SetPoint := 'False'}
      LightType  : tLightType;

		{ S7_SetPoint := 'True'}
      ReferenceNo  : Int;
   END_STRUCT;

END_TYPE

TYPE
{S7.extern=ReadWrite}
tReference :
   STRUCT
		{ S7_SetPoint := 'False'}
      Manager  : tDataManager;
      Data : tData;
      tier : tTier;
   END_STRUCT;

END_TYPE

{#ix-db: DB_StorageNok}
{S7.extern=ReadWrite}
CLASS PUBLIC DB_StorageNok 
{ S7_Optimized_Access := 'TRUE' }
   VAR PUBLIC 
      Data : Array[0..2] of tData_bobock;
   END_VAR
END_CLASS


{#ix-db: DBRivetingSetup}
{S7.extern=ReadWrite}
CLASS PUBLIC DBRivetingSetup 
{ S7_Optimized_Access := 'TRUE' }

   VAR PUBLIC 
		{ S7_SetPoint := 'False'}
      SadyParametrov  : Array[0..20] of tRivetingData;
      SadaNaHmi : tRivetingData;
   END_VAR
   VAR PUBLIC 
		{ S7_SetPoint := 'False'}
      ButtonLoadFromData  : tHmiButtonMts;
      ButtonSaveToData : tHmiButtonMts;
   END_VAR
   VAR PUBLIC 
		{ S7_SetPoint := 'True'}
      AcviteIndex  : Int;
      AnalogRetainTareData : Array[0..10] of Real;
   END_VAR
END_CLASS


{#ix-db: DB_Storage}
{S7.extern=ReadWrite}
CLASS PUBLIC DB_Storage 
{ DB_Accessible_From_OPC_UA := 'FALSE' ;
 S7_Optimized_Access := 'TRUE' }
   VAR PUBLIC 
		{ S7_SetPoint := 'False'}
      Data  : Array[0..50] of tData_bobock;
   END_VAR
END_CLASS


{#ix-db: DbData}
{S7.extern=ReadWrite}
CLASS PUBLIC DbData 
{ S7_Optimized_Access := 'TRUE' }

   VAR PUBLIC 
		{ S7_SetPoint := 'False'}
      Reference  : Array[0..10] of tReference;
		{ S7_SetPoint := 'False'}
      Station  : Array[0..10] of tData;
      Carrier : Array[0..10] of tData;
		{ S7_SetPoint := 'True'}
      CarrierAtStation  : Array[0..10] of UInt;
   END_VAR
END_CLASS


 
END_NAMESPACE