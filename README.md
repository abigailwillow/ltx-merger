# ltx-merger

## Description

Merges one ltx file into the other, used for Stalker modding

**Note:** This application relies on [.NET Core 3.1](https://dotnet.microsoft.com/download/dotnet/3.1/runtime). Without it, the application won't run.

## Usage
 
Open your terminal in the folder where you downloaded ltx-merger, then run `ltx-merger --source source.ltx --mods mods.ltx --output output.ltx` where `source.ltx` is the original file, `mods.ltx` is the modded ltx file that you would like to merge, and `output.ltx` is the output file to create. If the output is not specified the source file will be overwritten instead. You can use the command `ltx-merger -h` to get more information about the tool and its usages.

![image](https://user-images.githubusercontent.com/16174954/125197108-b830b300-e25c-11eb-82f7-f21a491a9fcd.png)
