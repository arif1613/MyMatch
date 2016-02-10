This is the readme file for LCI.jQueryMobile.MVC4.

This project will pull in the jQuery.Mobile NuGet package, and will add in the _Layout.Phone and _Layout.Tablet files
that you need to make this work.  Complete the three steps below and you should be on your way.

------------------------------------------------------------------------------------------------------------------------
1. Update the Global.asax.cs file
------------------------------------------------------------------------------------------------------------------------
   Add the following lines in the Global.asax.cs file:
   
      DisplayModeConfig.RegisterDisplayModes();

------------------------------------------------------------------------------------------------------------------------
2. Update the BundleConfig.cs file
------------------------------------------------------------------------------------------------------------------------
   Add the following lines in the App_Start\BundleConfig.cs file:
   
      bundles.Add(new ScriptBundle("~/bundles/MobileJS").Include(
        "~/Scripts/jquery.mobile-{version}.js",
        "~/Scripts/jquery-{version}.js"));

      bundles.Add(new StyleBundle("~/Content/MobileCSS").Include(
        "~/Content/jquery.mobile.structure-{version}.min.css",
        "~/Content/jquery.mobile-{version}.css"));

------------------------------------------------------------------------------------------------------------------------
3. Replace the icons in the Content\Images folder
------------------------------------------------------------------------------------------------------------------------
  I've included template icons needed for mobile device bookmarks and startup.  However, they 
  are for you to see and replace, as they are intentionally hideously green and yellow.  
  Please replace them or exclude them from your project and don't publish your app with them!

------------------------------------------------------------------------------------------------------------------------
4. Compile and Run
------------------------------------------------------------------------------------------------------------------------
