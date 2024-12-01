# Unity External Log Window

![Log Example](./media/LogExample.png)

Outputs a Unity game's log to an external console window.  This is similar to BepInEx's console window.

# Usage
Add the code below to add the console to Unity's logging system.

```csharp
    UnityExternalLogWindow.ExternalLog.Attach();
```
# Notes

Closing the console window will close the Unity process.


