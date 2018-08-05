# runGulp

@echo off setlocal

set DEPLOYMENT\_SOURCE= set IN\_PLACE\_DEPLOYMENT=1

if exist ..\wwwroot\deploy.cmd \( pushd ..\wwwroot rem call deploy.cmd popd \)

rem kick of build of csproj

echo record deployment timestamp date /t &gt;&gt; ..\deployment.log time /t &gt;&gt; ..\deployment.log echo ---------------------- &gt;&gt; ..\deployment.log echo Deployment done

endlocal

