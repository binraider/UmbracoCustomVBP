# UmbracoCustomVBP
A quick n dirty virtual path provider for .net - primarily Umbraco 7, as the test logging will write to /App_Data/Logs


To Use:
Place dll in the bin folder

Add these keys to AppSettings, with your own data, obvs

    <add key="vbp:startpaths" value="thecompany-foundation-annual-review-2020,thecompany-exchange-autumn-2020" />
    <add key="vbp:blobcontainerpath" value="https://thecompanydata.blob.core.windows.net/vpp" />
    <add key="vbp:defaultdocument" value="index.html" />
    <add key="vbp:404document" value="thecompany-foundation-annual-review-2020/index.html" />
    <add key="vbp:debuglogging" value="false" />

#### WHY?

Why did i build this? We have a few sites that have micro sites of flat html - sometimes with flipbooks, or javascript applications in them, that sort of thing. Since we use Azure WebApps almost exclusively it is a pain having to put all these thousands of files into the repo, for when we swap slots when using CI. I attempted to use Microsoft's VPP, in conjunction with Azure Blob Api, however there were various drawbacks along the way, so i ended up with this quick and dirty version.

#### SETTINGS EXPLAINED
Going through the settings in turn:

    <add key="vbp:startpaths" value="thecompany-foundation-annual-review-2020,thecompany-exchange-autumn-2020" />

These are the base folders that you want to "virtualize". It is a comma delimited list of the folders in question, without leading or trailing slashes. Requests starting with these folders will be handled by this Virtual Handler. 

    <add key="vbp:blobcontainerpath" value="https://thecompanydata.blob.core.windows.net/vpp" />
This is the public address of your blob container. The blob container needs to have public access set, or it won't work. I have decided to create a blob container called "vpp" for this, as you can see. 

    <add key="vbp:defaultdocument" value="index.html" />
 This is the default document, for when the user browses to a folder. As Azure blob storage (not the $web container, a normal version) will not look for a default document. 

    <add key="vbp:404document" value="thecompany-foundation-annual-review-2020/index.html" />
This is a path to your default document. You could put a custom 404.html page in the root of the container, and reference that like this:

    <add key="vbp:404document" value="404.html" />

Lastly there is a logging setting:
    <add key="vbp:debuglogging" value="false" />
This is only for when its just you using the website on your dev workstation - its not thread safe, so if you accidently leave it on, when deployed to production you are asking for trouble.

#### OTHER Settings

You need to add a reference to the module in the web.config:

    <remove name="vppModule" />
    <add name="vppModule" type="UmbracoCustomVBP.CustomVbpModule" />


Lastly you need to Amend the AppSetting "umbracoReservedUrls" to add the paths at the end. Slightly different format now - with the tilde and leading slash this time.

    ",~/thecompany-foundation-annual-review-2020,~/thecompany-exchange-autumn-2020"
    
    
#### Logging

There is a logging setting for when you are checking it out on your dev environment
