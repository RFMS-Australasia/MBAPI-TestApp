# MBAPI-TestApp

Test application for the Measure Boost API

(C) Copyright 2025 RFMS Australasia Limited

## Background

The Measure Boost API is a service that allows customers of RFMS Australasia to create custom integrations
with Measure Mobile and Measure Desktop.

You create this integration by building and hosting on the Internet a web service that meets the specification 
for the Measure Boost API. Once configured your web service will be called *in real time* when your users perform
certain actions in Measure, including: searching for customer and products, and exporting worksheet data.

This solution is a sample implementation of this API specification. You can use this solution as a starting
point. This example code returns static data defined in .json files to demonstrate how such a system can be 
designed.

In the simplest scenario, simply fork this project and then replace the code in the Services/DataAccess.cs with
logic that connects to your data. 
