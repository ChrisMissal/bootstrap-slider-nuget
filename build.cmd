@echo off
".nuget/Nuget.exe" "Install" "FAKE" "-OutputDirectory" ".packages" "-ExcludeVersion"
".packages\FAKE\tools\Fake.exe" build.fsx
