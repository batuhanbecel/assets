using System;
using System.Drawing;

namespace TaskbarStatus
{
    static class OverlayLayout
    {
        internal static Rectangle KeepOnScreen(Rectangle screen, Rectangle desired)
        {
            int width = Math.Min(desired.Width, screen.Width);
            int height = Math.Min(desired.Height, screen.Height);
            int left = Math.Max(screen.Left, Math.Min(desired.Left, screen.Right - width));
            int top = Math.Max(screen.Top, Math.Min(desired.Top, screen.Bottom - height));
            return new Rectangle(left, top, width, height);
        }

        // Whether a foreground window (its visible bounds) should count as fullscreen: it covers
        // the monitor give or take a few pixels (borderless games are not always exact), or it
        // covers the whole taskbar strip. Maximized windows stop at the work area, so they don't.
        internal static bool IsFullscreen(Rectangle window, Rectangle monitor, Rectangle taskbar, int slack)
        {
            if (!window.IntersectsWith(monitor)) return false;
            Rectangle grown = Rectangle.Inflate(window, slack, slack);
            if (grown.Contains(monitor)) return true;
            // Explorer may slide a hidden taskbar off the monitor; only the visible part counts.
            Rectangle bar = Rectangle.Intersect(taskbar, monitor);
            return bar.Width > 0 && bar.Height > 0 && grown.Contains(bar);
        }
    }
}
