@CLS
@C:
@ECHO Running Recipe Publisher Batch File C:\Projects\LCI.Nuget\LCI.jQueryMobile.MVC4\LCI.jQueryMobile.MVC4.Publish.bat

CD \Projects\LCI.Nuget\LCI.jQueryMobile.MVC4

@REM  My API key is: f5d42567-3a06-4255-967e-cec0ac791faa
@REM  CD \Projects\NuGet
"C:\Program Files (x86)\Microsoft Visual Studio 10.0\Common7\Tools\nuget" Push C:\Projects\LCI.Nuget\LCI.jQueryMobile.MVC4\LCI.jQueryMobile.MVC4.1.0.5.nupkg
@PAUSE
