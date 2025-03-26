USING RealMonsterData;
CONFIGURATION MyConfiguration
    TASK Main(Interval := T#10ms, Priority := 1);
    PROGRAM P1 WITH Main: MyProgram;

    VAR_GLOBAL
       {S7.extern=ReadWrite}      
       Monster : MonsterData.Monster;
       {S7.extern=ReadWrite}
       OnlineToPlain_should_copy_entire_structure : MonsterData.Monster;
       {S7.extern=ReadWrite}
       PlainToOnline_should_copy_entire_structure : MonsterData.Monster;
       {S7.extern=ReadWrite}
       OnlineToShadowAsync_should_copy_entire_structure : MonsterData.Monster;
       {S7.extern=ReadWrite}
       ShadowToOnlineAsync_should_copy_entire_structure : MonsterData.Monster;
       
       {S7.extern=ReadWrite}
       ITwinObjectOnlineToPlain_should_copy_entire_structure : MonsterData.Monster;
       {S7.extern=ReadWrite}
       ITwinObjectPlainToOnline_should_copy_entire_structure : MonsterData.Monster;
       {S7.extern=ReadWrite}
       ITwinObjectOnlineToShadowAsync_should_copy_entire_structure : MonsterData.Monster;
       {S7.extern=ReadWrite}
       ITwinObjectShadowToOnlineAsync_should_copy_entire_structure : MonsterData.Monster;

       {S7.extern=ReadWrite}
       ShadowToPlainAsync_should_copy_entire_structure : MonsterData.Monster;
       {S7.extern=ReadWrite}
       PlainToShadowAsync_should_copy_entire_structure : MonsterData.Monster;

       {S7.extern=ReadWrite}
       ITwinObjectShadowToPlainAsync_should_copy_entire_structure : MonsterData.Monster;
       {S7.extern=ReadWrite}
       ITwinObjectPlainToShadowAsync_should_copy_entire_structure : MonsterData.Monster;

       {S7.extern=ReadWrite}
       Pokus : Pokus;

       {S7.extern=ReadWrite}
       RealMonster : RealMonsterData.RealMonster;
       {S7.extern=ReadWrite}
       OnlineToShadow_should_copy : RealMonsterData.RealMonster;
       {S7.extern=ReadWrite}
       ShadowToOnline_should_copy : RealMonsterData.RealMonster;
       {S7.extern=ReadWrite}
       OnlineToPlain_should_copy: RealMonsterData.RealMonster;
       {S7.extern=ReadWrite}
       PlainToOnline_should_copy: RealMonsterData.RealMonster;


       {S7.extern=ReadWrite}
       ITwinObjectOnlineToShadow_should_copy : RealMonsterData.RealMonster;
       {S7.extern=ReadWrite}
       ITwinObjectShadowToOnline_should_copy : RealMonsterData.RealMonster;
       {S7.extern=ReadWrite}
       ITwinObjectOnlineToPlain_should_copy: RealMonsterData.RealMonster;
       {S7.extern=ReadWrite}
       ITwinObjectPlainToOnline_should_copy: RealMonsterData.RealMonster;

       {S7.extern=ReadWrite}
       p_online_shadow: all_primitives;
       {S7.extern=ReadWrite}
       p_shadow_online: all_primitives;

       {S7.extern=ReadWrite}
       p_online_plain: all_primitives;
       {S7.extern=ReadWrite}
       p_plain_online: all_primitives;

       {S7.extern=ReadWrite}
       p_shadow_plain: all_primitives;
       {S7.extern=ReadWrite}
       p_plain_shadow: all_primitives;

       {S7.extern=ReadWrite}
       StartPolling_should_update_cyclic_property : RealMonsterData.RealMonster;

       {S7.extern=ReadWrite}
       StartPolling_ConcurentOverload : RealMonsterData.RealMonster;  

       {S7.extern=ReadWrite}
       ChangeDetections : RealMonsterData.RealMonster;  

       {S7.extern=ReadWrite}
       GH_ISSUE_183 : GH_ISSUE_183.GH_ISSUE_183_1;
    END_VAR
END_CONFIGURATION

CLASS Pokus
    VAR PUBLIC
        Nested : Nested;
    END_VAR
    
END_CLASS

CLASS Nested
    VAR PUBLIC
        SomeString : STRING;
        SomeInt : INT;
        SomeByte : BYTE;
    END_VAR    
END_CLASS    
