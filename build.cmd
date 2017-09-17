@echo off
cls
IF NOT EXIST "build\tools\nuget.exe" (
  mkdir "build\tools\"
  powershell -Command "Invoke-WebRequest https://dist.nuget.org/win-x86-commandline/v4.3.0/nuget.exe -OutFile build\tools\nuget.exe"
)

IF NOT EXIST "build\tools\FAKE.Core\tools\Fake.exe" (
  "build\tools\nuget.exe" install "FAKE.Core" -OutputDirectory "build\tools" -ExcludeVersion -Version 4.63.2
)

"build\tools\FAKE.Core\tools\Fake.exe" build.fsx %*
