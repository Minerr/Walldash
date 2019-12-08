const admin = require("firebase-admin");

// Documentation
// https://firebase.google.com/docs/reference/js/firebase.firestore.CollectionReference

// Init firestore database
let serviceAccount = require("./service-accounts/walldash-database-4d4ce7a14eb3.json");
admin.initializeApp({credential: admin.credential.cert(serviceAccount)});
var db = admin.firestore();

// API functions
// Get all
async function getDocuments(collectionName)
{
    let collection = db.collection(collectionName);
    let data = await collection.get().then(snapshot => 
    {
        let documents;
        if(!snapshot.empty)
        {
            documents = [];
            snapshot.forEach(doc => 
            {
                if(doc.exists)
                {
                    documents.push(Object.assign({ id: doc.id }, doc.data()));
                }
            });
        }

        return documents;
    });

    return data;
}

// Get all
async function getDocumentsByAlias(collectionName, alias)
{
    let collection = db.collection(collectionName);
    let data = await collection.where("alias", "==", alias).get().then(snapshot => 
    {
        let documents;
        if(!snapshot.empty)
        {
            documents = [];
            snapshot.forEach(doc => 
            {
                if(doc.exists)
                {
                    documents.push(Object.assign({ id: doc.id }, doc.data()));
                }
            });
        }

        return documents;
    });

    return data;
}


// Get one
async function getDocument(collectionName, id)
{
    let data = await db.collection(collectionName).doc(id).get().then(doc => 
    {
        let document;
        if (doc.exists) 
        {
            document = Object.assign({ id: doc.id }, doc.data());
        }

        return document;
    });

    return data;
}

// Add
async function addDocument(collectionName, document)
{

    let collection = db.collection(collectionName);
    let documentId = await collection.add(document).then(ref => 
    {
        return ref.id;
    });

    return documentId;
}

// Update
async function updateDocument(collectionName, document, id)
{
    let ref = db.collection(collectionName).doc(id);
    let exists = await ref.get().then(snapshot => {
        return snapshot.exists;
    });
    
    if(exists)
    {
        ref.set(document);
        return true;
    }
    return false;
}

// Delete
async function deleteDocument(collectionName, id)
{
    let ref = db.collection(collectionName).doc(id);
    let exists = await ref.get().then(snapshot => {
        return snapshot.exists;
    });
    
    if(exists)
    {
        ref.delete();
        return true;
    }
    return false;
}


// Export functions
module.exports.getDocuments = getDocuments;
module.exports.getDocument = getDocument;
module.exports.getDocumentsByAlias = getDocumentsByAlias;

module.exports.addDocument = addDocument;

module.exports.updateDocument = updateDocument;

module.exports.deleteDocument = deleteDocument;

