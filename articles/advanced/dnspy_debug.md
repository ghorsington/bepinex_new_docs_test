# Debugging with dnSpy

Due to the in-memory patching introduced in BepInEx 4.0, it is not possible to use dnSpy to debug plugins with the base installation of BepInEx. Instead, the developers must use the modified `BepInEx.Patcher.exe`.

> **NOTE**  
> The dnSpy-compatible BepInEx patcher does not support in-memory preloader patchers!  
> Thus, you can only debug normal plug-ins.

### Setting up BepInEx

The permanent assembly patcher for BepInEx is provided as a separate download in the [Bleeding Edge](http://bepisbuilds.dyn.mk/bepinex_be) builds. In order to use the dnSpy debugger functionality, you must apply the hardpatch to the game.

To set up BepInEx to work with dnSpy debugger, do the following:

1. Download the base BepInEx build and the BepInEx patcher (from [releases](https://github.com/BepInEx/BepInEx/releases), [Bleeding Edge builds](http://bepisbuilds.dyn.mk/bepinex_be)) or  [build them yourself](./Building).
2. Install base BepInEx as per the [installation guide](https://github.com/BepInEx/BepInEx/wiki/Installation#installation).
3. Place `BepInEx.Patcher.exe` into the game's root folder
4. Run `BepInEx.Patcher.exe` (or `mono BepInEx.Patcher.exe` if you are on Linux/macOS). This will patch game's `UnityEngine.dll` and place `BepInEx.Bootstrap.dll` into the game's `Managed` folder.
5. Modify `doorstop_config.ini` as follows: 

    ```ini
    enabled=false
    ```

### Setting up dnSpy

1. Download the latest version of [dnSpy](https://github.com/0xd4d/dnSpy/releases) and an appropritate version of Unity-debugging package
2. From the downloaded Unity-debugging package, pick `mono.dll` that corresponds to the version of the game you want to debug and replace the game's original `mono.dll` with it
3. Open dnSpy and open the plugin DLL (from `BepInEx` folder) you want to debug. Additionally, open game's own assemblies to debug (from `Managed` folder)
4. Set up appropriate breakpoints
5. Open `Debug > Start Debugging` dialogue
6. Select `Unity` as the Debug engine, select the game's EXE (not the launcher) and press OK to start debugging

If everything worked correctly, dnSpy will not time out in 30 seconds (or in whatever timeout time you configured) and will break on any breakpoint you set.

### Reverting back to original BepInEx

When you don't want to debug anymore, you can revert back to the original game installation as follows:

1. Restore `mono.dll` to the original version
2. From every `Managed` folder found in the game's root (or its subfolders), remove `BepInEx.Bootstrap.dll` and restore `UnityEngine.dll.bak`
3. Edit `doorstop_config.ini` as follows:
  
    ```ini
    enabled=true
    ```