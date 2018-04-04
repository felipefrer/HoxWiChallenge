Thank you for downloading HoxWi Client

Please note that any data stored during this alpha period could be removed at any time due to techinical changes.

Please add this settings to your app settings or Web.Config file in order to make your client work:

    <add key="DefaultHoxDbSvcUrl" value="https://hoxwi.com"/>
    <add key="HoxDbApiSecret" value="YOUR-SECRET-KEY"/>
    <add key="DefaultHoxDbMode " value="Dynamic"/>

Before start using this component please create your account, it is completely free for up to a certain amount of transactions/month.

To create your account go to www.hoxwi.com or perform a single call (using postman for sample), as demonstrated bellow:

PUT: https://www.hoxwi.com/Wi/NewAccount

Type: application/json

Body: 
{
  "name":"Your Name",
  "email": "Your Email",
}

Then save the result carefully as it is your passport for the platform:

{
  "readyToUse": true,
  "secretkey": "xyz secret key value",
  "message": "Please save your secret key. It is your passport to access your services."
}



A very simple sample of use for this client is:

using HoxWi.Db;

...

        [HttpPost]
        public ActionResult Index(FormCollection form)
        {
            var client = new Client(Modes.Dynamic);
            var res = client.Add(new Db.InsertRequest("mycontainer", form));
            if (res.Success)
                ViewBag.Message = string.Format("Register had been included with the key {0}", res.Result);
            else
                ViewBag.Error = res.Error;

            return View();
        }

To expose an endpoint to provide or collect data from external sources (like partners) you must create public endpoints, following these steps:

1.	Create your public key (one for each external player if you wish)

Make a PUT call to: https://hoxwi.com/Wi/NewPublicKey

Raw body:

{
  "secretkey":"YOUR-SECRET-KEY",
  "publickey": "THE-PUBLIC-KEY-NAME",
  "description": "Any description to help you understand what this endpoint does"
}

2.	Configure your endpoint

Make a PUT call to: https://hoxwi.com/Dynamic/Add

Raw body:
{
  "secretkey":"YOUR-SECRET-KEY ",
  "container": "hoxendpoints",
  "lazy":true,
  "document": {
  	    "name":"THE-PUBLIC-ENDPOINT-NAME",
        "container": "THE-INTERNAL-CONTAINER",
        "storageType": "mysql",
        "action": "insert",
        "restricted": true,
        "preJs":"",
        "posJs":""
    }
}

That is all. You now can share your endpoint with third parties and let then insert data into your brand-new container, the address would be:

https://hoxwi.com/Wi/ THE-PUBLIC-KEY-NAME/THE-PUBLIC-ENDPOINT-NAME

The raw body can contain any valid Json data.