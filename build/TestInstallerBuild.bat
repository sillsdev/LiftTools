call "c:\Program Files (x86)\Microsoft Visual Studio 10.0\VC\vcvarsall.bat"

pushd c:\dev\lifttools\build
MSbuild /target:installer /property:teamcity_build_checkoutDir=c:\dev\lifttools  /property:teamcity_dotnet_nunitlauncher_msbuild_task="notthere" /property:BUILD_NUMBER="*.*.6.789" /property:Minor="1"
popd
PAUSE

#/verbosity:detailed