⚠️ *Code Migration Notice*: The implementation of GxSoapHandler is now included in the GeneXus .NET standard classes. You can access the code and details at [DotNetClasses/GxSoapHandler](https://github.com/genexuslabs/DotNetClasses/tree/gx-soap-handler/dotnet/src/extensions/ws/src/GxSoapHandler)

# GxSoapHandler
Component to rewrite webservice credential. When calling a service, its values ​​can be overwritten using the location data type.

Starting with the GeneXus v15 upgrade 11 version, there is a new property (Non Standard), location.Configuration, that allows these parameters to be made dynamic. 

In GeneXus, something like this would be programmed:

```
&location = GetLocation("WS_eFactura") 
&location.Configuration= "Name;IDENTITY;PATH_CERT_DGI; PATH_ CERT_CLIENT;PASSWORD;URI" // This is the new property, where It pass a string in this case

//It load a query as a test for DGI
&pWS_eFacturaData.xmlData.FromString("")
&pWS_eFacturaDataResult = &WS_eFactura.EFACRECEPCIONSOBRE(&pWS_eFacturaData)
&longvar = &pWS_eFacturaDataResult.ToXml()
```

A GxSoapHandler.dll assembly must be created with this project, that receives the parameters and modifies the credentials.

Additionally, this line must be added to the `web.config` file:
```
<add key="NativeChannelConfigurator" value="GxSoapHandler" />
```
