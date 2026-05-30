CLASS widget

END_CLASS

TYPE
    Gadget : STRUCT
        running : BOOL;
    END_STRUCT;
END_TYPE

TYPE
    Shade : (Light, Dark) := Light;
END_TYPE

INTERFACE PUBLIC IWidget

END_INTERFACE
