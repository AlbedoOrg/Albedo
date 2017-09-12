@echo off
cls

IF NOT EXIST "build\tools\FAKE.Core\tools\Fake.exe" (
  nuget install "FAKE.Core" -OutputDirectory "build\tools" -ExcludeVersion -Version 4.63.2
)

"build\tools\FAKE.Core\tools\Fake.exe" build.fsx %*
