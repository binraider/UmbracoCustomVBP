# UmbracoCustomVBP
A quick n dirty virtual path provider for .net - primarily Umbraco 7, as the test logging will write to /App_Data/Logs

## WHY?

Why did i build this? We have a few sites that have micro sites of flat html - sometimes with flipbooks, or javascript applications in them, that sort of thing. Since we use Azure WebApps almost exclusively it is a pain having to put all these thousands of files into the repo, for when we swap slots when using CI. I attempted to use Microsoft's VPP, in conjunction with Azure Blob Api, however there were various drawbacks along the way, so i ended up with this quick and dirty version. Yes we could use Front Door - in fact some of our clients use Front Door, but some don't, and so for them there is this. In addition Azure storage offers a specialised "$web" folder, that is made to host a static site - however i did not want to rely on that for this application.

## TO USE:

1) Place dll in the bin folder, or install the nuget package when it is ready (Its not ready).

2) Add these keys to AppSettings, with your own data, obvs

        <add key="vbp:startpaths" value="thecompany-foundation-annual-review-2020,thecompany-exchange-autumn-2020" />
        <add key="vbp:blobcontainerpath" value="https://thecompanydata.blob.core.windows.net/vpp" />
        <add key="vbp:defaultdocument" value="index.html" />
        <add key="vbp:404document" value="thecompany-foundation-annual-review-2020/index.html" />
        <add key="vbp:debuglogging" value="false" />

3) You need to add a reference to the module in the web.config. Without this it wont work at all. Also you should make this the first module - certainly before "UmbracoModule", "ClientDependencyModule" and "ImageProcessorModule", or again it wont work.

        <remove name="vppModule" />
        <add name="vppModule" type="UmbracoCustomVBP.CustomVbpModule" />
    

4) You need to Amend the AppSetting "umbracoReservedUrls" to add the paths at the end. Slightly different format now - with the tilde and leading slash this time. 

        ",~/thecompany-foundation-annual-review-2020,~/thecompany-exchange-autumn-2020"
    

5) Don't forget to upload your folders and files to the blob storage.


## HOW IT WORKS
It works by individually downloading the requested files and then pumping them out through the website front door as bytes, as if they are hosted locally.

## SETTINGS EXPLAINED
Going through the settings in turn:

#### Base folders

    <add key="vbp:startpaths" value="thecompany-foundation-annual-review-2020,thecompany-exchange-autumn-2020" />

These are the base folders that you want to "virtualize". It is a comma delimited list of the folders in question, without leading or trailing slashes. Requests starting with these folders will be handled by this Virtual Handler. 

#### Blob container Url

    <add key="vbp:blobcontainerpath" value="https://thecompanydata.blob.core.windows.net/vpp" />
This is the public address of your blob container. The blob container needs to have public access set, or it won't work. I have decided to create a blob container called "vpp" for this, as you can see. 

#### Default document

    <add key="vbp:defaultdocument" value="index.html" />
 This is the default document, for when the user browses to a folder. As Azure blob storage (not the $web container, a normal version) will not look for a default document. 

#### 404 File

    <add key="vbp:404document" value="thecompany-foundation-annual-review-2020/index.html" />
This is a path to your default 404 file. Alternatively you could put a custom 404.html page in the root of the container, and reference that like this:

    <add key="vbp:404document" value="404.html" />

#### Logging setting

Lastly there is a logging setting:

    <add key="vbp:debuglogging" value="false" />
This is ONLY for when its just you using the website on your dev workstation - its not thread safe, so if you accidently leave it on, when deployed to production you are asking for trouble.


    
    

