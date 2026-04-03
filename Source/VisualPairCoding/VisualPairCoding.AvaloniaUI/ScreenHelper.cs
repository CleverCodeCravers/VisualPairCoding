using Avalonia;
using Avalonia.Controls;
using Avalonia.Platform.Storage;
using System.Linq;

namespace VisualPairCoding.AvaloniaUI;

public static class ScreenHelper
{
    /// <summary>
    /// Gets the bounds of the screen containing the given pixel point.
    /// Falls back to primary screen if no match found.
    /// </summary>
    public static PixelRect GetScreenBoundsAtPoint(Window ownerWindow, PixelPoint point)
    {
        var screens = ownerWindow.Screens;

        foreach (var screen in screens.All)
        {
            if (screen.Bounds.Contains(point))
                return screen.Bounds;
        }

        // Fallback: primary screen
        return screens.Primary?.Bounds ?? new PixelRect(0, 0, 1920, 1080);
    }

    /// <summary>
    /// Gets the bounds of the screen at the given index (0-based).
    /// Falls back to primary screen if index is out of range.
    /// </summary>
    public static PixelRect GetScreenBoundsAtIndex(Window ownerWindow, int screenIndex)
    {
        var screens = ownerWindow.Screens;

        if (screenIndex >= 0 && screenIndex < screens.All.Count)
            return screens.All[screenIndex].Bounds;

        // Fallback: primary screen
        return screens.Primary?.Bounds ?? new PixelRect(0, 0, 1920, 1080);
    }

    /// <summary>
    /// Gets the bounds of the screen where the RunSessionForm window is currently located.
    /// This is used as the best approximation for "where the user is looking"
    /// since Avalonia doesn't provide global mouse position easily.
    /// </summary>
    public static PixelRect GetScreenBoundsForWindow(Window window)
    {
        var windowCenter = new PixelPoint(
            window.Position.X + (int)(window.Width / 2),
            window.Position.Y + (int)(window.Height / 2));

        return GetScreenBoundsAtPoint(window, windowCenter);
    }

    /// <summary>
    /// Returns screen names for the UI dropdown.
    /// Index 0 = "Follow mouse", then "Monitor 1", "Monitor 2", etc.
    /// </summary>
    public static string[] GetScreenSelectionOptions(Window ownerWindow)
    {
        var screens = ownerWindow.Screens;
        var options = new string[screens.All.Count + 1];
        options[0] = "Follow window position";

        for (int i = 0; i < screens.All.Count; i++)
        {
            var s = screens.All[i];
            var primary = s.IsPrimary ? " (Primary)" : "";
            options[i + 1] = $"Monitor {i + 1}: {s.Bounds.Width}x{s.Bounds.Height}{primary}";
        }

        return options;
    }

    /// <summary>
    /// Resolves the target screen bounds based on user selection.
    /// selectedIndex 0 = follow window position, 1+ = fixed monitor.
    /// </summary>
    public static PixelRect ResolveTargetScreen(Window sessionWindow, int selectedScreenIndex)
    {
        if (selectedScreenIndex <= 0)
            return GetScreenBoundsForWindow(sessionWindow);

        return GetScreenBoundsAtIndex(sessionWindow, selectedScreenIndex - 1);
    }
}
