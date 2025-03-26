CONFIGURATION MyConfiguration
TASK Main(Interval := T#10ms, Priority := 1);
PROGRAM P1 WITH Main: MyProgram;
	VAR_GLOBAL
	{#ix-attr: [DBAttribute()]}
	{S7.extern=ReadWrite}
	DB_StorageNok : nmspc.DB_StorageNok;
	{#ix-attr: [DBAttribute()]}
	{S7.extern=ReadWrite}
	DBRivetingSetup : nmspc.DBRivetingSetup;
	{#ix-attr: [DBAttribute()]}
	{S7.extern=ReadWrite}
	DB_Storage : nmspc.DB_Storage;
	{#ix-attr: [DBAttribute()]}
	{S7.extern=ReadWrite}
	DbData : nmspc.DbData;
	END_VAR
END_CONFIGURATION
PROGRAM MyProgram
    VAR
        
    END_VAR
    ;
END_PROGRAM