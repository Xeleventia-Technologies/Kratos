using Microsoft.AspNetCore.Components;

using Kratos.Api.Common.Services;

namespace Kratos.Api._Pages._Layouts
{
    public class HtmxLayoutProvider : LayoutComponentBase
    {
        [Inject]
        private HtmxService HtmxService { get; set; } = default!;

        protected override void OnInitialized()
        {
            base.OnInitialized();
        }

        protected override void OnParametersSet()
        {
            base.OnParametersSet();
        }

        public RenderFragment? GetChildContent() => Body;

        protected override void BuildRenderTree(Microsoft.AspNetCore.Components.Rendering.RenderTreeBuilder builder)
        {
            if (!HtmxService.IsHtmxRequest())
            {
                // Render with MainLayout
                builder.OpenComponent<LayoutView>(0);
                builder.AddAttribute(1, "Layout", typeof(MainLayout));
                builder.AddAttribute(2, "ChildContent", (RenderFragment)(innerBuilder =>
                {
                    innerBuilder.AddContent(3, Body);
                }));
                builder.CloseComponent();
            }
            else
            {
                // Render without layout
                builder.AddContent(0, Body);
            }
        }
    }
}
