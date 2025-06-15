using System.Diagnostics;
using static WndView.Enums;
using static WndView.Functions;
using static WndView.Structs;

namespace WndView
{
    internal class Methods
    {
        //取得工作區大小
        public static Size GetScreenSize(in Form form)
        {
            Screen currentScreen = Screen.FromControl(form);
            return currentScreen.WorkingArea.Size;
        }


        //項目篩選
        public static bool IsWindowValidForCapture(IntPtr hWnd)
        {
            if (hWnd == GetShellWindow()) return false; // 排除桌面
            if (GetWindowTextLength(hWnd) == 0) return false; //排除沒有標題的視窗
            if (!IsWindowVisible(hWnd)) return false; // 排除隱藏視窗
            if (IsIconic(hWnd)) return false; // 排除縮小視窗
            if (CkeckBackgroundAppWindow(hWnd)) return false; //排除Cloaked的視窗
            if (GetAncestor(hWnd, GetAncestorFlags.GetRoot) != hWnd) return false; //排除子視窗或巢狀視窗
            return true;
        }

        //檢查handle是否為背景程序
        public static bool CkeckBackgroundAppWindow(in IntPtr hWnd)
        {
            int cloaked = 0;
            int hr = DwmGetWindowAttribute(hWnd, DWMWINDOWATTRIBUTE.Cloaked, out cloaked, sizeof(int));
            if (hr == 0 && cloaked != 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        //取得Icon
        public static Icon GetAppIcon(IntPtr hWnd)
        {
            try
            {
                // 用 exe 路徑取得 icon
                GetWindowThreadProcessId(hWnd, out uint pid);
                var proc = Process.GetProcessById((int)pid);
                string exePath = proc.MainModule?.FileName ?? "";
                Icon? icon = Icon.ExtractAssociatedIcon(exePath);
                if (icon != null) return icon;
            }
            catch { }

            return SystemIcons.Application;
        }

        #region DWM方法
        //依Handle建立DWM縮圖
        public static int RegisterThumbnail(in IntPtr Handle, in IntPtr ItemhWnd, out IntPtr thumb)
        {
            return DwmRegisterThumbnail(Handle, ItemhWnd, out thumb);
        }

        //釋放依Handle建立的DWM縮圖
        public static void UnregisterThumbnail(IntPtr thumb)
        {
            DwmUnregisterThumbnail(thumb);
        }

        //更新DWM縮圖
        public static void UpdateThumb(in IntPtr thumb, in int FormMargins, in int currentDwmWidth, in int currentDwmHeight)
        {
            if (thumb != IntPtr.Zero)
            {
                DWM_THUMBNAIL_PROPERTIES props = new DWM_THUMBNAIL_PROPERTIES
                {
                    dwFlags = (uint)(DWM.DWM_TNP_RECTDESTINATION | DWM.DWM_TNP_OPACITY | DWM.DWM_TNP_VISIBLE | DWM.DWM_TNP_SOURCECLIENTAREAONLY),
                    rcDestination = new ThumbRect(FormMargins, FormMargins, currentDwmWidth, currentDwmHeight),
                    opacity = 255,
                    fVisible = true,
                    fSourceClientAreaOnly = false
                };
                _ = DwmUpdateThumbnailProperties(thumb, ref props);
            }
        }

        //查詢DWM縮圖的原始尺寸
        public static int QueryThumbnailSize(in IntPtr thumb, out int initialDwmWidth, out int initialDwmHeight)
        {
            int result = DwmQueryThumbnailSourceSize(thumb, out ThumbSize CheckItemSize);
            initialDwmWidth = CheckItemSize.x;
            initialDwmHeight = CheckItemSize.y;
            return result;
        }
        #endregion
    }
}
