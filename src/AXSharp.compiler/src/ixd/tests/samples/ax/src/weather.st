{#ix[Container(Layout.Tabs)]}
CLASS weather
    VAR PUBLIC                        
        GeoLocation : GeoLocation;               
        {#ix[Container(Layout.Wrap)]}
        Temperature : REAL;
        Humidity : REAL;
        Location : STRING;  
        ChillFactor : REAL;
        Feeling : Feeling;
    END_VAR    
END_CLASS

CLASS weathers
    VAR PUBLIC
        i : ARRAY[0..50] OF weatherBase;
    END_VAR    
END_CLASS

TYPE
    ///<summary>
    /// Aa
    ///</summary>
    Feeling : (Freezing, 
        /// <summary>
        /// Cc
        /// </summary>
        Cold, 
        ///<summary>Dd</summary>
        Lookworm, Warm, 
        ///<summary>Ff</summary>
        Hot
        );
END_TYPE

TYPE
    ///<summary>
    /// Zz
    ///</summary>
    Colors : STRING (
        ///<summary>Yy</summary>
        RED := 'dsa',


        /// <summary>
        /// Xx
        /// </summary>
        GREEN := 'fs',

        ///<summary>Ww</summary>
        BLUE := 'rew'
    );
END_TYPE

TYPE
    ErrorStates : WORD (
        OK := WORD#16#000F,
        Failed := WORD#16#00F0,
        Pending := WORD#16#0F00
    ) := Pending; // Default will be Pending
END_TYPE