# deploy

@if "%SCM\_TRACE\_LEVEL%" NEQ "4" @echo off

:: ---------------------- :: KUDU Deployment Script :: Version: 1.0.15 :: ----------------------

:: Prerequisites :: -------------

:: Verify node.js installed where node 2&gt;nul &gt;nul IF %ERRORLEVEL% NEQ 0 \( echo Missing node.js executable, please install node.js, if already installed make sure it can be reached from current environment. goto error \)

:: Setup :: -----

setlocal enabledelayedexpansion

SET ARTIFACTS=%~dp0%..\artifacts

IF NOT DEFINED DEPLOYMENT\_SOURCE \( SET DEPLOYMENT\_SOURCE=%~dp0%. \)

IF NOT DEFINED DEPLOYMENT\_TARGET \( SET DEPLOYMENT\_TARGET=%ARTIFACTS%\wwwroot \)

IF NOT DEFINED NEXT\_MANIFEST\_PATH \( SET NEXT\_MANIFEST\_PATH=%ARTIFACTS%\manifest

IF NOT DEFINED PREVIOUS\_MANIFEST\_PATH \( SET PREVIOUS\_MANIFEST\_PATH=%ARTIFACTS%\manifest \) \)

IF NOT DEFINED KUDU\_SYNC\_CMD \( :: Install kudu sync echo Installing Kudu Sync call npm install kudusync -g --silent IF !ERRORLEVEL! NEQ 0 goto error

:: Locally just running "kuduSync" would also work SET KUDU\_SYNC\_CMD=%appdata%\npm\kuduSync.cmd \) IF NOT DEFINED DEPLOYMENT\_TEMP \( SET DEPLOYMENT\_TEMP=%temp%\_\_\_deployTemp%random% SET CLEAN\_LOCAL\_DEPLOYMENT\_TEMP=true \)

IF DEFINED CLEAN\_LOCAL\_DEPLOYMENT\_TEMP \( IF EXIST "%DEPLOYMENT\_TEMP%" rd /s /q "%DEPLOYMENT\_TEMP%" mkdir "%DEPLOYMENT\_TEMP%" \)

IF DEFINED MSBUILD\_PATH goto MsbuildPathDefined SET MSBUILD\_PATH=%ProgramFiles\(x86\)%\MSBuild\14.0\Bin\MSBuild.exe :MsbuildPathDefined

:::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::: :: Deployment :: ----------

echo Handling .NET Web Application deployment.

:: 1. Restore NuGet packages IF /I "Microsoft.Bot.Sample.LuisBot.sln" NEQ "" \( call :ExecuteCmd nuget restore "%DEPLOYMENT\_SOURCE%\Microsoft.Bot.Sample.LuisBot.sln" IF !ERRORLEVEL! NEQ 0 goto error \)

:: 2. Build to the temporary path IF /I "%IN\_PLACE\_DEPLOYMENT%" NEQ "1" \( call :ExecuteCmd "%MSBUILD\_PATH%" "%DEPLOYMENT\_SOURCE%\Microsoft.Bot.Sample.LuisBot.csproj" /nologo /verbosity:m /t:Build /t:pipelinePreDeployCopyAllFilesToOneFolder /p:\_PackageTempDir="%DEPLOYMENT\_TEMP%";AutoParameterizationWebConfigConnectionStrings=false;Configuration=Release;UseSharedCompilation=false /p:SolutionDir="%DEPLOYMENT\_SOURCE%.\" %SCM\_BUILD\_ARGS% \) ELSE \( call :ExecuteCmd "%MSBUILD\_PATH%" "%DEPLOYMENT\_SOURCE%\Microsoft.Bot.Sample.LuisBot.csproj" /nologo /verbosity:m /t:Build /p:AutoParameterizationWebConfigConnectionStrings=false;Configuration=Release;UseSharedCompilation=false /p:SolutionDir="%DEPLOYMENT\_SOURCE%.\" %SCM\_BUILD\_ARGS% \)

IF !ERRORLEVEL! NEQ 0 goto error

:: 3. KuduSync IF /I "%IN\_PLACE\_DEPLOYMENT%" NEQ "1" \( call :ExecuteCmd "%KUDU\_SYNC\_CMD%" -v 50 -f "%DEPLOYMENT\_TEMP%" -t "%DEPLOYMENT\_TARGET%" -n "%NEXT\_MANIFEST\_PATH%" -p "%PREVIOUS\_MANIFEST\_PATH%" -i ".git;.hg;.deployment;deploy.cmd" IF !ERRORLEVEL! NEQ 0 goto error \)

:::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::: goto end

:: Execute command routine that will echo out when error :ExecuteCmd setlocal set _CMD_=%\* call %_CMD_% if "%ERRORLEVEL%" NEQ "0" echo Failed exitCode=%ERRORLEVEL%, command=%_CMD_% exit /b %ERRORLEVEL%

:error endlocal echo An error has occurred during web site deployment. call :exitSetErrorLevel call :exitFromFunction 2&gt;nul

:exitSetErrorLevel exit /b 1

:exitFromFunction \(\)

:end endlocal echo Finished successfully.

