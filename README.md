# MBAPI-TestApp

Test application for the Measure Boost Webhooks API

(C) Copyright 2025 RFMS Australasia Limited

## Background

The Measure Boost Webhooks API is a service that allows customers of RFMS Australasia to create custom integrations
with Measure Mobile and Measure Desktop.

You create this integration by building and hosting on the Internet a web service that receives and replies
to webhooks sent by Measure Boost. Once configured your web service will be called *in real time* when your users perform
certain actions in Measure, including: searching for customer and products, and exporting worksheet data.

This solution is a sample implementation of this specification. You can use this solution as a starting
point. This example code returns static data defined in .json files to demonstrate how such a system can be 
designed.

In the simplest scenario, simply fork this project and then replace the code in the Services/DataAccess.cs with
logic that connects to your data. 

## NOTE

This system is still under active development and subject to change

## Deployment

- Deploy to Azure as an App Service
- Once deployed, go to Environment variables:
	- Add a new App Setting
	- Name should be "Settings:SharedSecret"
	- Value should be the shared secret provided by RFMS Australasia

## Verification

HMAC verification is optional. To disable this feature, modify Program.cs and remove this line:

```app.UseMiddleware<HmacVerificationService>();```

## Testing

A simple test library is included in this solution. It allows you to run and test your code locally prior to deployment.

## More information

See documentation here: https://public.3.basecamp.com/p/uwfsCpMLLD3RnXqFFKKCsHQd
