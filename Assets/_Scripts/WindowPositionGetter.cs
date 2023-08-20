using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using UnityEngine;
using Debug = UnityEngine.Debug;

public class WindowPositionGetter : MonoBehaviour
{
    //// 导入Windows API函数
    //[DllImport("user32.dll")]
    //private static extern IntPtr GetActiveWindow();
    [SerializeField] bool isTest;
    public delegate bool WNDENUMPROC(IntPtr hwnd, uint lParam);
    [DllImport("user32.dll", SetLastError = true)]
    public static extern bool EnumWindows(WNDENUMPROC lpEnumFunc, uint lParam);

    [DllImport("user32.dll", SetLastError = true)]
    public static extern IntPtr GetParent(IntPtr hWnd);

    [DllImport("user32.dll")]
    public static extern uint GetWindowThreadProcessId(IntPtr hWnd, ref uint lpdwProcessId);

    [DllImport("kernel32.dll")]
    public static extern void SetLastError(uint dwErrCode);

    [DllImport("user32.dll")]
    static extern bool SetWindowPos(IntPtr hWnd, int hWndInsertAfter, int X, int Y, int cx, int cy, uint uFlags);


    #region 窗口栏尺寸
    // 导入Windows API函数
    [DllImport("user32.dll")]
    private static extern IntPtr GetForegroundWindow();

    [DllImport("user32.dll")]
    private static extern int GetSystemMetrics(int nIndex);

    public static int GetWindowBarHeight()
    {
        IntPtr handle = GetForegroundWindow();

        // SM_CYSIZEFRAME 和 SM_CYCAPTION 对应窗口的边框和标题栏尺寸
        int borderWidth = GetSystemMetrics(5); // SM_CYSIZEFRAME
        int titleBarHeight = GetSystemMetrics(4); // SM_CYCAPTION

        return borderWidth + titleBarHeight;
    }
    [EditorButton]
    private void ShowBorderSize()
    {
        Debug.Log(GetWindowBarHeight());
    }
    #endregion


    public static IntPtr GetProcessWnd()
    {
        IntPtr ptrWnd = IntPtr.Zero;
        uint pid = (uint)Process.GetCurrentProcess().Id;  // 当前进程 ID

        bool bResult = EnumWindows(new WNDENUMPROC(delegate (IntPtr hwnd, uint lParam)
        {
            uint id = 0;

            if (GetParent(hwnd) == IntPtr.Zero)
            {
                GetWindowThreadProcessId(hwnd, ref id);
                if (id == lParam)    // 找到进程对应的主窗口句柄
                {
                    ptrWnd = hwnd;   // 把句柄缓存起来
                    SetLastError(0);    // 设置无错误
                    return false;   // 返回 false 以终止枚举窗口
                }
            }

            return true;

        }), pid);

        return (!bResult && Marshal.GetLastWin32Error() == 0) ? ptrWnd : IntPtr.Zero;
    }

    [DllImport("user32.dll")]
    private static extern bool GetWindowRect(IntPtr hwnd, out RECT lpRect);
    
    private IntPtr nowWindow;
    [SerializeField] Vector2 windowPosition;
    [SerializeField] RECT rect;
    [SerializeField] Vector2 screenSize;
    Resolution primaryRes;
    public int offsetX,offsetY;
    [Serializable]
    private struct RECT
    {
        public int left;
        public int top;
        public int right;
        public int bottom;
    }

    void Start()
    {
        // 获取活动窗口的句柄
        //nowWindow = GetActiveWindow();
        nowWindow = GetProcessWnd();
        primaryRes = Screen.currentResolution;
        screenSize = new Vector2(primaryRes.width, -primaryRes.height);
        // 获取窗口的桌面坐标系中的绝对位置
        //GetWindowPosition();

        //if (!Application.isEditor || isTest)
        //{
        //    SetWindowsPosition();
        //}

        //Debug.Log("Window Position: " + windowPosition);
    }
    public Vector2 GetWindowPosition()
    {
        GetWindowRect(nowWindow, out rect);

        windowPosition = new Vector2(rect.left + (rect.right-rect.left)/2, -rect.top - (rect.bottom - rect.top)/2) - screenSize/2;

        return windowPosition;
    }

    const int ScreenSizeX = 800, ScreenSizeY = 600;
    const uint SWP_NOOWNERZORDER = 0x0200;
    const uint SWP_SHOWWINDOW = 0x0040;
    const uint SWP_NOSIZE = 0x0001;
    [EditorButton]
    public void SetWindowsPositionToCenter()
    {
        //Screen.SetResolution(800, 600,false);
        //SetWindowPos(nowWindow, 0, primaryRes.width / 2 - offsetX, primaryRes.height / 2 - offsetY - GetWindowBarHeight()/2, 
        //    ScreenSizeX, ScreenSizeY + GetWindowBarHeight(), SWP_NOOWNERZORDER | SWP_NOSIZE);
        //Screen.SetResolution(ScreenSizeX, ScreenSizeY, false);
        SetWindowsPosition(new Vector2(primaryRes.width / 2 - offsetX, primaryRes.height / 2 - offsetY));
    }
    public void SetWindowsPosition(Vector2 pos)
    {
        //const uint SWP_SHOWWINDOW = 0x0040;
        //Screen.SetResolution(800, 600,false);
        SetWindowPos(nowWindow, 0, (int)pos.x, (int)pos.y - GetWindowBarHeight() / 2,
            ScreenSizeX, ScreenSizeY + GetWindowBarHeight(), SWP_NOOWNERZORDER);
        //Screen.SetResolution(ScreenSizeX, ScreenSizeY, false);
    }
}






