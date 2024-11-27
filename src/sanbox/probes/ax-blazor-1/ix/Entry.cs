 using AXSharp.Connector;

 public static class Entry
    {
        public static ax_blazor_1.ax_blazor_1 Plc
        {
            get
            {
                return new ax_blazor_1.ax_blazor_1(new ConnectorAdapter(typeof(DummyConnectorFactory)), new object[]{"192.168.0.1", "Everybody", ""});
            }
        }
    }