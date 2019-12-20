// Dependencies
import functions from "firebase-functions";
import firestoreAPI from "../firestoreAPI";
import express from "express";
import joi from "@hapi/joi"; // https://hapi.dev/family/joi/?v=16.1.8
import cors from "cors";

// Fields
const app = express();
const collectionName = "dashboards";
const baseEndpoint = `/${collectionName}/`;

// Joi Schemas
const dashboardSchema = joi.object({
    accountId: joi.string()
        .required(),
    
    title: joi.string()
        .min(3)
        .max(128)
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
        let validationResult = dashboardSchema.validate(req.body);

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
            response.errorMessage = `Could not retrieve ${collectionName}.`;
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
        let itemId = req.params.id;
        let data = await firestoreAPI.getDocument(collectionName, itemId);

        // If data is not undefined/null then it succeeded
        if(!(data === undefined))
        {
            console.log(data);
            response.data = data;
            res.status(200);
        }
        else 
        {
            response.errorMessage = `Could not find data with id: ${itemId}`;
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
        let itemId = req.params.id;
        let validationResult = dashboardSchema.validate(req.body);

        // If error is undefined/null, then validation succeeded. 
        if(validationResult.error === undefined)
        {
            let success = await firestoreAPI.updateDocument(collectionName, validationResult.value, itemId);
            if(success)
            {
                response.data = "200 OK";
                res.status(200);
            }
            else 
            {
                response.errorMessage = `Could not find data with id: ${itemId}`;
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
        let itemId = req.params.id;
        if(itemId === undefined)
        {
            response.errorMessage = "An id must be specified.";
            res.status(405);
        }
        else
        {
            let success = await firestoreAPI.deleteDocument(collectionName, itemId);
            if(success)
            {
                response.data = "200 OK";
                res.status(200);
            }
            else 
            {
                response.errorMessage = `Could not find data with id: ${itemId}`;
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
exports.app = functions.region("europe-west1").https.onRequest(app);
