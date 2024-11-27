using AXSharp.Presentation;

namespace ix_integration_library.IxComponentServiceViewModel
{
    public class IxComponentViewModel : RenderableViewModelBase
    {
        public IxComponentViewModel()
        {
        }
        public ixcomponent Component { get; set; }
        public override object Model { get => this.Component; set { this.Component = value as ixcomponent; } }
    }
}
