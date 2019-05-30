nuget restore

dotet restore

msbuild Savory.OAuth2.QQ.sln /t:rebuild /p:configuration=release

nuget pack Savory.OAuth2.QQ.nuspec

pause