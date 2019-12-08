// Dependencies
const functions = require('firebase-functions');
const firestoreAPI = require("./firestoreAPI");
const express = require("express");
const joi = require("@hapi/joi"); // https://hapi.dev/family/joi/?v=16.1.8
const cors = require("cors");

// Fields
const app = express();
const collectionName = "Metrics";
const baseEndpoint = `/${collectionName}/`;

// Joi Schemas
const metricSchema = joi.object({
    accountId: joi.string()
        .required(),
    
    alias: joi.string()
        .min(3)
        .max(128)
        .required(),

    number: joi.number()
        .required(),

    timestamp: joi.string()
        .isoDate().message("\"timestamp\" must be in ISO 8601 date format")
        .required()
});

// RESTful API
app.use(express.json()); // use json for responses
app.use(cors({ origin: true }));

// Create
app.post(baseEndpoint, async (req, res) => 
{
    let response = {
        "data": {},
        "errorMessage": ""
    }

    try 
	{
        let validationResult = metricSchema.validate(req.body);

        // If error is undefined, then validation succeeded. 
        if(validationResult.error === undefined)
        {
            let data = await firestoreAPI.addDocument(collectionName, validationResult.value);
            response = data;
            res.status(201);
        }
        else
        {
            response.errorMessage = String(validationResult.error);
            res.status(400);
        }
	}
	catch(error) 
	{
        response.errorMessage = String(error);
        console.log(response.errorMessage);
        res.status(400);
    }
    
    res.send(response);
});

// Retrieve all
app.get(baseEndpoint, async (req, res) => 
{
    let response = {
        "data": undefined,
        "errorMessage": undefined
    }

	try 
	{
        let data = await firestoreAPI.getDocuments(collectionName);

        // If data is not undefined, then it succeeded
        if(!(data === undefined) && data.length > 0)
        {
		    response.data = data;
            res.status(200);
        }
        else
        {
            response.errorMessage = "Could not retrieve metrics.";
            res.status(404);
        }
	}
	catch(error) 
	{
        response.errorMessage = String(error);
        console.log(response.errorMessage);
        res.status(400);
    }
    
    res.send(response);
});

// Retrieve one
app.get(baseEndpoint + ":id", async (req, res) => 
{
    let response = {
        "data": undefined,
        "errorMessage": undefined
    }

	try 
	{
        let metricId = req.params.id;
        let data = await firestoreAPI.getDocument(collectionName, metricId);

        // If data is not undefined/null then it succeeded
        if(!(data === undefined))
        {
            console.log(data);
            response.data = data;
            res.status(200);
        }
        else 
        {
            response.errorMessage = `Could not find metric with id: ${metricId}`;
            res.status(404);
        }
	}
	catch(error) 
	{
        response.errorMessage = String(error);
        console.log(response.errorMessage);
        res.status(400);
	}
    
    res.send(response);
});

// Update
app.put(baseEndpoint + ":id", async (req, res) => 
{
    let response = {
        "data": {},
        "errorMessage": ""
    }

    try 
	{
        let metricId = req.params.id;
        let validationResult = metricSchema.validate(req.body);

        // If error is undefined/null, then validation succeeded. 
        if(validationResult.error === undefined)
        {
            let success = await firestoreAPI.updateDocument(collectionName, validationResult.value, metricId);
            if(success)
            {
                response.data = "200 OK";
                res.status(200);
            }
            else 
            {
                response.errorMessage = `Could not find metric with id: ${metricId}`;
                res.status(404);
            }
        }
        else
        {
            response.errorMessage = String(validationResult.error);
            res.status(400);
        }
	}
	catch(error) 
	{
        response.errorMessage = String(error);
        console.log(response.errorMessage);
        res.status(400);
    }
    
    res.send(response);
});

// Delete
app.delete(baseEndpoint + ":id", async (req, res) => 
{
    let response = {
        "data": {},
        "errorMessage": ""
    }

    try 
	{
        let metricId = req.params.id;
        if(metricId === undefined)
        {
            response.errorMessage = "An Id must be specified.";
            res.status(405);
        }
        else
        {
            let success = await firestoreAPI.deleteDocument(collectionName, metricId);
            if(success)
            {
                response.data = "200 OK";
                res.status(200);
            }
            else 
            {
                response.errorMessage = `Could not find metric with id: ${metricId}`;
                res.status(404);
            }
        }
	}
	catch(error) 
	{
        response.errorMessage = String(error);
        console.log(response.errorMessage);
        res.status(400);
    }
    
    res.send(response);
});

// DEBUG
// var port = 3000;
// app.listen(port, () => {
// 	console.log(`Listening on port ${port}...`);
// });

// Expose Express API as a single Cloud Function:
exports.api = functions.region("europe-west1").https.onRequest(app);
