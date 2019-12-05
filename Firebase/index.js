const admin = require("firebase-admin");
const express = require("express");
const app = express();

const port = 3000;

// Init firestore database
let serviceAccount = require("./service-accounts/walldash-database-4d4ce7a14eb3.json");
admin.initializeApp({credential: admin.credential.cert(serviceAccount)});
var db = admin.firestore();

// https://hapi.dev/family/joi/?v=16.1.8
// https://www.youtube.com/watch?v=pKd0Rpw7O48

// WebAPI
app.use(express.json()); // use json for responses

// Endpoints
var metricsEndpoint = "/api/Metrics/";
var accountsEndpoint = "/api/Accounts/";

app.get(metricsEndpoint, async (req, res) => 
{
	try 
	{
		let data = [];

		console.log(`test test ${metricsEndpoint}`);
		console.log(`test thx nodemon! ${metricsEndpoint}`);
		console.log(`test ok? ${metricsEndpoint}`);

		// What will we do?

		let metrics = db.collection('Metrics');
		let metricData = await metrics.get()
		.then(snapshot => 
		{
			snapshot.forEach(doc => 
			{
				console.log(doc.id, '=>', doc.data());
				data.push(doc.data());
			});
		});
	
		res.send(data);
	} 
	catch(error) 
	{
		console.log(error);
		res.send(error);
	}
});


app.listen(port, () => {
	console.log(`Listening on port ${port}...`);
});