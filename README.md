# GxSoapHandler
Component to rewrite webserivce credential
Al llamar a un servicio se pueden sobreescribir los valores del mismo usando el tipo de datos location. 
A partir de la version Genexus V15 Upgrade 11 hay una nueva propiedad (Non Standard), location.Configuration,que permite hacer dinamicos estos parametros 
En genexus se debe programar algo  asi 

  &location = GetLocation("WS_eFactura")
	&location.Configuration= "Nombre;IDENTITY;PATH_CERT_DGI; PATH_ CERT_CLIENTE;PASSWORD;URI"    // Esta es la nueva propiedad,donde paso un string  en este caso

	//Cargo una consulta como test para DGI
	&pWS_eFacturaData.xmlData.FromString("")
	&pWS_eFacturaDataResult = &WS_eFactura.EFACRECEPCIONSOBRE(&pWS_eFacturaData)
	&longvar = &pWS_eFacturaDataResult.ToXml()


Se debe armar un assembly GxSoapHandler.dll con este proyectoque recibe los parametros y modifica las credenciales

Ademas se debe agregar en el web.config la linea   
           <add key="NativeChannelConfigurator" value="GxSoapHandler" />
    
