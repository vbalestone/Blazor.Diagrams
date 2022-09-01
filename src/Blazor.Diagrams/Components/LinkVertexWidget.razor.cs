﻿using System;
using Blazor.Diagrams.Core.Models;
using Blazor.Diagrams.Core.Models.Base;
using Blazor.Diagrams.Extensions;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace Blazor.Diagrams.Components;

public partial class LinkVertexWidget : IDisposable
{
    private bool _shouldRender = true;

    [CascadingParameter] public BlazorDiagram BlazorDiagram { get; set; } = null!;
    [Parameter] public LinkVertexModel Vertex { get; set; } = null!;
    [Parameter] public string? Color { get; set; }
    [Parameter] public string? SelectedColor { get; set; }

    private string? ColorToUse => Vertex.Selected ? SelectedColor : Color;

    public void Dispose()
    {
        Vertex.Changed -= OnVertexChanged;
    }

    protected override void OnInitialized()
    {
        Vertex.Changed += OnVertexChanged;
    }

    protected override bool ShouldRender()
    {
        if (!_shouldRender) return false;

        _shouldRender = false;
        return true;
    }

    private void OnVertexChanged(Model _)
    {
        _shouldRender = true;
        InvokeAsync(StateHasChanged);
    }

    private void OnPointerDown(PointerEventArgs e)
    {
        BlazorDiagram.TriggerPointerDown(Vertex, e.ToCore());
    }

    private void OnPointerUp(PointerEventArgs e)
    {
        BlazorDiagram.TriggerPointerUp(Vertex, e.ToCore());
    }

    private void OnDoubleClick(MouseEventArgs e)
    {
        Vertex.Parent.Vertices.Remove(Vertex);
        Vertex.Parent.Refresh();
    }
}