# UmbracoCustomVBP
A quick n dirty virtual path provider for .net - primarily Umbraco 7, as the logging will write to /App_Data/Logs

To Use:
Place dll in the bin folder

Add these keys to AppSettings, with your own data, obvs

    <add key="vbp:startpaths" value="thecompany-foundation-annual-review-2020,thecompany-exchange-autumn-2020" />
    <add key="vbp:blobcontainerpath" value="https://thecompanydata.blob.core.windows.net/vpp" />
    <add key="vbp:defaultdocument" value="index.html" />
    <add key="vbp:404document" value="thecompany-foundation-annual-review-2020/index.html" />
    <add key="vbp:debuglogging" value="false" />




Going through the settings in turn:

    <add key="vbp:startpaths" value="thecompany-foundation-annual-review-2020,thecompany-exchange-autumn-2020" />
In this example i have two folders in the blob container, basically two micro sites with flat html in them, and i put the names of those folders in the "vbp:startpaths" AppSetting like so: "thecompany-foundation-annual-review-2020,thecompany-exchange-autumn-2020". They are a comma delimited list. Any request that starts with these paths will be handled by this Virtual Blob Handler, unless it cannot be found, in which case it will be handled by the parent Umbraco 7 instance.

    <add key="vbp:blobcontainerpath" value="https://thecompanydata.blob.core.windows.net/vpp" />
This is the public address of your blob container. The blob container needs to have public access set, or it won't work. I have decided to create a blob container called "vpp" for this, as you can see. 

    <add key="vbp:defaultdocument" value="index.html" />
 This is the default document, for when the user browses to a folder. As Azure blob storage (not the $web container, a normal version) will not look for a default document. 

    <add key="vbp:404document" value="thecompany-foundation-annual-review-2020/index.html" />
This is a path to your default document. You could put a custom 404.html page in the root of the container, and reference that like this:
    <add key="vbp:404document" value="404.html" />


Amend the AppSetting "umbracoReservedUrls" to add these paths at the end:

    ",~/thecompany-foundation-annual-review-2020,~/thecompany-exchange-autumn-2020"
    
