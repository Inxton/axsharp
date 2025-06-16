NAMESPACE axosimple
    TYPE
        HwIdentifiers : WORD
        (
            plc_line_HwID :=	WORD#32,
            plc_line_Rail_0_HwID :=	WORD#257,
            plc_line_plc_line_HwID :=	WORD#48,
            plc_line_plc_line_CPU_display_1_HwID :=	WORD#54,
            plc_line_plc_line_Card_reader_writer_1_HwID :=	WORD#51,
            plc_line_plc_line_OPC_UA_1_HwID :=	WORD#117,
            plc_line_plc_line_Virtual_communication_interface_HwID :=	WORD#135,
            plc_line_plc_line_profinet_x1_HwID :=	WORD#64,
            plc_line_plc_line_profinet_x1_Port_1_HwID :=	WORD#65,
            plc_line_plc_line_profinet_x1_Port_2_HwID :=	WORD#66,
            plc_line_plc_line_profinet_x2_HwID :=	WORD#72,
            plc_line_plc_line_profinet_x2_Port_3_HwID :=	WORD#73,
            plc_line_plc_line_profinet_x2_Port_4_HwID :=	WORD#74,
            plc_line_plc_line_profinet_x3_HwID :=	WORD#120,
            plc_line_plc_line_profinet_x3_Port_5_HwID :=	WORD#121,
            profinet_plc_line_HwID :=	WORD#256,
    
            NONE := WORD#0
        );
    END_TYPE
END_NAMESPACE
