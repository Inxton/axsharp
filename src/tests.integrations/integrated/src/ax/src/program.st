PROGRAM MyProgram
    VAR_EXTERNAL          
        StartPolling_should_update_cyclic_property : RealMonsterData.RealMonster;         
        GH_ISSUE_183 : GH_ISSUE_183.GH_ISSUE_183_1;

        StartPolling_ConcurentOverload : RealMonsterData.RealMonster;  
    END_VAR        
    StartPolling_should_update_cyclic_property.Id := StartPolling_should_update_cyclic_property.Id + ULINT#1;

    StartPolling_should_update_cyclic_property.DriveA.NestedLevelOne.NestedLevelTwo.NestedLevelThree.Position := StartPolling_should_update_cyclic_property.DriveA.NestedLevelOne.NestedLevelTwo.NestedLevelThree.Position  + REAL#1.0;
    
    StartPolling_ConcurentOverload.Id := StartPolling_ConcurentOverload.Id + ULINT#1; 

    StartPolling_ConcurentOverload.ArrayOfBytes[0] := StartPolling_ConcurentOverload.ArrayOfBytes[1]; 
    StartPolling_ConcurentOverload.ArrayOfBytes[1] := StartPolling_ConcurentOverload.ArrayOfBytes[2]; 
    StartPolling_ConcurentOverload.ArrayOfBytes[2] := StartPolling_ConcurentOverload.ArrayOfBytes[3]; 
    StartPolling_ConcurentOverload.ArrayOfBytes[3] := TO_BYTE( TO_INT(StartPolling_ConcurentOverload.ArrayOfBytes[0]) + 1 );

    StartPolling_ConcurentOverload.ArrayOfDrives[0].Position    := StartPolling_ConcurentOverload.ArrayOfDrives[1].Position ;
    StartPolling_ConcurentOverload.ArrayOfDrives[0].Velo        := StartPolling_ConcurentOverload.ArrayOfDrives[1].Velo ;
    StartPolling_ConcurentOverload.ArrayOfDrives[0].Dcc         := StartPolling_ConcurentOverload.ArrayOfDrives[1].Acc ;
    StartPolling_ConcurentOverload.ArrayOfDrives[0].Acc         := StartPolling_ConcurentOverload.ArrayOfDrives[1].Dcc ;

    StartPolling_ConcurentOverload.ArrayOfDrives[1].Position    := StartPolling_ConcurentOverload.ArrayOfDrives[2].Position ;
    StartPolling_ConcurentOverload.ArrayOfDrives[1].Velo        := StartPolling_ConcurentOverload.ArrayOfDrives[2].Velo ;
    StartPolling_ConcurentOverload.ArrayOfDrives[1].Dcc         := StartPolling_ConcurentOverload.ArrayOfDrives[2].Acc ;
    StartPolling_ConcurentOverload.ArrayOfDrives[1].Acc         := StartPolling_ConcurentOverload.ArrayOfDrives[2].Dcc ;

    StartPolling_ConcurentOverload.ArrayOfDrives[2].Position    := StartPolling_ConcurentOverload.ArrayOfDrives[3].Position ;
    StartPolling_ConcurentOverload.ArrayOfDrives[2].Velo        := StartPolling_ConcurentOverload.ArrayOfDrives[3].Velo ;
    StartPolling_ConcurentOverload.ArrayOfDrives[2].Dcc         := StartPolling_ConcurentOverload.ArrayOfDrives[3].Acc ;
    StartPolling_ConcurentOverload.ArrayOfDrives[2].Acc         := StartPolling_ConcurentOverload.ArrayOfDrives[3].Dcc ;

    StartPolling_ConcurentOverload.ArrayOfDrives[3].Position    := StartPolling_ConcurentOverload.ArrayOfDrives[0].Position + 0.02;
    StartPolling_ConcurentOverload.ArrayOfDrives[3].Velo        := StartPolling_ConcurentOverload.ArrayOfDrives[0].Velo + 0.03;
    StartPolling_ConcurentOverload.ArrayOfDrives[3].Dcc         := StartPolling_ConcurentOverload.ArrayOfDrives[0].Acc + 0.04;
    StartPolling_ConcurentOverload.ArrayOfDrives[3].Acc         := StartPolling_ConcurentOverload.ArrayOfDrives[0].Dcc + 0.05;


    StartPolling_ConcurentOverload.DriveA.Position    := StartPolling_ConcurentOverload.ArrayOfDrives[0].Position ;
    StartPolling_ConcurentOverload.DriveA.Velo        := StartPolling_ConcurentOverload.ArrayOfDrives[0].Velo ;
    StartPolling_ConcurentOverload.DriveA.Dcc         := StartPolling_ConcurentOverload.ArrayOfDrives[0].Acc ;
    StartPolling_ConcurentOverload.DriveA.Acc         := StartPolling_ConcurentOverload.ArrayOfDrives[0].Dcc ;

    StartPolling_ConcurentOverload.DriveA.NestedLevelOne.Position    := StartPolling_ConcurentOverload.ArrayOfDrives[1].Position ;
    StartPolling_ConcurentOverload.DriveA.NestedLevelOne.Velo        := StartPolling_ConcurentOverload.ArrayOfDrives[1].Velo ;
    StartPolling_ConcurentOverload.DriveA.NestedLevelOne.Dcc         := StartPolling_ConcurentOverload.ArrayOfDrives[1].Acc ;
    StartPolling_ConcurentOverload.DriveA.NestedLevelOne.Acc         := StartPolling_ConcurentOverload.ArrayOfDrives[1].Dcc ;

    StartPolling_ConcurentOverload.DriveA.NestedLevelOne.NestedLevelTwo.Position    := StartPolling_ConcurentOverload.ArrayOfDrives[2].Position ;
    StartPolling_ConcurentOverload.DriveA.NestedLevelOne.NestedLevelTwo.Velo        := StartPolling_ConcurentOverload.ArrayOfDrives[2].Velo ;
    StartPolling_ConcurentOverload.DriveA.NestedLevelOne.NestedLevelTwo.Dcc         := StartPolling_ConcurentOverload.ArrayOfDrives[2].Acc ;
    StartPolling_ConcurentOverload.DriveA.NestedLevelOne.NestedLevelTwo.Acc         := StartPolling_ConcurentOverload.ArrayOfDrives[2].Dcc ;

    StartPolling_ConcurentOverload.DriveA.NestedLevelOne.NestedLevelTwo.NestedLevelThree.Position    := StartPolling_ConcurentOverload.ArrayOfDrives[3].Position ;
    StartPolling_ConcurentOverload.DriveA.NestedLevelOne.NestedLevelTwo.NestedLevelThree.Velo        := StartPolling_ConcurentOverload.ArrayOfDrives[3].Velo ;
    StartPolling_ConcurentOverload.DriveA.NestedLevelOne.NestedLevelTwo.NestedLevelThree.Dcc         := StartPolling_ConcurentOverload.ArrayOfDrives[3].Acc ;
    StartPolling_ConcurentOverload.DriveA.NestedLevelOne.NestedLevelTwo.NestedLevelThree.Acc         := StartPolling_ConcurentOverload.ArrayOfDrives[3].Dcc ;


END_PROGRAM
