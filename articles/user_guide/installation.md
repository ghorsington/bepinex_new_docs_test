---
uid: installation
---

# Installing BepInEx

## Requirements

* *Supported Operating Systems*
    - Windows 7, 8, 8.1 and 10 (both x86 and x64 are supported)
    - Any OS that has support for [Wine](https://www.winehq.org/) (Linux, macOS, FreeBSD, ...)
    - Any OS that has support for [Mono](https://www.mono-project.com/) (Windows, Linux, macOS)
* *Supported Unity games*
    - Unity 4 or newer
    - **The game has to use Mono as the scripting backend. More info in [limitations section](<xref:limitations>).**

> [!IMPORTANT]
> Games built with IL2CPP are not supported!  
> For more information, read the whole explanation in the [limitations section](<xref:limitations>).

## Where to download BepInEx

BepInEx is distributed in two builds: *stable* and *bleeding edge*.

**Stable builds** are available on [GitHub](https://github.com/BepInEx/BepInEx/releases).  
Stable builds are released once a new iteration of BepInEx is considered feature-complete.  
They have the least bugs, but some newest features might not be available.  
**It is recommended to use stable builds in most cases.**

**Bleeding edge builds** are available on [BepisBuilds](https://builds.bepis.io/projects/bepinex_be).
Bleeding edge builds are always the latest builds of the source code. Thus they are the opposite to stable builds: they have the newest features and bugfixes available, but usually tend to be the most buggy.  
Therefore you should use bleeding edge builds only if you are asked to or if you want to preview the upcoming version of BepInEx.


## Installing BepInEx

Currently, BepInEx can be installed manually.

1. Download the correct version of BepInEx.

    [Download BepInEx from one of the available sources.](#where-to-download-bepinex)  
    In the download section, all BepInEx distributions are designated using the following naming convention:  
    
    `BepInEx_<arch>_<build>_<version>.zip`

    Each value has different options depending on the game and your OS:

    * `arch` -- The OS and architecture this BepInEx was built against. Has the following values:
        - `x86`: For computers running *32-bit Windows*.
        - `x64`: For computers running *64-bit Windows*.
        - `Patcher`: For computers running Linux, macOS and other non-Windows systems.
    * `build` -- *Only in bleeding edge builds.* Specifies the exact commit BepInEx was built against.
    * `version` -- Exact version of BepInEx.

2. Extract the contents into the game root.

    After you have downloaded the correct game version, extact the archive into the game folder where the game exectuable is located (so called *game root folder*).

3. *Only for `Patcher` builds:* Run the patcher.

    If you have downloaded and installed the `Patcher` build, run `BepInEx.Patcher.exe` which will permanently patch the game to run BepInEx.

    > [!NOTE]  
    > If you are using a non-Windows system, you might need to run the patcher via mono:  
    > `mono BepInEx.Patcher.exe`

## Next: Installing plugins and configuration.

After you're done installing BepInEx, you can start installing and using plugins. Simply put plugins into `BepInEx/plugins` folder in the game folder and you're good to go.

Additionally, you might want to configure plugins and BepInEx itself. For that, please refer to the [configuration guide](<xref:configuration>).