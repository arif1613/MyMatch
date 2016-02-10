@CLS
@C:
@ECHO Running Recipe Packager Batch File C:\Projects\LCI.Nuget\LCI.jQueryMobile.MVC4\LCI.jQueryMobile.MVC4.Package.bat

CD \Projects\LCI.Nuget\LCI.jQueryMobile.MVC4
Del LCI.jQueryMobile.MVC4*.nupkg /Q

@ECHO Packaging Recipe using NuGet
"C:\Program Files (x86)\Microsoft Visual Studio 10.0\Common7\Tools\nuget" pack LCI.jQueryMobile.MVC4.nuspec

@ECHO Publishing NuGet Package LCI.jQueryMobile.MVC4.*.nupkg to My Packages Folder - C:\Projects\NuGet\
Copy LCI.jQueryMobile.MVC4*.nupkg C:\Projects\NuGet\ /y
@PAUSE
