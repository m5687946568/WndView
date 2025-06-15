using System.Runtime.InteropServices;

public class HotkeyManager : NativeWindow, IDisposable
{
    [DllImport("user32.dll")]
    public static extern bool RegisterHotKey(IntPtr hWnd, int id, uint fsModifiers, Keys vk);

    [DllImport("user32.dll")]
    public static extern bool UnregisterHotKey(IntPtr hWnd, int id);

    private const int WM_HOTKEY = 0x0312;
    private Dictionary<int, Action> hotkeyActions = new Dictionary<int, Action>();
    private int nextId = 1;

    public HotkeyManager()
    {
        CreateHandle(new CreateParams());
    }

    public int Register(Keys key, uint modifiers, Action action)
    {
        int id = nextId++;
        RegisterHotKey(this.Handle, id, modifiers, key);
        hotkeyActions[id] = action;
        return id;
    }

    public void Unregister(int id)
    {
        UnregisterHotKey(this.Handle, id);
        hotkeyActions.Remove(id);
    }

    protected override void WndProc(ref Message m)
    {
        if (m.Msg == WM_HOTKEY)
        {
            int id = m.WParam.ToInt32();
            if (hotkeyActions.TryGetValue(id, out var action))
            {
                action?.Invoke();
            }
        }
        base.WndProc(ref m);
    }

    public void Dispose()
    {
        foreach (var id in hotkeyActions.Keys)
        {
            UnregisterHotKey(this.Handle, id);
        }
        DestroyHandle();
    }

}
