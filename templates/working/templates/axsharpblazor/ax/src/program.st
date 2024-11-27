PROGRAM MyProgram
    VAR_EXTERNAL          
        counters : counters;                
    END_VAR        
    counters.wrap.RunCounter();
    counters.stack.RunCounter();
    counters.grid.RunCounter();
    counters.tabs.RunCounter();
END_PROGRAM
