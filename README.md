# AZLpixelArtExporter
Program for render pixel art from Azur Lane (need decompiled "activity_coloring_template.lua" from scripts file).

LuaScripts/DecompOrigSrc/activity_coloring_template.lua
this is a file with coloring templates. For get it you need.
1. decode "scripts" file
2. extract "activity_coloring_template.lua" from decoded "scripts" (UnityBundle)
3. unlock "activity_coloring_template.lua" 
4. decompile unlocked "activity_coloring_template.lua" (Luajit 2.1.0 compiled lua)

```
AZLpixelartExporter [-r]
"-r" programm will delete "./IMG" before save rendered images.
```
