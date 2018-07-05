# build

@echo off setlocal

set DEPLOYMENT\_SOURCE= set IN\_PLACE\_DEPLOYMENT=1

if exist ..\wwwroot\deploy.cmd \( pushd ..\wwwroot call deploy.cmd popd \)

endlocal

