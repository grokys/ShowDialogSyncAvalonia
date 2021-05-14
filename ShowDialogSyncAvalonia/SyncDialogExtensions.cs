using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading;
using System.Threading.Tasks;
using Avalonia.Controls;
using Avalonia.Platform;
using Avalonia.Threading;

namespace ShowDialogSyncAvalonia
{
    public static class SyncDialogExtensions
    {
        public static void ShowDialogSync(this Window window, Window owner)
        {
            if (window.PlatformImpl is IMacOSTopLevelPlatformHandle)
            {

            }
            else
            {
                using var source = new CancellationTokenSource();
                window.ShowDialog(owner).ContinueWith(
                    t => source.Cancel(),
                    TaskScheduler.FromCurrentSynchronizationContext());
                Dispatcher.UIThread.MainLoop(source.Token);
            }
        }

        [return: MaybeNull]
        public static T ShowDialogSync<T>(this Window window, Window owner)
        {
            if (window.PlatformImpl is IMacOSTopLevelPlatformHandle)
            {
                throw new NotImplementedException();
            }
            else
            {
                using var source = new CancellationTokenSource();
                var result = default(T);
                window.ShowDialog<T>(owner).ContinueWith(
                    t => 
                    {
                        if (t.IsCompletedSuccessfully)
                            result = t.Result;
                        source.Cancel(); 
                    },
                    TaskScheduler.FromCurrentSynchronizationContext());
                Dispatcher.UIThread.MainLoop(source.Token);
                return result;
            }
        }
    }
}
