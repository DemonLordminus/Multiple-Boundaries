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
    //// ����Windows API����
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


    #region �������ߴ�
    // ����Windows API����
    [DllImport("user32.dll")]
    private static extern IntPtr GetForegroundWindow();

    [DllImport("user32.dll")]
    private static extern int GetSystemMetrics(int nIndex);

    public static int GetWindowBarHeight()
    {
        IntPtr handle = GetForegroundWindow();

        // SM_CYSIZEFRAME �� SM_CYCAPTION ��Ӧ���ڵı߿�ͱ������ߴ�
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
        uint pid = (uint)Process.GetCurrentProcess().Id;  // ��ǰ���� ID

        bool bResult = EnumWindows(new WNDENUMPROC(delegate (IntPtr hwnd, uint lParam)
        {
            uint id = 0;

            if (GetParent(hwnd) == IntPtr.Zero)
            {
                GetWindowThreadProcessId(hwnd, ref id);
                if (id == lParam)    // �ҵ����̶�Ӧ�������ھ��
                {
                    ptrWnd = hwnd;   // �Ѿ����������
                    SetLastError(0);    // �����޴���
                    return false;   // ���� false ����ֹö�ٴ���
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
        // ��ȡ����ڵľ��
        //nowWindow = GetActiveWindow();
        nowWindow = GetProcessWnd();
        primaryRes = Screen.currentResolution;
        screenSize = new Vector2(primaryRes.width, -primaryRes.height);
        // ��ȡ���ڵ���������ϵ�еľ���λ��
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






