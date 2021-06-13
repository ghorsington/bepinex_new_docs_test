---
uid: porting
---
# Porting BepInEx

## Notes on porting

While BepInEx 4.x is very game-agnostic, there are still some things one must know to get BepInEx working on different versions of Unity.

When trying to install BepInEx on a game it wasn't ever installed on before, note the following:


## 1. Check whether the game executable is x86 or x64

Some games are still being built as x86 executables. Thus, make sure that you use x86 BepInEx with x86 game and x64 BepInEx with x64 game.

Failing to do so will cause the game to not load BepInEx.

## 2. BepInEx won't work with .NET or IL2CPP runtimes

Currently (version 4.1) BepInEx will only work with games using Mono as their backend CLR runtime.  
You can check that the game uses Mono by looking for `mono.dll` in the game's folder (e.g. with Windows' search tool).

## 3. Different games may require different entrypoints

When you run BepInEx on a new game for the first time, you might encounter the same error as in [#45](https://github.com/BepInEx/BepInEx/issues/45):

```
[Fatal] Could not run preloader!
[Fatal] System.Exception: The entrypoint type is invalid! Please check your config.ini
```

The error means that BepInEx's default entry point configuration will not work with the game.

Currently there exist two main solutions:

1. If the game runs a new version of Unity (2017.1 or newer), in `config.ini` simply change
    ```ini
    entrypoint-assembly=UnityEngine.dll
    ```
    to
    ```ini
    entrypoint-assembly=UnityEngine.CoreModule.dll
    ```

2. *If you are a developer* (or have someone who is), use dnSpy to find a new entry point in `Assembly-CSharp.dll`.  
    The entry point should be placed in the very first Scene the game loads (or the very first MonoBehaviour the game uses)
    
    You can configure the entrypoint [in `config.ini`](./Configuration#preloader-section).
    