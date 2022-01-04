﻿using Avalonia.Media;

using System.Windows.Input;

namespace SmartFamily.Menus
{
    public interface IMenuItem
    {
        string Label { get; }

        DrawingGroup Icon { get; }

        ICommand Command { get; }

        string Gesture { get; }
    }
}